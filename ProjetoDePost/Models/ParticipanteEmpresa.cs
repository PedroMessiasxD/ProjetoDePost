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

        [ForeignKey("Usuario")]  
        public string UsuarioId {  get; set; }
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        [ForeignKey("Campanha")]
        public int? CampanhaId { get; set; }
        [JsonIgnore]
        public Campanha Campanha { get; set; }
        public string Papel {  get; set; }
        public DateTime DataEntrada { get; set; } = DateTime.Now;


    }
}
