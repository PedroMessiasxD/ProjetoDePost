using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjetoDePost.Models
{
    /// <summary>
    /// Representa um usuário no sistema.
    /// Um usuário pode ser participante de várias empresas e administrador de outras.
    /// </summary>
    public class Usuario : IdentityUser
    {
        [Required(ErrorMessage ="O nome é obrigatório.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        public string Sobrenome { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        /// <summary>
        /// Coleção de postagens criadas pelo usuário (relação 1:N).
        /// </summary>
        public ICollection<Postagem> Postagens { get; set; } = new List<Postagem>();

        /// <summary>
        /// Participações do usuário em várias empresas (relação N:N).
        /// </summary>
        public ICollection<ParticipanteEmpresa> ParticipanteEmpresas { get; set; } = new List<ParticipanteEmpresa>();

        /// <summary>
        /// Coleção de empresas criadas e administradas pelo usuário (relação 1:N).
        /// </summary>
        public ICollection<Empresa> EmpresasCriadas { get; set; } = new List<Empresa>();

    }
}
