using Newtonsoft.Json;

namespace ProjetoDePost.Responses
{
    public class RootResponse
    {
        [JsonProperty("Value")]
        public OpenAiResponse Value { get; set; }
    }
}
