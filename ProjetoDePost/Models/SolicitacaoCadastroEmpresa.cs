using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoDePost.Models
{
    /// <summary>
    /// Representa uma solicitação de cadastro de empresa, feita por um usuário.
    /// </summary>
    public class SolicitacaoCadastroEmpresa
    {
        public int Id { get; set; }
        
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public string NomeEmpresa { get; set; }
        public string DescricaoEmpresa { get; set; } 
        public string SetorAtuacao { get; set; }
        public string LinkRedeSocial { get; set; }    
        public string NomeAdministrador{ get; set; }
        public string EmailAdministrador{ get; set; }
        public string TelefoneAdministrador { get; set; }
        public string Status { get; set; }
        public DateTime DataSolicitacao { get; set; } = DateTime.Now;
        public bool Aprovado { get; set; } = false;
    }
}
