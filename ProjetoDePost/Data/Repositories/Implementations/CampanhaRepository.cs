using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Data.Repositories.Implementations.Generic;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;
using System.Runtime.CompilerServices;

namespace ProjetoDePost.Data.Repositories.Implementations
{
    public class CampanhaRepository : GenericRepository<Campanha>, ICampanhaRepository
    {
        private readonly ProjetoDePostContext _context;

        public CampanhaRepository(ProjetoDePostContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Campanha> BuscarPorIdAsync(int id)
        {
            return await _context.Campanhas
                .Include(c => c.Postagens)
                .Include(c => c.Participantes)
                    .ThenInclude(p => p.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Campanha>> ObterCampanhasPorEmpresaAsync(int empresaId)
        {
            var campanhas = await _context.Campanhas
                 .Where(c => c.EmpresaId == empresaId)
                 .Include(c => c.Participantes)          
                    .ThenInclude(p => p.Usuario)
                 .Include(c => c.Postagens)
                 .ToListAsync();

            // Verificação temporária
            foreach (var campanha in campanhas)
            {
                Console.WriteLine($"Campanha {campanha.Id} tem {campanha.Participantes.Count} participantes");
                foreach (var participante in campanha.Participantes)
                {
                    Console.WriteLine($"Participante: {participante.Id}, UsuarioNome: {participante.Usuario?.Nome}");
                }
            }

            return campanhas;
        }

        public async Task<IEnumerable<Campanha>> ObterCampanhasVinculadasAoParticipanteAsync(int participanteId)
        {
            return await _context.Campanhas
                         .Include(c => c.Participantes)
                          .ThenInclude(p => p.Usuario)
                         .Where(c => c.Participantes.Any(p => p.Id == participanteId))
                         .ToListAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var campanha = await _context.Campanhas.FindAsync(id);
            if (campanha != null)
            {
                _context.Campanhas.Remove(campanha);
                await _context.SaveChangesAsync();
            }
        }
    }
}
