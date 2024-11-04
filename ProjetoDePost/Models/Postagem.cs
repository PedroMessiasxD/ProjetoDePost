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
        [ForeignKey("Projeto")]
        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }
       
        public bool Postado { get; set; }
        [Required]
        public string Tema { get; set; }
        [Required]
        [StringLength(500)]
        public string Conteudo { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
