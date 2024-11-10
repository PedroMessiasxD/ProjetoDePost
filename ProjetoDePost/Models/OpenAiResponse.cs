using Newtonsoft.Json;

namespace ProjetoDePost.Models
{
    /// <summary>
    /// Representa a estrutura de dadados que é recebida pela API
    /// </summary>
    public class OpenAiResponse
    {
        [JsonProperty("choices")]
        public List<Choice> Choices { get; set; }
    }
}
