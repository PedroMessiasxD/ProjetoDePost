using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Data.Repositories.Implementations;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Data.Validations;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Implementations;
using ProjetoDePost.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

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
        private readonly IPostagemRepository _postagemRepository;
        private readonly ILogger<CampanhaService> _logger;
        private readonly IHistoricoCampanhaService _historicoCampanhaService;
        private readonly IParticipanteEmpresaService _participanteEmpresaService;
        private readonly IValidator<CampanhaCreateDto> _validator;
        private readonly IParticipanteEmpresaRepository _participanteEmpresaRepository;

        public CampanhaService(ICampanhaRepository campanhaRepository,
            IMapper mapper, ISolicitacaoCampanhaRepository solicitacaoCampanhaRepository,
            ProjetoDePostContext context, ILogger<CampanhaService> logger,
             IHistoricoCampanhaService historicoCampanhaService, IParticipanteEmpresaService participanteEmpresaService,
             IValidator<CampanhaCreateDto> validator, IParticipanteEmpresaRepository participanteEmpresaRepository)
        {
            _campanhaRepository = campanhaRepository;
            _mapper = mapper;
            _solicitacaoCampanhaRepository = solicitacaoCampanhaRepository;
            _context = context;
            _logger = logger;
            _historicoCampanhaService = historicoCampanhaService;
            _participanteEmpresaService = participanteEmpresaService;
            _validator = validator;
            _participanteEmpresaRepository = participanteEmpresaRepository;
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
        public async Task<IEnumerable<CampanhaReadDto>> ObterTodasPorEmpresaAsync(int empresaId, string usuarioId)
        {
            var usuarioVinculado = await _participanteEmpresaRepository.BuscarPorUsuarioIdEEmpresaIdAsync(usuarioId, empresaId);

            if (usuarioVinculado == null)
                throw new UnauthorizedAccessException("Você não está vinculado a esta empresa.");

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
        public async Task<Campanha> AceitarCampanha(int solicitacaoCampanhaId, string usuarioId)
        {
            var solicitacao = await _solicitacaoCampanhaRepository.BuscarPorIdAsync(solicitacaoCampanhaId);
            if (solicitacao == null)
                throw new KeyNotFoundException("Solicitação de campanha não encontrada.");

            var isAdminEmpresarial = await _participanteEmpresaService.VerificarSeUsuarioEhAdminEmpresarial(usuarioId, solicitacao.EmpresaId);
            if (!isAdminEmpresarial)
                throw new UnauthorizedAccessException("Você não tem permissão para aceitar solicitações dessa empresa.");

            var novaCampanha = _mapper.Map<Campanha>(solicitacao);
            novaCampanha.Aprovada = true;
            novaCampanha.Ativa = false;

            var validator = new CampanhaCreateDtoValidator();
            var validationResult = await validator.ValidateAsync(new CampanhaCreateDto
            {
                Nome = novaCampanha.Nome,
                EmpresaId = novaCampanha.EmpresaId,
                TemaPrincipal = novaCampanha.TemaPrincipal,
                Frequencia = novaCampanha.Frequencia,
                Descricao = novaCampanha.Descricao
            });

            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var participantes = await _participanteEmpresaRepository.ObterParticipantesPorEmpresaIdAsync(solicitacao.EmpresaId);
            novaCampanha.Participantes = participantes.Select(p => new ParticipanteEmpresa
            {
                UsuarioId = p.UsuarioId,
                EmpresaId = p.EmpresaId,
                CampanhaId = novaCampanha.Id,
                Papel = p.Papel,
                DataEntrada = DateTime.UtcNow
            }).ToList();
            await _campanhaRepository.CriarAsync(novaCampanha);

            await _historicoCampanhaService.GuardarHistorico(novaCampanha);

            await _solicitacaoCampanhaRepository.DeletarAsync(solicitacaoCampanhaId);

            await _context.SaveChangesAsync();

            return novaCampanha;
        }

        public async Task RecusarCampanha(int solicitacaoCampanhaId, string usuarioId)
        {
            var solicitacao = await _solicitacaoCampanhaRepository.BuscarPorIdAsync(solicitacaoCampanhaId);

            if (solicitacao == null)
            {
                throw new KeyNotFoundException("Solicitação de campanha não encontrada.");
            }

            var isAdminEmpresarial = await _participanteEmpresaService.VerificarSeUsuarioEhAdminEmpresarial(usuarioId, solicitacao.EmpresaId);
            if (!isAdminEmpresarial)
                throw new UnauthorizedAccessException("Você não tem permissão para recusar solicitações dessa empresa.");

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

        public async Task<IEnumerable<SolicitacaoCampanhaReadDto>> ObservarSolicitacoesAsync(string usuarioId, int empresaId)
        {
            var isAdminEmpresarial = await _participanteEmpresaService.VerificarSeUsuarioEhAdminEmpresarial(usuarioId, empresaId);
            if (!isAdminEmpresarial)
                throw new UnauthorizedAccessException("Você não tem permissão para observar as solicitações dessa empresa.");

            var solicitacoesPendentes = await _solicitacaoCampanhaRepository.ObterSolicitacoesPendentesAsync();
            var solicitacoesPendentesDto = _mapper.Map<IEnumerable<SolicitacaoCampanhaReadDto>>(solicitacoesPendentes);
            return solicitacoesPendentesDto;
        }

        public async Task<Campanha> AbandonarCampanha(int campanhaId)
        {
            
            var campanha = await _campanhaRepository.BuscarPorIdAsync(campanhaId);
            if (campanha == null)
                throw new KeyNotFoundException("Campanha não encontrada.");
            if (campanha.Ativa == false)
            {
                throw new Exception("Campanha já inativa.");
            }
            campanha.Ativa = false;
            await _campanhaRepository.AtualizarAsync(campanha);
            
            await _historicoCampanhaService.GuardarHistorico(campanha);

            return campanha;


        }
    }
}