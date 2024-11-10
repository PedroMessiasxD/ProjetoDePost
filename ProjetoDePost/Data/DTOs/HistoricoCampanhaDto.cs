namespace ProjetoDePost.Data.DTOs
{
    public class HistoricoCampanhaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CampanhaId { get; set; }
        public int EmpresaId { get; set; }
        public string NomeCampanha { get; set; }
        public string TemaPrincipal { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Aprovada { get; set; }
        public bool Ativa { get; set; }
        public string ConteudoGerado { get; set; }
    }
}
