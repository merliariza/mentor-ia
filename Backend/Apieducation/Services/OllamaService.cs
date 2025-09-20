using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OllamaService
    {
        private readonly HttpClient _http;

        public OllamaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> AskAsync(string prompt)
        {
            var request = new
            {
                model = "llama3", // 👈 asegúrate que tienes este modelo descargado en Ollama
                prompt = prompt,
                stream = false    // para recibir toda la respuesta de una vez
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await _http.PostAsync("http://localhost:11434/api/generate", content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            // Ollama responde con algo como:
            // { "model":"llama3", "created_at":"...", "response":"{ \"allow\": true, \"topic\": \"Fotosíntesis\", \"answer\": \"...\" }", "done":true }

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            // El campo que nos interesa es "response", porque allí viene la respuesta de la IA
            var result = root.GetProperty("response").GetString();

            return result ?? "";
        }
    }
}
