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
        /// Participações do usuário em várias empresas (relação N:N).
        /// </summary>
        [JsonIgnore]
        public ICollection<ParticipanteEmpresa> ParticipanteEmpresas { get; set; } = new List<ParticipanteEmpresa>();

       

    }
}
