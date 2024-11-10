using AutoMapper;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;

namespace ProjetoDePost.Services.Implementations
{
    public class HistoricoCampanhaService : IHistoricoCampanhaService
    {
        private readonly ICampanhaService _campanhaService;
        private readonly IPostagemService _postagemService;
        private readonly IMapper _mapper;     
        private readonly IHistoricoCampanhaRepository _historicoCampanhaRepository;

        public HistoricoCampanhaService(ICampanhaService campanhaService, IPostagemService postagemService,
            IHistoricoCampanhaRepository historicoCampanhaRepository, IMapper mapper)
        {
            _campanhaService = campanhaService;
            _postagemService = postagemService;
            _historicoCampanhaRepository = historicoCampanhaRepository;
            _mapper = mapper;
        }

        public async Task GuardarHistorico(Campanha campanha)
        {
            var postagens = await _postagemService.BuscarPorCampanhaIdAsync(campanha.Id);

            if (campanha.Aprovada && campanha.Ativa)
            {
                var historico = new HistoricoCampanha
                {
                    CampanhaId = campanha.Id,
                    EmpresaId = campanha.EmpresaId,
                    DataCriacao = DateTime.Now,
                    ConteudoGerado = postagens.FirstOrDefault()?.ConteudoGerado,
                    Aprovada = campanha.Aprovada,
                    Ativa = campanha.Ativa
                };

                await _historicoCampanhaRepository.AdicionarHistoricoAsync(historico);
            }
        }

        public async Task<List<HistoricoCampanhaDto>> ObterHistoricoPorEmpresaAsync(int empresaId)
        {
            var historicos = await _historicoCampanhaRepository.ObterHistoricoPorEmpresaAsync(empresaId);
            Console.WriteLine($"Mapeando {historicos.Count} registros para HistoricoCampanhaDto");
            return _mapper.Map<List<HistoricoCampanhaDto>>(historicos);
        }

    }
}
