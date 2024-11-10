using System.Security.Cryptography.X509Certificates;

namespace ProjetoDePost.Data.DTOs
{
    // Só membros da Empresa.
    public class SolicitacaoCampanhaCreateDto
    {
        public string Nome { get; set; }
        public int EmpresaId { get; set; }
        public string TemaPrincipal { get; set; }
        public int Frequencia { get; set; }
        public string Descricao { get; set; }
   
    }
}
