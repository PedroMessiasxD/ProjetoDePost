using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    public interface ISolicitacaoCadastroEmpresaService
    {
        Task<SolicitacaoCadastroEmpresaRespostaDto> CriarSolicitacaoAsync(SolicitacaoCadastroEmpresaDto dto, string usuarioId);
        Task<IEnumerable<SolicitacaoCadastroEmpresaRespostaDto>> ObterSolicitacoesPendentesAsync();
        Task<bool> AprovarSolicitacaoAsync(int solicitacaoId);
        Task<bool> RecusarSolicitacaoAsync(int solicitacaoId);
    }
}
