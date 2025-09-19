using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTOs;

namespace Apieducation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly OllamaService _ollama;

        public AIController(OllamaService ollama)
        {
            _ollama = ollama;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] UserChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Question))
            {
                return BadRequest(new
                {
                    error = "La pregunta no puede estar vacía."
                });
            }

            // Paso 1: Validar si la pregunta es educativa usando IA
            var checkPrompt = $"Clasifica la siguiente pregunta: \"{request.Question}\". " +
                              "Responde solo con 'EDUCATIVA' si se relaciona con enseñanza (matemáticas, ciencia, historia, programación, etc.), " +
                              "o 'NO' si no es educativa.";

            var checkResult = await _ollama.AskAsync(checkPrompt);

            if (!checkResult.Contains("EDUCATIVA", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new
                {
                    user = request.User.FullName,
                    question = request.Question,
                    answer = "❌ Solo puedo responder preguntas relacionadas con aprendizaje o enseñanza."
                });
            }

            // Paso 2: Generar respuesta pedagógica
            var progressInfo = request.Progress.Any()
                ? string.Join(", ", request.Progress.Select(p => $"{p.Topic}: {p.Score} puntos"))
                : "sin progreso registrado";

            var prompt = $"El usuario {request.User.FullName} tiene este progreso: {progressInfo}. " +
                         $"Su pregunta es: {request.Question}. " +
                         "Responde como un profesor de manera clara, pedagógica y motivadora.";

            var answer = await _ollama.AskAsync(prompt);

            return Ok(new
            {
                user = request.User.FullName,
                question = request.Question,
                answer
            });
        }
    }

    // DTO de entrada al chat
    public class UserChatRequest
    {
        public UserMemberDto User { get; set; } = null!;
        public List<ProgressDto> Progress { get; set; } = new();
        public string Question { get; set; } = string.Empty;
    }
}
