using System.ComponentModel.DataAnnotations;

namespace ProjetoDePost.Data.DTOs
{
    /// <summary>
    /// DTO para atualizar informações de um usuário.
    /// </summary>
    public class UsuarioUpdateDto
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
       
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Senha { get; set; }
    }
}
