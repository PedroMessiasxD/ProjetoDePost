using System.ComponentModel.DataAnnotations;

namespace ProjetoDePost.Data.DTOs
{
    public class SolicitacaoCadastroEmpresaDto
    {
        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        public string NomeEmpresa { get; set; }

        [Required(ErrorMessage = "A descrição da empresa é obrigatória.")]
        public string DescricaoEmpresa { get; set; }

        [Required(ErrorMessage = "O setor de atuação é obrigatório.")]
        public string SetorAtuacao { get; set; }

        [Url(ErrorMessage = "O link da rede social deve ser uma URL válida.")]
        public string LinkRedeSocial { get; set; }

        [Required(ErrorMessage = "O nome do administrador é obrigatório.")]
        public string NomeAdministrador { get; set; }

        [EmailAddress(ErrorMessage = "O email do administrador deve ser um endereço de email válido.")]
        public string EmailAdministrador { get; set; }

        [Required(ErrorMessage = "O telefone do administrador é obrigatório.")]
        public string TelefoneAdministrador { get; set; }
    }
}
