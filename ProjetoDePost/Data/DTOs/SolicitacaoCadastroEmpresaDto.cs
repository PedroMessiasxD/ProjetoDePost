using System.ComponentModel.DataAnnotations;

namespace ProjetoDePost.Data.DTOs
{
    public class SolicitacaoCadastroEmpresaDto
    {
        
        public string NomeEmpresa { get; set; }        
        public string DescricaoEmpresa { get; set; }      
        public string SetorAtuacao { get; set; }     
        public string LinkRedeSocial { get; set; }       
        public string NomeAdministrador { get; set; }     
        public string EmailAdministrador { get; set; }        
        public string TelefoneAdministrador { get; set; }
    }
}
