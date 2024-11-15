using Newtonsoft.Json;
using OpenAI.Chat;
using ProjetoDePost.Responses;
using ProjetoDePost.Services.Interfaces;



namespace ProjetoDePost.Services.Implementations
{
    /// <summary>
    /// Serviço que integra com a API da OpenAI para gerar ideias de postagem automaticamente.
    /// Utiliza a descrição, tema e frequência da campanha para gerar respostas baseadas no prompt.
    /// </summary>
    public class OpenAiService : IOpenAiService
    {
        private readonly string _apiKey;
        private readonly ILogger<OpenAiService> _logger;
     
        
        public OpenAiService(IConfiguration configuration, ILogger<OpenAiService> logger)
        {
            _apiKey = configuration["OpenAI:ApiKey"];
            _logger = logger;
        }

        /// <summary>
        /// Método que molda a resposta da API com base no prompt.
        /// </summary>
        /// <param name="promptDescricao"></param>
        /// <param name="temaPrincipal"></param>
        /// <param name="frequencia"></param>
        /// <returns>Resposta da API externa</returns>

        public async Task<string> GerarIdeiasDePostagem(string promptDescricao, string temaPrincipal, int frequencia)
        {

            var client = new ChatClient(model: "gpt-4o-mini", _apiKey);

            var prompt = $"Crie ideias de postagens para a seguinte descrição: '{promptDescricao}." +
                         $"O tema principal é '{temaPrincipal} e queremos {frequencia} ideias diferentes." +
                         $"Traga a resposta começando com a seguinte frase: 'Aqui estão as postagens :' " +
                         $"Separe cada ideia com esse caractér especial <--->";

                var completion = await client.CompleteChatAsync(prompt);
                var responseJson = JsonConvert.SerializeObject(completion);

                Console.WriteLine(responseJson);

            try
            {
                var parsedResponse = JsonConvert.DeserializeObject<RootResponse>(responseJson);
                if (parsedResponse?.Value?.Content != null && parsedResponse.Value.Content.Count > 0)
                {
                    return parsedResponse.Value.Content[0].Text.Trim();
                }
                else
                {
                    throw new Exception("A resposta da API está vazia ou não tem o formato esperado.");
                }
            }
            catch (JsonSerializationException ex)
            {
                throw new Exception("Falha ao deserializar a resposta da API da OpenAI.", ex);
            }


        }

    }
}
