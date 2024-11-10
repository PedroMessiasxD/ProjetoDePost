using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoDePost.Models
{
    /// <summary>
    /// Representa uma postagem gerada ou criada manualmente em um projeto.
    /// </summary>
    public class Postagem
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Identificador do projeto ao qual a postagem pertence.
        /// </summary>
        [ForeignKey("Campanha")]
        public int CampanhaId { get; set; }
        public Campanha Campanha { get; set; }
        public bool Postado { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;        
        public string ConteudoGerado { get; set; }
    }
}
