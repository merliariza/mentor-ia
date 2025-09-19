using System;
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
            _http.BaseAddress = new Uri("http://localhost:11434");
            _http.Timeout = TimeSpan.FromSeconds(60); // Timeout seguro
        }

        public async Task<string> AskAsync(string prompt)
        {
            try
            {
                var request = new
                {
                    model = "llama3:latest", // modelo correcto según curl
                    prompt = prompt,
                    stream = false
                };

                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync("/api/generate", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorText = await response.Content.ReadAsStringAsync();
                    return $"Error en Ollama: {response.StatusCode} - {errorText}";
                }

                var responseString = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(responseString);
                // Algunos modelos devuelven un array, ajustamos según la documentación
                if (doc.RootElement.TryGetProperty("results", out var results) && results.GetArrayLength() > 0)
                {
                    return results[0].GetProperty("content")[0].GetString() ?? "";
                }

                // Fallback
                return doc.RootElement.GetProperty("response").GetString() ?? "";
            }
            catch (Exception ex)
            {
                return $"Ocurrió un error al conectarse a Ollama: {ex.Message}";
            }
        }
    }
}
