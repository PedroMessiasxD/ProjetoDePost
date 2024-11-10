using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Data.Repositories.Implementations;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;

namespace ProjetoDePost.Services.Implementations
{
    /// <summary>
    /// Serviço para gerenciamento de postagens automáticas, utilizando o OpenAiService para gerar conteúdo.
    /// Baseia-se nos dados de uma campanha (descrição, tema e frequência) para criar postagens com conteúdo dinâmico,
    /// respeitando o limite diário configurado pela campanha.
    /// </summary>
    public class PostagemService : IPostagemService
    {
        private readonly IPostagemRepository _postagemRepository;
        
        private readonly ICampanhaRepository _campanhaRepository;
        private readonly IMapper _mapper;
        private readonly IOpenAiService _openAiService;
        private readonly ILogger<PostagemService> _logger;
        private readonly ProjetoDePostContext _context;
        private readonly ICampanhaService _campanhaService;
        private readonly IHistoricoCampanhaRepository _historicoCampanhaRepository;

        public PostagemService(IPostagemRepository postagemRepository, IMapper mapper, IOpenAiService openAiService, 
            ICampanhaRepository campanhaRepository, ILogger<PostagemService> logger,
            ProjetoDePostContext context, IHistoricoCampanhaRepository historicoCampanhaRepository)
        {
            _postagemRepository = postagemRepository;
            _mapper = mapper;
            _openAiService = openAiService;
            _campanhaRepository = campanhaRepository;
            _logger = logger;
            _context = context;
            _historicoCampanhaRepository = historicoCampanhaRepository;
        }
        
        public async Task<IEnumerable<PostagemReadDto>> BuscarPorCampanhaIdAsync(int campanhaId)
        {
            var postagens = await _postagemRepository.BuscarPorCampanhaIdAsync(campanhaId);
            return _mapper.Map<IEnumerable<PostagemReadDto>>(postagens);
        }

        public async Task<PostagemReadDto> CriarPostagemAsync(PostagemCreateDto postagemCreateDto)
        {
            var campanha = await _campanhaRepository.BuscarPorIdAsync(postagemCreateDto.CampanhaId);

            if (campanha == null)
            {
                throw new KeyNotFoundException("Campanha não encontrada");
            }

            // Verifica se as configurações de frequência da campanha são válidas antes de continuar.
            if (campanha.FrequenciaMaxima <= 0 || campanha.Frequencia <= 0)
            {
                _logger.LogError($"Configurações de frequência incorretas: FrequenciaMaxima = {campanha.FrequenciaMaxima}, Frequencia = {campanha.Frequencia}");
                throw new Exception("Configurações de frequência da campanha estão incorretas.");
            }

            // Verifica se o limite de postagens foi atingido.
            if (!await PodeGerarPostagemAsync(postagemCreateDto.CampanhaId, campanha.FrequenciaMaxima))
            {
                throw new Exception("Limite de postagens para hoje atingido.");
            }

            // Verificar se a descrição, tema e frequência estão válidos antes de gerar conteúdo
            if (string.IsNullOrWhiteSpace(campanha.Descricao) || string.IsNullOrWhiteSpace(campanha.TemaPrincipal) || campanha.Frequencia <= 0)
            {
                throw new Exception("A descrição, o tema ou a frequência da campanha estão inválidos.");
            }

            // Tenta gerar o conteúdo da postagem a partir da API da OpenAI.
            string conteudoGerado;
            try
            {
                conteudoGerado = await _openAiService.GerarIdeiasDePostagem(
                    campanha.Descricao, campanha.TemaPrincipal, campanha.Frequencia);

                // Se não houver conteúdo gerado, lança uma exceção.
                if (string.IsNullOrWhiteSpace(conteudoGerado))
                {
                    throw new Exception("Não foi possível gerar o conteúdo da postagem.");
                }
            }
            catch (Exception ex)
            {
                // Tratar exceção da API
                _logger.LogError($"Erro ao gerar conteúdo da postagem: {ex.Message}");
                throw new Exception("Erro ao gerar conteúdo da postagem através da API.", ex);
            }           
            // Criação da postagem.
            var postagem = new Postagem
            {
                CampanhaId = campanha.Id,
                Postado = true,
                DataCriacao = DateTime.Now,
                ConteudoGerado = conteudoGerado
            };
            await _postagemRepository.CriarAsync(postagem);

            var historicoCampanhas = await _historicoCampanhaRepository.ObterHistoricoPorEmpresaAsync(campanha.Id);
            var historicoExistente = historicoCampanhas.FirstOrDefault(h => h.CampanhaId == campanha.Id);
            if (historicoExistente != null)
            {
                historicoExistente.ConteudoGerado = conteudoGerado;
                historicoExistente.NomeCampanha = campanha.Nome;
                historicoExistente.TemaPrincipal = campanha.TemaPrincipal;
                historicoExistente.Ativa = true;
                await _historicoCampanhaRepository.AtualizarHistoricoAsync(historicoExistente);
            }
            else
            {
                // Caso o histórico não exista, cria um novo histórico
                var novoHistorico = _mapper.Map<HistoricoCampanha>(campanha);  // Mapeia para a entidade HistoricoCampanha
                novoHistorico.ConteudoGerado = conteudoGerado;
                novoHistorico.NomeCampanha = campanha.Nome; // Inclui o nome da campanha
                novoHistorico.TemaPrincipal = campanha.TemaPrincipal; // Inclui o tema principal

                await _historicoCampanhaRepository.AdicionarHistoricoAsync(novoHistorico); 
            }

            _logger.LogInformation($"Postagem {postagem.Id} criada com sucesso com conteúdo gerado: {conteudoGerado}");
           
            return _mapper.Map<PostagemReadDto>(postagem);
          
            /*return new PostagemReadDto
            {
                Id = postagem.Id,
                CampanhaId = campanha.Id,
                DataCriacao = postagem.DataCriacao,
                ConteudoGerado = conteudoGerado
            };
            */
        }


