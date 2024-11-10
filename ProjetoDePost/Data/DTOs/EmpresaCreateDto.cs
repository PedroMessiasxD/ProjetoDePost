using System.ComponentModel.DataAnnotations;

namespace ProjetoDePost.Data.DTOs
{
    public class EmpresaCreateDto
    {
        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição da empresa é obrigatória.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O setor de atuação é obrigatório.")]
        public string SetorAtuacao { get; set; }

        public string LinksRedesSociais { get; set; }
    }
}
