using System.ComponentModel.DataAnnotations;

namespace ProjetoDePost.Data.DTOs
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = " O email é obrigatório.")]
        [EmailAddress(ErrorMessage =" Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
