﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Data.Repositories.Implementations;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Implementations;
using ProjetoDePost.Services.Interfaces;

namespace ProjetoDePost.Services
{
    /// <summary>
    /// Serviço que implementa as operações CRUD e lógicas de negócios relacionadas às campanhas.
    /// </summary>
    public class CampanhaService : ICampanhaService
    {
        private readonly ICampanhaRepository _campanhaRepository;
        private readonly IMapper _mapper;
        private readonly ISolicitacaoCampanhaRepository _solicitacaoCampanhaRepository;
        private readonly ProjetoDePostContext _context;
        private readonly IPostagemService _postagemService;
        private readonly IPostagemRepository _postagemRepository;
        private readonly ILogger<CampanhaService> _logger;
        private readonly IHistoricoCampanhaRepository _historicoCampanhaRepository;

        public CampanhaService(ICampanhaRepository campanhaRepository, 
            IMapper mapper, ISolicitacaoCampanhaRepository solicitacaoCampanhaRepository,
            ProjetoDePostContext context, IPostagemService postagemService, ILogger<CampanhaService> logger,
            IHistoricoCampanhaRepository historicoCampanhaRepository)
        {
            _campanhaRepository = campanhaRepository;
            _mapper = mapper;
            _solicitacaoCampanhaRepository = solicitacaoCampanhaRepository;
            _context = context;
            _postagemService = postagemService;
            _logger = logger;
            _historicoCampanhaRepository = historicoCampanhaRepository;
        }

        /// <summary>
        /// Cria uma nova campanha com base nos dados fornecidos.
        /// </summary>
        public async Task<Campanha> CriarCampanhaAsync(CampanhaCreateDto campanhaDto)
        {
            var campanha = _mapper.Map<Campanha>(campanhaDto);
            await _campanhaRepository.CriarAsync(campanha);
            return campanha;
        }

        /// <summary>
        /// Obtém uma campanha pelo seu ID.
        /// </summary>
        public async Task<CampanhaReadDto> ObterPorIdAsync(int id)
        {
            var campanha = await _campanhaRepository.BuscarPorIdAsync(id);
            return campanha != null ? _mapper.Map<CampanhaReadDto>(campanha) : null;
        }

        /// <summary>
        /// Obtém todas as campanhas de uma empresa específica.
        /// </summary>
        public async Task<IEnumerable<CampanhaReadDto>> ObterTodasPorEmpresaAsync(int empresaId)
        {
            var campanhas = await _campanhaRepository.ObterCampanhasPorEmpresaAsync(empresaId);
            return _mapper.Map<IEnumerable<CampanhaReadDto>>(campanhas);
        }

        /// <summary>
        /// Obtém campanhas vinculadas a um participante específico.
        /// </summary>
        public async Task<IEnumerable<CampanhaReadDto>> ObterVinculadasAoParticipanteAsync(int participanteId)
        {
            var campanhas = await _campanhaRepository.ObterCampanhasVinculadasAoParticipanteAsync(participanteId);
            return _mapper.Map<IEnumerable<CampanhaReadDto>>(campanhas);
        }

        /// <summary>
        /// Atualiza uma campanha existente com novos dados.
        /// </summary>
        public async Task AtualizarCampanhaAsync(int id, CampanhaUpdateDto campanhaDto)
        {
            var campanhaExistente = await _campanhaRepository.BuscarPorIdAsync(id);
            if (campanhaExistente == null)
                throw new KeyNotFoundException("Campanha não encontrada.");

            _mapper.Map(campanhaDto, campanhaExistente);
            await _campanhaRepository.AtualizarAsync(campanhaExistente);
        }

        /// <summary>
        /// Remove uma campanha com base no ID fornecido.
        /// </summary>
        public async Task RemoverCampanha(int id)
        {
            var campanha = await _campanhaRepository.BuscarPorIdAsync(id);
            if (campanha == null)
                throw new KeyNotFoundException("Campanha não encontrada.");

            await _campanhaRepository.DeletarAsync(id);
        }

        /// <summary>
        /// Aprova uma campanha, alterando seu status para aprovado.
        /// </summary>
        public async Task<Campanha> AceitarCampanha(int solicitacaoCampanhaId)
        {
            var solicitacao = await _solicitacaoCampanhaRepository.BuscarPorIdAsync(solicitacaoCampanhaId);
            if (solicitacao == null)
                throw new KeyNotFoundException("Solicitação de campanha não encontrada.");

            var novaCampanha = _mapper.Map<Campanha>(solicitacao);
            novaCampanha.Aprovada = true;
            
            await _campanhaRepository.CriarAsync(novaCampanha);
            try
            {


                var historicoCampanha = _mapper.Map<HistoricoCampanha>(novaCampanha);
                historicoCampanha.ConteudoGerado = string.Empty;
                historicoCampanha.Ativa = false;
                await _historicoCampanhaRepository.AdicionarHistoricoAsync(historicoCampanha);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
            }
            /*
            // Aqui eu estou verificando se a lista de postagens possui algum item.
            if (novaCampanha.Postagens.Any())
            {
                // Mapeando e registrando o histórico da campanha
                var historicoCampanha = _mapper.Map<HistoricoCampanha>(novaCampanha);
                historicoCampanha.ConteudoGerado = string.Empty;
                historicoCampanha.Ativa = false;

                // Salvando o histórico
                await _historicoCampanhaRepository.AdicionarHistoricoAsync(historicoCampanha);
            }
            else
            {
                // Tô tentando entender o porquê não está sendo passado.
                Console.WriteLine("A campanha não possui postagens associadas, o histórico não foi registrado.");
            }
            */

            await _solicitacaoCampanhaRepository.DeletarAsync(solicitacaoCampanhaId);
            
            await _context.SaveChangesAsync();
            
            return novaCampanha;
        }

        public async Task RecusarCampanha(int solicitacaoCampanhaId)
        {
            var solicitacao = await _solicitacaoCampanhaRepository.BuscarPorIdAsync(solicitacaoCampanhaId);
            
            if (solicitacao == null)
            {
                throw new KeyNotFoundException("Solicitação de campanha não encontrada.");
            }
            solicitacao.Aprovada = false;
            await _solicitacaoCampanhaRepository.AtualizarAsync(solicitacao);

            await _solicitacaoCampanhaRepository.DeletarAsync(solicitacaoCampanhaId);

            // Tentando salvar manualmente, para resolver BUGS.
            await _context.SaveChangesAsync();
        }

        public async Task<SolicitacaoCampanha> SolicitarCriacaoDeCampanha(CampanhaCreateDto campanhaDto)
        {
            var solicitacao = _mapper.Map<SolicitacaoCampanha>(campanhaDto);
            solicitacao.Aprovada = false;
            solicitacao.FrequenciaMaxima = 10;
            await _solicitacaoCampanhaRepository.CriarAsync(solicitacao);
            return solicitacao;
        }

        public async Task<IEnumerable<SolicitacaoCampanhaReadDto>> ObservarSolicitacoesAsync()
        {
            var solicitacoesPendentes = await _solicitacaoCampanhaRepository.ObterSolicitacoesPendentesAsync();
            var solicitacoesPendentesDto = _mapper.Map<IEnumerable<SolicitacaoCampanhaReadDto>>(solicitacoesPendentes);
            return solicitacoesPendentesDto;
        }

       
    }
}