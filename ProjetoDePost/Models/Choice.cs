using Newtonsoft.Json;

namespace ProjetoDePost.Models
{
    /// <summary>
    /// Representa uma parte da resposta que é recebida pela API. Cada Choice contém um texto gerado pela API, através de text.
    /// </summary>
    public class Choice
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
