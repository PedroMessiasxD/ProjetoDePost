using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoDePost.Models
{
    public class HistoricoCampanha
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [ForeignKey("Campanha")]
        public int CampanhaId { get; set; }
        public string NomeCampanha { get; set; }
        public string TemaPrincipal { get; set; }
        public Campanha Campanha { get; set; }
        public int EmpresaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public string? ConteudoGerado { get; set; }
        public bool Aprovada { get; set; }
        public bool Ativa { get; set; }
    }
}
