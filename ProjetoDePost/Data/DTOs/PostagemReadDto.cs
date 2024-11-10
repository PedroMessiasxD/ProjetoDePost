namespace ProjetoDePost.Data.DTOs
{
    public class PostagemReadDto
    {
        public int Id { get; set; }
        public int CampanhaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public string ConteudoGerado { get; set; }
    }
}
