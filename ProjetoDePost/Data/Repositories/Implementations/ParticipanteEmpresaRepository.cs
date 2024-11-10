using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data.Repositories.Implementations.Generic;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;
using System.Linq.Expressions;

namespace ProjetoDePost.Data.Repositories.Implementations
{
    /// <summary>
    /// Repositório para operações CRUD específicas da entidade ParticipanteEmpresa.
    /// </summary>
    public class ParticipanteEmpresaRepository : GenericRepository<ParticipanteEmpresa>, IParticipanteEmpresaRepository
    {
        private readonly ProjetoDePostContext _context;

        public ParticipanteEmpresaRepository(ProjetoDePostContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ParticipanteEmpresa> ObterAdminPorEmpresaIdAsync(int empresaId,string usuarioId)
        {
            return await _context.ParticipanteEmpresas
                .Include(pe => pe.Empresa)
                .FirstOrDefaultAsync(p => p.EmpresaId == empresaId && p.UsuarioId == usuarioId && p.Papel == "Administrador");
        }

        public async Task<IEnumerable<ParticipanteEmpresa>> ObterParticipantesPorEmpresaIdAsync(int empresaId)
        {
            return await _context.ParticipanteEmpresas
                .Where(p => p.EmpresaId == empresaId)
                .ToListAsync();
        }

        public async Task<ParticipanteEmpresa> BuscarPorCondicaoAsync(Expression<Func<ParticipanteEmpresa, bool>> predicate)
        {
            return await _context.Set<ParticipanteEmpresa>().FirstOrDefaultAsync(predicate);
        }

        public async Task<ParticipanteEmpresa> ObterPorIdAsync(int participanteId)
        {
            return await _context.ParticipanteEmpresas
                .Include(pe => pe.Empresa) 
                .FirstOrDefaultAsync(pe => pe.Id == participanteId);
        }

        public async Task CriarAsync(ParticipanteEmpresa participanteEmpresa)
        {
            await _context.ParticipanteEmpresas.AddAsync(participanteEmpresa);
            await _context.SaveChangesAsync();
        }
    }
}
