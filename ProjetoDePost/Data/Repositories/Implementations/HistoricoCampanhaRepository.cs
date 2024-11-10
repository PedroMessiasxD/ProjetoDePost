using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data.Repositories.Implementations.Generic;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;

namespace ProjetoDePost.Data.Repositories.Implementations
{
    public class HistoricoCampanhaRepository : GenericRepository<HistoricoCampanha>, IHistoricoCampanhaRepository
    {
        private readonly ProjetoDePostContext _context;

        public HistoricoCampanhaRepository(ProjetoDePostContext context) : base(context) 
        {
            _context = context;
        }

        public async Task AdicionarHistoricoAsync(HistoricoCampanha historico)
        {
            await _context.AddAsync(historico);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HistoricoCampanha>> ObterHistoricoPorEmpresaAsync(int empresaId)
        {
             var historicos = await _context.HistoricoCampanhas
                .Where(h => h.EmpresaId == empresaId)
                .Include(h => h.Campanha)
                .ToListAsync();
            
            Console.WriteLine($"Encontrados {historicos.Count} registros de histórico para EmpresaId {empresaId}");
            return historicos;
        }

        public async Task AtualizarHistoricoAsync(HistoricoCampanha historicoCampanha)
        {
            var existingHistorico = await _context.HistoricoCampanhas
                .FirstOrDefaultAsync(h => h.CampanhaId == historicoCampanha.CampanhaId);

            if (existingHistorico != null)
            {
                existingHistorico.ConteudoGerado = historicoCampanha.ConteudoGerado;
                existingHistorico.Ativa = historicoCampanha.Ativa;
                existingHistorico.DataCriacao = DateTime.Now;

                _context.HistoricoCampanhas.Update(existingHistorico);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Se não encontrar um histórico existente, cria um novo
                await AdicionarHistoricoAsync(historicoCampanha);
            }
        }
    }
}
