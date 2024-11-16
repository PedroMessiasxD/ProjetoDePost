using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoDePost.Models
{
    public class Postagem
    {
        public int Id { get; set; }

        [ForeignKey("Campanha")]
        public int CampanhaId { get; set; }
        [JsonIgnore]
        public Campanha Campanha { get; set; }
        public bool Postado { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;        
        public string ConteudoGerado { get; set; }
    }
}
