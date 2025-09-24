using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using Domain.Entities;
using Application.Interfaces;
using System.Text.Json;
using System.Text.RegularExpressions;
using Application.DTOs;

namespace Apieducation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationController : ControllerBase
    {
        private readonly OllamaService _ollama;
        private readonly IUnitOfWork _unitOfWork;

        public EvaluationController(OllamaService ollama, IUnitOfWork unitOfWork)
        {
            _ollama = ollama;
            _unitOfWork = unitOfWork;
        }

        
        private string ExtractJson(string input)
        {
            var match = Regex.Match(input, @"\{.*\}", RegexOptions.Singleline);
            return match.Success ? match.Value : input;
        }

       
        [HttpPost("generate-quiz/{progressId}")]
        public async Task<IActionResult> GenerateQuiz(int progressId)
        {
            var progress = await _unitOfWork.Progress.GetByIdAsync(progressId);

            if (progress == null)
                return NotFound("Progress no encontrado");

            var prompt = $@"
Eres un asistente educativo. Genera un cuestionario en JSON para el tema: {progress.Topic}.
El JSON debe tener el formato EXACTO:

{{
  ""questions"": [
    {{
      ""question"": ""¿Cuál es la capital de Francia?"",
      ""options"": [""París"", ""Roma"", ""Madrid"", ""Londres""],
      ""correctAnswer"": ""París""
    }}
  ]
}}

Reglas:
- Genera entre 3 y 5 preguntas.
- Usa el idioma del tema tanto en las preguntas como en las opciones.
- Opciones deben ser plausibles.
- Solo devuelve JSON válido, nada más.";

            var rawResponse = await _ollama.AskAsync(prompt);

          
            var jsonPart = ExtractJson(rawResponse);

            try
            {
                using var doc = JsonDocument.Parse(jsonPart);
                var root = doc.RootElement;

                var quizResult = new
                {
                    questions = root.GetProperty("questions")
                                    .EnumerateArray()
                                    .Select(q => new
                                    {
                                        question = q.GetProperty("question").GetString(),
                                        options = q.GetProperty("options").EnumerateArray().Select(o => o.GetString()).ToList(),
                                        correctAnswer = q.GetProperty("correctAnswer").GetString()
                                    }).ToList()
                };

                return Ok(quizResult);
            }
            catch
            {
                return BadRequest(new { error = "La IA no devolvió un JSON válido", raw = rawResponse });
            }
        }

        [HttpPost("submit-quiz")]
        public async Task<IActionResult> SubmitQuiz([FromBody] QuizSubmissionDto submission)
        {
            var progress = await _unitOfWork.Progress.GetWithSessionsAsync(submission.ProgressId);

            if (progress == null)
                return NotFound("Progress no encontrado");

            int correctCount = submission.Answers.Count(a => a.GivenAnswer == a.CorrectAnswer);

            var session = new EvaluationSession
            {
                ProgressId = progress.Id,
                Flashcards = submission.Answers.Select(a => new Flashcard
                {
                    Question = a.Question,
                    Answer = a.CorrectAnswer
                }).ToList(),
                Score = (int)Math.Round((double)correctCount / submission.Answers.Count * 100),
                Feedback = (double)correctCount / submission.Answers.Count >= 0.7
                    ? "¡Buen trabajo! Has pasado la evaluación."
                    : "Necesitas repasar un poco más."
            };

            _unitOfWork.EvaluationSession.Add(session);

            progress.Score = session.Score;
            progress.Feedback = session.Feedback;
            _unitOfWork.Progress.Update(progress);

            await _unitOfWork.SaveAsync();

            return Ok(new
            {
                progressId = progress.Id,
                sessionId = session.Id,
                session.Score,
                session.Feedback
            });
        }

        [HttpGet("flashcards/{progressId}")]
        public async Task<IActionResult> GetFlashcards(int progressId)
        {
            var flashcards = await _unitOfWork.Flashcard.GetByProgressIdAsync(progressId);
            var result = flashcards.Select(f => new { f.Question, f.Answer }).ToList();
            return Ok(result);
        }
    }


}
