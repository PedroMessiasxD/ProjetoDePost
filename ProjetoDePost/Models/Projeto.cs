using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoDePost.Models
{
    /// <summary>
    /// Representa um projeto associado a uma empresa, onde os participantes podem gerenciar posts.
    /// </summary>
    public class Projeto
    {
        public int Id { get; set; }
        public string Nome { get; set; }


        /// <summary>
        /// Identificador da empresa a qual o projeto pertence.
        /// </summary>
        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }


        public int QuantidadeMaxPostagem { get; set; }
        
        /// <summary>
        /// Coleção de posts gerenciados pelo projeto.
        /// </summary>
        public ICollection<Postagem> Postagens { get; set; } = new List<Postagem>();

    }
}
