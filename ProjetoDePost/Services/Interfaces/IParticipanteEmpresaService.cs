using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de operações relacionadas à ParticipanteEmpresa.
    /// </summary>
    public interface IParticipanteEmpresaService
    {
        Task<bool> UsuarioEhAdminEmpresaAsync(string usuarioId, int empresaId);
        Task<IEnumerable<ParticipanteEmpresa>> ObterParticipantesPorEmpresaAsync(int empresaId);
        Task<bool> VerificarSeUsuarioEhAdminEmpresarial(string usuarioId, int empresaId);
        Task AdicionarUsuarioNaEmpresaAsync(ParticipanteEmpresaCreateDto participanteEmpresaCreateDto);
        Task<ParticipanteEmpresaReadDto> AdicionarParticipanteACampanhaAsync(ParticipanteEmpresaCreateDto participanteEmpresaCreateDto, int campanhaId);
    }
}
