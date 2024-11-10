namespace ProjetoDePost.Data.DTOs
{
    public class SolicitacaoCampanhaReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EmpresaId { get; set; }
        public string TemaPrincipal { get; set; }
        public int Frequencia { get; set; }
        public string Descricao { get; set; }
        public bool Aprovada { get; set; }

        public int FrequenciaMaxima { get; set; } = 10;
    }
}
