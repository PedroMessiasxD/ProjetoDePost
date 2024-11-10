using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    public interface ICampanhaService
    {
        Task<Campanha> CriarCampanhaAsync(CampanhaCreateDto campanhaDto);
       
        Task<CampanhaReadDto> ObterPorIdAsync(int id);
        Task<IEnumerable<CampanhaReadDto>> ObterTodasPorEmpresaAsync(int empresaId);
        Task<IEnumerable<CampanhaReadDto>> ObterVinculadasAoParticipanteAsync(int participanteId);
        Task AtualizarCampanhaAsync(int id, CampanhaUpdateDto campanhaDto);
        Task RemoverCampanha(int id);
        Task<SolicitacaoCampanha> SolicitarCriacaoDeCampanha(CampanhaCreateDto campanhaDto);
        Task<Campanha> AceitarCampanha(int solicitacaoCampanhaId);
        Task RecusarCampanha(int solicitacaoCampanhaId);
        Task<IEnumerable<SolicitacaoCampanhaReadDto>> ObservarSolicitacoesAsync();

    }
}
