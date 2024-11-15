using AutoMapper;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;

namespace ProjetoDePost.Services.Implementations
{
    public class HistoricoCampanhaService : IHistoricoCampanhaService
    {
        private readonly IPostagemRepository _postagemRepository;
        private readonly IMapper _mapper;
        private readonly IHistoricoCampanhaRepository _historicoCampanhaRepository;

        public HistoricoCampanhaService(IPostagemRepository postagemRepository,
            IHistoricoCampanhaRepository historicoCampanhaRepository, IMapper mapper)
        {
            _postagemRepository = postagemRepository;
            _historicoCampanhaRepository = historicoCampanhaRepository;
            _mapper = mapper;
        }

        public async Task GuardarHistorico(Campanha campanha, string conteudoGerado = null)
        {
            var postagens = await _postagemRepository.BuscarPorCampanhaIdAsync(campanha.Id);
            var historico = new HistoricoCampanha
            {
                CampanhaId = campanha.Id,
                EmpresaId = campanha.EmpresaId,
                DataCriacao = DateTime.Now,
                ConteudoGerado = conteudoGerado ?? postagens.FirstOrDefault()?.ConteudoGerado,
                Aprovada = campanha.Aprovada,
                Ativa = campanha.Ativa,
                Nome = campanha.Nome,
                NomeCampanha = campanha.Nome,
                TemaPrincipal = campanha.TemaPrincipal
            };
           await _historicoCampanhaRepository.AdicionarHistoricoAsync(historico);
        }

        public async Task<List<HistoricoCampanhaDto>> ObterHistoricoPorEmpresaAsync(int empresaId)
        {
            var historicos = await _historicoCampanhaRepository.ObterHistoricoPorEmpresaAsync(empresaId);
            Console.WriteLine($"Mapeando {historicos.Count} registros para HistoricoCampanhaDto");
            return _mapper.Map<List<HistoricoCampanhaDto>>(historicos);
        }

    }
}
