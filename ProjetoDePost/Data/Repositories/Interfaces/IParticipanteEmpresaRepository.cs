using ProjetoDePost.Data.Repositories.Interfaces.Generic;
using ProjetoDePost.Models;
using System.Linq.Expressions;

namespace ProjetoDePost.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface específica para operações relacionadas à entidade ParticipanteEmpresa.
    /// </summary>
    public interface IParticipanteEmpresaRepository : IGenericRepository<ParticipanteEmpresa>
    {

        Task<ParticipanteEmpresa> ObterAdminPorEmpresaIdAsync(int empresaId, string usuarioId);
       
        
        /// <summary>
        /// Obtém a participação de um usuário específico em uma empresa específica.
        /// </summary>
        Task<IEnumerable<ParticipanteEmpresa>> ObterParticipantesPorEmpresaIdAsync(int empresaId);
        Task<ParticipanteEmpresa> BuscarPorCondicaoAsync(Expression<Func<ParticipanteEmpresa, bool>> predicate);

        Task<ParticipanteEmpresa> ObterPorIdAsync(int participanteId);

        Task<ParticipanteEmpresa> BuscarPorUsuarioIdEEmpresaIdAsync(string usuarioId, int empresaId);
       
    }
}
