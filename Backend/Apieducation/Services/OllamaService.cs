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
            _http.Timeout = TimeSpan.FromMinutes(5);
        }

        public async Task<string> AskAsync(string prompt)
        {
            var request = new
            {
                model = "llama3", 
                prompt = prompt,
                stream = false   
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await _http.PostAsync("http://localhost:11434/api/generate", content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var result = root.GetProperty("response").GetString();

            return result ?? "";
        }
    }
}
