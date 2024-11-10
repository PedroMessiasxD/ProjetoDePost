using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data.Repositories.Implementations.Generic;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data.Repositories.Implementations
{
    public class EmpresaRepository : GenericRepository<Empresa>, IEmpresaRepository
    {
        private readonly ProjetoDePostContext _context;
        
        public EmpresaRepository(ProjetoDePostContext context) : base(context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Empresa> ObterPorIdAsync(int empresaId)
        {
            return await _context.Empresas
               .Include(e => e.Participantes) 
               .ThenInclude(p => p.Usuario)
               .FirstOrDefaultAsync(e => e.Id == empresaId);
        }

        public async Task<List<Empresa>> ListarTodasAsEmpresasAsync()
        {
            return await _context.Empresas
                .Include(e => e.Participantes) 
                .ThenInclude(p => p.Usuario)
                .ToListAsync();
        }

        public async Task<IEnumerable<Empresa>> BuscarTodosAsync(bool incluirParticipantes = false)
        {
            var query = _context.Empresas.AsQueryable();

            if (incluirParticipantes)
            {
                query = query.Include(e => e.Participantes)
                    .ThenInclude(p => p.Usuario);
            }

            return await query.ToListAsync();
        }
    }
}
