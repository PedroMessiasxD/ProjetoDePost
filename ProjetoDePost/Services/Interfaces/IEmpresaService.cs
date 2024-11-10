using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    public interface IEmpresaService
    {
        Task<Empresa> SolicitarCadastroEmpresaAsync(EmpresaCreateDto empresaDto, string usuarioId);
        Task<IEnumerable<Empresa>> ListarSolicitacoesAsync(string usuarioId);

        Task<IEnumerable<EmpresaReadDto>> ListarTodasAsEmpresasAsync();
    }
}
