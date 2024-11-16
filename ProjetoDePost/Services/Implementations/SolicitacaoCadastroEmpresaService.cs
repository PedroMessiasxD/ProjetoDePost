using ProjetoDePost.Data;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ProjetoDePost.Services.Implementations
{
    public class SolicitacaoCadastroEmpresaService : ISolicitacaoCadastroEmpresaService
    {
        private readonly ProjetoDePostContext _context;
        private readonly IMapper _mapper;
        private readonly IEmpresaService _empresaService;

        public SolicitacaoCadastroEmpresaService(ProjetoDePostContext context, IMapper mapper, IEmpresaService empresaService)
        {
            _context = context;
            _mapper = mapper;
            _empresaService = empresaService;
        }

        public async Task<SolicitacaoCadastroEmpresaRespostaDto> CriarSolicitacaoAsync(SolicitacaoCadastroEmpresaDto dto, string usuarioId)
        {
            var solicitacao = _mapper.Map<SolicitacaoCadastroEmpresa>(dto);
            solicitacao.Status = "Pendente";
            solicitacao.DataSolicitacao = DateTime.Now;
            solicitacao.UsuarioId = usuarioId;
           

            _context.SolicitacoesCadastroEmpresa.Add(solicitacao);
            await _context.SaveChangesAsync();

            return new SolicitacaoCadastroEmpresaRespostaDto
            {
                SolicitacaoId = solicitacao.Id,
                Status = solicitacao.Status,
                NomeAdministrador = solicitacao.NomeAdministrador,
                EmailAdministrador = solicitacao.EmailAdministrador,
                Mensagem = "Solicitação de cadastro criada com sucesso."
            };
        }

        public async Task<IEnumerable<SolicitacaoCadastroEmpresaRespostaDto>> ObterSolicitacoesPendentesAsync()
        {
            var solicitacoes = await _context.SolicitacoesCadastroEmpresa
                .Where(s => s.Status == "Pendente")
                .ToListAsync();

            return _mapper.Map<IEnumerable<SolicitacaoCadastroEmpresaRespostaDto>>(solicitacoes);
        }

        public async Task<bool> AprovarSolicitacaoAsync(int solicitacaoId)
        {
            var solicitacao = await _context.SolicitacoesCadastroEmpresa.FindAsync(solicitacaoId);
            if (solicitacao == null || solicitacao.Status != "Pendente")
                return false;

            solicitacao.Status = "Aprovada";
            await _context.SaveChangesAsync();

            
            var empresaDto = new EmpresaCreateDto
            {
                Nome = solicitacao.NomeEmpresa,
                Descricao = solicitacao.DescricaoEmpresa,
                SetorAtuacao = solicitacao.SetorAtuacao,
                LinksRedesSociais = solicitacao.LinkRedeSocial
            };

            await _empresaService.SolicitarCadastroEmpresaAsync(empresaDto, solicitacao.UsuarioId);
            return true;
        }

        public async Task<bool> RecusarSolicitacaoAsync(int solicitacaoId)
        {
            var solicitacao = await _context.SolicitacoesCadastroEmpresa.FindAsync(solicitacaoId);
            if (solicitacao == null || solicitacao.Status != "Pendente")
                return false;

            solicitacao.Status = "Recusada";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
