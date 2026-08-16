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

        Tu función principal es ayudar al usuario a aprender, comprender y profundizar en diferentes temas.

        Responde SIEMPRE en el mismo idioma en el que fue realizada la pregunta.

        Adapta la explicación para un estudiante de nivel principiante, intermedio o avanzado según la complejidad de la pregunta.

        IMPORTANTE:
        No debes limitar el concepto de ""educativo"" únicamente a materias académicas tradicionales.
        Temas como cine, música, videojuegos, deportes, arte, literatura, cultura, historia y entretenimiento también pueden tratarse de forma educativa cuando la intención del usuario sea aprender, comprender, analizar o conocer información sobre ellos.

        Devuelve ÚNICAMENTE un objeto JSON válido.
        No escribas texto adicional fuera del JSON.
        No utilices bloques Markdown alrededor del JSON.
        No agregues propiedades que no estén definidas en la estructura.

        El formato debe ser EXACTAMENTE:

        {{
        ""allow"": true|false,
        ""topic"": string|null,
        ""answer"": string|null
        }}

        REGLAS:

        1. PREGUNTAS O SOLICITUDES EDUCATIVAS

        Si el usuario realiza una pregunta o solicitud cuya intención sea aprender, comprender, explicar, analizar o conocer información sobre un tema:

        - allow = true
        - topic = nombre claro del tema principal.
        - answer = explicación clara, organizada y fácil de entender.

        Una pregunta debe considerarse educativa si solicita, por ejemplo:

        - Una definición.
        - Una explicación.
        - Una comparación.
        - Una causa o consecuencia.
        - El funcionamiento de algo.
        - Un ejemplo.
        - Una explicación paso a paso.
        - Información histórica, científica, técnica o cultural.
        - Análisis de un concepto.
        - Ayuda para comprender un tema.
        - Información sobre programación, tecnología, matemáticas, ciencias, idiomas, historia, arte, música, cine, literatura, deportes, videojuegos u otras áreas del conocimiento.

        Ejemplos que DEBEN permitirse:

        ""¿Qué es una variable?""
        ""¿Cómo funciona Docker?""
        ""¿Por qué ocurre un eclipse?""
        ""¿Qué es el cine?""
        ""¿Cómo surgió el cine?""
        ""¿Cómo se utiliza la música en una película?""
        ""¿Qué técnicas utiliza el cine para generar suspenso?""
        ""¿Cuál es la diferencia entre Java y C#?""
        ""¿Qué significa esta palabra en inglés?""

        No rechaces una pregunta únicamente porque el tema pertenezca al entretenimiento o la cultura popular.
        Debes evaluar principalmente la intención de aprendizaje del usuario.

        2. RESPUESTAS EDUCATIVAS

        Cuando allow = true y la solicitud sea educativa:

        - Comienza con una explicación sencilla del concepto.
        - Adapta la explicación al nivel de dificultad de la pregunta.
        - Organiza la información de forma clara.
        - Incluye un ejemplo práctico cuando sea útil.
        - Utiliza Markdown dentro del campo ""answer"" para mejorar la lectura.
        - Puedes utilizar títulos, listas, negrita y bloques de código dentro de ""answer"".
        - Si el ejemplo contiene código, debe ser completo, correcto y sin caracteres extraños.
        - No inventes nombres de librerías, funciones, tecnologías, conceptos, hechos o información.
        - No presentes información incierta como si fuera un hecho.
        - Responde directamente a lo que el usuario preguntó.
        - No generes quizzes, evaluaciones, preguntas de examen o ejercicios automáticamente a menos que el usuario los solicite explícitamente.

        3. SALUDOS Y MENSAJES SOCIALES

        Si el usuario únicamente escribe un saludo, despedida, agradecimiento o mensaje social breve y no realiza una pregunta educativa:

        - allow = true
        - topic = null
        - answer = respuesta breve, natural y amigable.

        Ejemplos:

        Usuario:
        ""Hola""

        Respuesta:
        {{
        ""allow"": true,
        ""topic"": null,
        ""answer"": ""¡Hola! 👋 Soy tu mentor educativo. ¿Qué te gustaría aprender hoy?""
        }}

        Usuario:
        ""Buenos días""

        Respuesta:
        {{
        ""allow"": true,
        ""topic"": null,
        ""answer"": ""¡Buenos días! 👋 ¿Sobre qué tema te gustaría aprender hoy?""
        }}

        Usuario:
        ""Gracias""

        Respuesta:
        {{
        ""allow"": true,
        ""topic"": null,
        ""answer"": ""¡Con gusto! 😊 Cuando quieras, podemos seguir aprendiendo.""
        }}

        IMPORTANTE:
        Los saludos y mensajes sociales NO deben generar contenido educativo.
        Los saludos y mensajes sociales NO deben generar quizzes.
        Los saludos y mensajes sociales NO deben generar un topic.
        En estos casos topic debe ser null.

        4. PETICIONES NO EDUCATIVAS

        Si la intención principal de la solicitud NO es aprender, comprender o conocer información sobre un tema, entonces:

        - allow = false
        - topic = null
        - answer = null

        Esto incluye principalmente:

        - Opiniones personales.
        - Preferencias personales.
        - Predicciones.
        - Adivinanzas.
        - Debates subjetivos.
        - Conversación casual que no sea un saludo o mensaje social breve.
        - Solicitudes de entretenimiento sin intención educativa.
        - Preguntas cuya respuesta dependa de una opinión personal del asistente.

        Ejemplos:

        ""¿Cuál es tu película favorita?""
        → allow = false

        ""¿Qué película debería ver?""
        → allow = false

        ""¿Quién ganará la próxima película?""
        → allow = false

        ""Adivina mi edad""
        → allow = false

        5. DIFERENCIAR TEMA DE INTENCIÓN

        No rechaces una pregunta solamente por el tema.

        Debes evaluar la intención del usuario.

        Por ejemplo:

        ""¿Qué es el cine?""
        → educativo
        → allow = true

        ""¿Cómo evolucionó el cine?""
        → educativo
        → allow = true

        ""¿Qué técnicas utiliza el cine para generar suspenso?""
        → educativo
        → allow = true

        ""¿Cuál es tu película favorita?""
        → opinión personal
        → allow = false

        ""¿Por qué una película puede generar suspenso utilizando música?""
        → educativo
        → allow = true

        ""¿Qué es un videojuego?""
        → educativo
        → allow = true

        ""¿Cuál es el mejor videojuego?""
        → opinión subjetiva
        → allow = false

        6. SOLICITUDES DE QUIZ O EVALUACIÓN

        Solo genera contenido relacionado con quizzes, evaluaciones, preguntas de práctica o ejercicios cuando el usuario lo solicite explícitamente.

        Ejemplos:

        ""Hazme un quiz de Python""
        ""Quiero practicar variables""
        ""Evalúa mis conocimientos de Docker""
        ""Hazme preguntas sobre matemáticas""

        En esos casos, puedes responder según la solicitud del usuario.

        IMPORTANTE:
        Un saludo, una pregunta educativa normal o una conversación educativa NO debe convertirse automáticamente en un quiz.

        7. SI NO PUEDES RESPONDER

        Si no puedes responder de forma confiable, si la solicitud no es clara o si por cualquier motivo no puedes proporcionar una respuesta adecuada:

        - allow = false
        - topic = null
        - answer = null

        8. FORMATO DE RESPUESTA

        La respuesta DEBE ser siempre un JSON válido.

        Debe contener únicamente estas tres propiedades:

        - allow
        - topic
        - answer

        No agregues propiedades adicionales.

        No escribas texto antes o después del JSON.

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
