using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;
using System.Text.Json;
using System.Text.RegularExpressions;

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
            var progress = await _unitOfWork.Progress.GetWithSessionsAsync(progressId);
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

                var quizResult = root.GetProperty("questions")
                                    .EnumerateArray()
                                    .Select(q => new
                                    {
                                        Question = q.GetProperty("question").GetString(),
                                        Options = q.GetProperty("options").EnumerateArray().Select(o => o.GetString()).ToList(),
                                        CorrectAnswer = q.GetProperty("correctAnswer").GetString()
                                    }).ToList();

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
            await _unitOfWork.SaveAsync();
            var allSessions = await _unitOfWork.EvaluationSession.GetByProgressIdAsync(progress.Id);
            progress.Score = (int)Math.Round(allSessions.Average(s => s.Score));
            progress.Feedback = progress.Score >= 70
                ? "¡Buen desempeño general! Sigue así."
                : "Necesitas repasar un poco más en general.";

            _unitOfWork.Progress.Update(progress);
            await _unitOfWork.SaveAsync();

            return Ok(new
            {
                ProgressId = progress.Id,
                SessionId = session.Id,
                SessionScore = session.Score,
                SessionFeedback = session.Feedback,
                ProgressScore = progress.Score,
                ProgressFeedback = progress.Feedback
            });
        }

        [HttpGet("flashcards/{progressId}")]
        public async Task<IActionResult> GetFlashcards(int progressId)
        {
            var progress = await _unitOfWork.Progress.GetByIdAsync(progressId);
            if (progress == null)
                return NotFound("Progress no encontrado");

            var flashcards = await _unitOfWork.Flashcard.GetByProgressIdAsync(progressId);

            var result = flashcards.Select(f => new
            {
                f.Id,
                f.Question,
                f.Answer,
                EvaluationSessionId = f.EvaluationSessionId
            }).ToList();

            return Ok(result);
        }
    }
}