        private async Task<bool> PodeGerarPostagemAsync(int campanhaId, int frequenciaMaxima)
        {
            var postagensHoje = await _postagemRepository.ObterPostagensPorCampanhaEDataAsync(campanhaId, DateTime.Today);

            return postagensHoje.Count < frequenciaMaxima;
        }

        public async Task<PostagemReadDto> ObterPostagemPorIdAsync(int id)
        {
            var postagem = await _postagemRepository.BuscarPorIdAsync(id);
            if (postagem == null)
            {
                throw new KeyNotFoundException("Postagem não encontrada.");
            }

            var postagemReadDto = _mapper.Map<PostagemReadDto>(postagem);

            if (string.IsNullOrWhiteSpace(postagemReadDto.ConteudoGerado)) 
            {
                postagemReadDto.ConteudoGerado = "Conteúdo não gerado";               
            }

            return postagemReadDto;
        }

        public async Task DeletarPostagemAsync(int id)
        {
            var postagem = await _postagemRepository.BuscarPorIdAsync(id);
            if (postagem == null)
            {
                throw new KeyNotFoundException("Postagem não encontrada.");
            }

            await _postagemRepository.DeletarAsync(id);
        }

        public async Task<IEnumerable<PostagemReadDto>> ObterPostagensPorEmpresaAsync(int empresaId)
        {
            // Busca campanhas pela empresa
            var campanhasDaEmpresa = await _campanhaRepository.ObterCampanhasPorEmpresaAsync(empresaId);
            var postagensDaEmpresa = new List<Postagem>();

            foreach (var campanha in campanhasDaEmpresa)
            {
                var postagens = await _postagemRepository.BuscarPorCampanhaIdAsync(campanha.Id);

                foreach (var postagem in postagens)
                {
                    postagem.Campanha = campanha;
                    postagensDaEmpresa.Add(postagem);
                }
            }

            return _mapper.Map<IEnumerable<PostagemReadDto>>(postagensDaEmpresa);
        }

        public async Task<List<Campanha>> ObterCampanhasPorEmpresaAsync(int empresaId)
        {
            return await _context.Campanhas
                .Where(c => c.EmpresaId == empresaId)
                .Include(c => c.Postagens) // Inclui as postagens para o histórico
                .ToListAsync();
        }

    }
}
