using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class OpenRouterService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;
        private readonly string _model;

        public OpenRouterService(
            HttpClient http,
            IConfiguration configuration)
        {
            _http = http;
            _http.Timeout = TimeSpan.FromMinutes(5);

            _apiKey = configuration["OpenRouter:ApiKey"]
                ?? throw new Exception("OpenRouter ApiKey no configurada.");

            _model = configuration["OpenRouter:Model"]
                ?? "poolside/laguna-s-2.1:free";

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);

            _http.DefaultRequestHeaders.Add("HTTP-Referer", "http://localhost");

            _http.DefaultRequestHeaders.Add("X-Title", "MENTOR-IA");
        }

        public async Task<string> AskAsync(string prompt)
        {
            var request = new
            {
                model = _model,
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = prompt
                    }
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await _http.PostAsync(
                "https://openrouter.ai/api/v1/chat/completions",
                content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);

            var result = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return result ?? "";
        }
    }
}