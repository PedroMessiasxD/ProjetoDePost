/*using ProjetoDePost.Services.Interfaces;

namespace ProjetoDePost.Services.Implementations
{
    public class OpenAiService : IOpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenAI:ApiKey"];
        }

        public async Task<string> GerarPost(string tema, int quantidade)
        {
            var prompt = $"Crie {quantidade} posts sobre o tema: {tema}";

            var requestContent = new
            {
                model = "text-davinci-003",
                prompt,
                max_tokens = 100,
                n = quantidade
            };
        }
    }
}
*/