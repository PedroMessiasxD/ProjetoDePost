using System.ComponentModel.DataAnnotations;

namespace ProjetoDePost.Data.DTOs
{
    /// <summary>
    /// DTO para criar um novo usuário.
    /// </summary>
    public class UsuarioCreateDto
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage ="A confirmação de senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage ="As senhas não correspondem.")]
        public string ConfirmarSenha {  get; set; }

    }
}
