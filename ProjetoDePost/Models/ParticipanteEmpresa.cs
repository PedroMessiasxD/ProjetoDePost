using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoDePost.Models
{
    /// <summary>
    /// Representa a relação entre um usuário e uma empresa.
    /// Define o papel do usuário na empresa e a data de entrada.
    /// </summary>
    public class ParticipanteEmpresa
    {
        public int Id { get; set; }

        /// <summary>
        /// Identificador do usuário que participa da empresa.
        /// </summary>
        [ForeignKey("Usuario")]  
        public string UsuarioId {  get; set; }
        
        // Navegação para o Usuário!
        public virtual Usuario Usuario { get; set; }

        /// <summary>
        /// Identificador da empresa da qual o usuário participa.
        /// </summary>
        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }
        
        // Navegação para a Empresa!
        public Empresa Empresa { get; set; }

        /// <summary>
        /// Relacionamento com a campanha
        /// </summary>
        [ForeignKey("Campanha")]
        public int? CampanhaId { get; set; }
        [JsonIgnore]
        public Campanha Campanha { get; set; }

        /// <summary>
        /// Papel do usuário dentro da empresa, como "Administrador" ou "Membro".
        /// </summary>
        public string Papel {  get; set; }

        /// <summary>
        /// Data em que o usuário entrou na empresa.
        /// </summary>
        public DateTime DataEntrada { get; set; } = DateTime.Now;


    }
}
