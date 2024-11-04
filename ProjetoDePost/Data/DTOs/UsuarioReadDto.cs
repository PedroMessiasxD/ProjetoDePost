namespace ProjetoDePost.Data.DTOs
{
    /// <summary>
    /// DTO para representar um usuário durante operações de leitura.
    /// </summary>
    public class UsuarioReadDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
