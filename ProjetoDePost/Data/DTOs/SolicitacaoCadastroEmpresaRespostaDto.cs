namespace ProjetoDePost.Data.DTOs
{
    public class SolicitacaoCadastroEmpresaRespostaDto
    {
        public int SolicitacaoId { get; set; }
        public string Status { get; set; }
        public string Mensagem { get; set; }
        public string EmailAdministrador { get; set; }
        public string NomeAdministrador { get; set; }

    }
}
