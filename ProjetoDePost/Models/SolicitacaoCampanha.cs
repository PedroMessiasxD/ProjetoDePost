using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoDePost.Models
{
    public class SolicitacaoCampanha
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }
        public string TemaPrincipal { get; set; }
        public int Frequencia { get; set; }
        public string Descricao { get; set; }
        public bool Aprovada { get; set; }
        public int FrequenciaMaxima { get; set; } 

    }
}
