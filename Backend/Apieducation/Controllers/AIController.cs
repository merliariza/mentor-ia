using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTOs;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Apieducation.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Apieducation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly OpenRouterService _ai;
        private readonly ILogger<AIController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AIController(
            OpenRouterService ai,
            ILogger<AIController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _ai = ai;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] UserChatRequestDto request)
        {
        var prompt = $@"
        Eres un mentor educativo cuyo objetivo es enseñar de forma clara, precisa y didáctica.

        Responde SIEMPRE en el mismo idioma en el que fue realizada la pregunta.

        Adapta la explicación para un estudiante de nivel principiante, intermedio o avanzado según la complejidad de la pregunta.

        Devuelve ÚNICAMENTE un objeto JSON válido. No escribas texto adicional, explicaciones fuera del JSON ni bloques Markdown.

        El formato debe ser exactamente:

        {{
        ""allow"": true|false,
        ""topic"": string|null,
        ""answer"": string|null
        }}

        Reglas:

        1. Si la pregunta es educativa:
        - allow = true
        - topic = nombre del tema principal.
        - answer = explicación clara, organizada y fácil de entender.

        2. La respuesta debe:
        - Comenzar con una explicación sencilla del concepto.
        - Incluir un ejemplo práctico cuando sea útil.
        - Si el ejemplo contiene código, este debe ser completo, correcto y sin caracteres extraños.
        - No inventar nombres, librerías o información que no exista.
        - Utilizar Markdown dentro del campo ""answer"" para mejorar la lectura (títulos, listas y bloques de código).

        3. Si la pregunta NO es educativa (opiniones personales, entretenimiento, cultura popular no académica, debates subjetivos, predicciones o adivinanzas):
        - allow = false
        - topic = null
        - answer = null

        4. Si no puedes responder por cualquier motivo:
        - allow = false
        - topic = null
        - answer = null

        5. No agregues propiedades adicionales al JSON.

        Usuario: {request.User.FullName}

        Pregunta:
        {request.Question}";

            var responseString = await _ai.AskAsync(prompt);

            _logger.LogInformation("= RESPUESTA IA =");
            _logger.LogInformation("{Response}", responseString);

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

                    
                    var existing = (await _unitOfWork.Progress.GetAllAsync())
                        .FirstOrDefault(p => p.UserMemberId == request.User.Id && p.Topic == topic);

                    if (existing == null)
                    {
                        existing = new Progress
                        {
                            UserMemberId = request.User.Id,
                            Topic = topic,
                            Score = 0,
                            Feedback = $"Tema iniciado el {DateTime.UtcNow}"
                        };
                        _unitOfWork.Progress.Add(existing);
                    }
                    else
                    {
                        existing.Feedback = $"Última interacción: {DateTime.UtcNow}";
                        _unitOfWork.Progress.Update(existing);
                    }

                    await _unitOfWork.SaveAsync();

                    return Ok(new
                    {
                        user = request.User.FullName,
                        question = request.Question,
                        topic,
                        answer,
                        progressId = existing.Id
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "No se pudo parsear la respuesta como JSON.");
                    _logger.LogWarning("Respuesta recibida:");
                    _logger.LogWarning(responseString);
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

}
