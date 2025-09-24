using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTOs;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Apieducation.Helpers;

namespace Apieducation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly OllamaService _ollama;
        private readonly ILogger<AIController> _logger;

        public AIController(OllamaService ollama, ILogger<AIController> logger)
        {
            _ollama = ollama;
            _logger = logger;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] UserChatRequest request)
        {
            var prompt = $@"
Eres un asistente educativo que RESPONDE SÓLO EN EL IDIOMA EN EL CUAL FUE HECHA LA PREGUNTA. RESPONDE ÚNICAMENTE UN OBJETO JSON VÁLIDO y NADA MÁS. No incluyas explicaciones, advertencias, ni texto libre fuera del JSON. El JSON debe seguir exactamente este formato:

{{
  ""allow"": true|false,
  ""topic"": string|null,
  ""answer"": string|null
}}

Reglas:
1) Si la pregunta es educativa, devuelve allow = true, topic = nombre del tema y answer = explicación didáctica en el idioma en el cual se hizo la pregunta.
2) Si la pregunta NO es educativa, devuelve allow = false, topic = null y answer = null.
3) Si por alguna razón no puedes procesar, devuelve allow = false, topic = null, answer = null.
4) NO uses mayúsculas innecesarias, NO incluyas comentarios, NO pongas texto adicional.
5) Si corresponde, puedes añadir un ejemplo práctico en la respuesta, para reforzar la comprensión.
Usuario: {request.User.FullName}
Pregunta: {request.Question}
";

            var responseString = await _ollama.AskAsync(prompt);

            _logger.LogInformation("IA - respuesta cruda: {Response}", responseString);

            if (JsonHelpers.TryExtractJson(responseString, out var json))
            {
                try
                {
                    using var doc = JsonDocument.Parse(json);
                    var root = doc.RootElement;

                    bool allow = root.GetProperty("allow").GetBoolean();
                    string topic = root.TryGetProperty("topic", out var t) && t.ValueKind != JsonValueKind.Null ? t.GetString() ?? "" : "";
                    string answer = root.TryGetProperty("answer", out var a) && a.ValueKind != JsonValueKind.Null ? a.GetString() ?? "" : "";

                    _logger.LogDebug("IA - JSON parseado: {Json}", json);

                    if (!allow)
                    {
                        return Ok(new
                        {
                            user = request.User.FullName,
                            question = request.Question,
                            answer = "Lo siento, no puedo dar respuesta a este tema. Solo temas educativos son válidos."
                        });
                    }

                    return Ok(new
                    {
                        user = request.User.FullName,
                        question = request.Question,
                        topic,
                        answer
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al parsear el JSON extraído. JSON: {Json}", json);
                   
                }
            }


            try
            {
                using var doc = JsonDocument.Parse(responseString);
                var root = doc.RootElement;

                bool allow = root.GetProperty("allow").GetBoolean();
                string topic = root.TryGetProperty("topic", out var t) && t.ValueKind != JsonValueKind.Null ? t.GetString() ?? "" : "";
                string answer = root.TryGetProperty("answer", out var a) && a.ValueKind != JsonValueKind.Null ? a.GetString() ?? "" : "";

                _logger.LogDebug("IA - JSON directo: {Json}", responseString);

                if (!allow)
                {
                    return Ok(new
                    {
                        user = request.User.FullName,
                        question = request.Question,
                        answer = "Lo siento, no puedo dar respuesta a este tema. Solo temas educativos son válidos."
                    });
                }

                return Ok(new
                {
                    user = request.User.FullName,
                    question = request.Question,
                    topic,
                    answer
                });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "No se pudo parsear la respuesta de la IA como JSON. Respuesta cruda guardada para auditoría.");
            }

            _logger.LogWarning("IA devolvió respuesta no-JSON para la pregunta: {Question}. Respuesta cruda: {Response}", request.Question, responseString);

            return Ok(new
            {
                user = request.User.FullName,
                question = request.Question,
                answer = "Lo siento, no puedo procesar esa petición en este momento."
            });
        }
    }

    public class UserChatRequest
    {
        public UserMemberDto User { get; set; } = null!;
        public List<ProgressDto> Progress { get; set; } = new();
        public string Question { get; set; } = string.Empty;
    }
}
