using Newtonsoft.Json;

namespace ProjetoDePost.Responses
{
    public class ContentItem
    {
        [JsonProperty("Text")]
        public string Text { get; set; }
    }
}
