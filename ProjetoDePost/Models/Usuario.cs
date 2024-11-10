using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoDePost.Models
{
    /// <summary>
    /// Representa um usuário no sistema.
    /// Um usuário pode ser participante de várias empresas e administrador de outras.
    /// </summary>
    public class Usuario : IdentityUser
    {
        
        public string Nome { get; set; }
        
        public string Sobrenome { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;


        /// <summary>
        /// Indica se o cadastro do usuário foi confirmado após a configuração inicial.
        /// </summary>
        public bool CadastroConfirmado { get; set; } = false;


        /// <summary>
        /// Indica se o usuário é o administrador de uma empresa cadastrada.
        /// </summary>
        public bool EAdministradorEmpresa { get; set; } = false;

        /// <summary>
        ///  Indica se o usuário é o "Administrador Global"
        /// </summary>
        public bool IsAdministradorGlobal { get; set; } = false;
        /// <summary>
        /// Coleção de postagens criadas pelo usuário (relação 1:N).
        /// </summary>
        public ICollection<Postagem> Postagens { get; set; } = new List<Postagem>();


        /// <summary>
        /// Participações do usuário em várias empresas (relação N:N).
        /// </summary>
        [JsonIgnore]
        public ICollection<ParticipanteEmpresa> ParticipanteEmpresas { get; set; } = new List<ParticipanteEmpresa>();

       
        
        /// <summary>
        /// Coleção de empresas criadas e administradas pelo usuário (relação 1:N).
        /// </summary>
        public ICollection<Empresa> EmpresasCriadas { get; set; } = new List<Empresa>();

    }
}
