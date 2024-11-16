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
        private readonly IParticipanteEmpresaService _participanteEmpresaService;

        public HistoricoCampanhaService(IPostagemRepository postagemRepository,
            IHistoricoCampanhaRepository historicoCampanhaRepository, IMapper mapper,
            IParticipanteEmpresaService participanteEmpresaService)
        {
            _postagemRepository = postagemRepository;
            _historicoCampanhaRepository = historicoCampanhaRepository;
            _mapper = mapper;
            _participanteEmpresaService = participanteEmpresaService;
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

        public async Task<List<HistoricoCampanhaDto>> ObterHistoricoPorEmpresaAsync(int empresaId, string usuarioId)
        {
            var estaVinculado = await _participanteEmpresaService.VerificarSeUsuarioEstaAssociadoEmpresa(usuarioId, empresaId);
            if (!estaVinculado)
                throw new UnauthorizedAccessException("Você não tem permissão para acessar o histórico dessa empresa.");


            var historicos = await _historicoCampanhaRepository.ObterHistoricoPorEmpresaAsync(empresaId);
            Console.WriteLine($"Mapeando {historicos.Count} registros para HistoricoCampanhaDto");
            return _mapper.Map<List<HistoricoCampanhaDto>>(historicos);
        }

    }
}
