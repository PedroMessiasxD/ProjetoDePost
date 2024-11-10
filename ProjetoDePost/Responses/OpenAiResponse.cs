using Newtonsoft.Json;

namespace ProjetoDePost.Responses
{
    public class OpenAiResponse
    {
        [JsonProperty("Content")]
        public List<ContentItem> Content { get; set; }
    }
}
