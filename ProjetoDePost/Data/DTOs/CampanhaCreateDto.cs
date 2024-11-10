namespace ProjetoDePost.Data.DTOs
{
    public class CampanhaCreateDto
    {
        public string Nome { get; set; }
        public int EmpresaId { get; set; }
        public string TemaPrincipal { get; set; }
        public int Frequencia { get; set; }
        public string Descricao { get; set; }
    
    }
}
