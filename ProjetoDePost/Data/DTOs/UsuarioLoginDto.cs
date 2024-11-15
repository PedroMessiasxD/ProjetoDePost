using System.ComponentModel.DataAnnotations;

namespace ProjetoDePost.Data.DTOs
{
    public class UsuarioLoginDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
