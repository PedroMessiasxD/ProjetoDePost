using AutoMapper;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Data.Repositories.Interfaces.Generic;
using ProjetoDePost.Exceptions;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Interfaces;

namespace ProjetoDePost.Services.Implementations
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public EmpresaService(IEmpresaRepository empresaRepository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _empresaRepository = empresaRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<Empresa> SolicitarCadastroEmpresaAsync(EmpresaCreateDto empresaDto, string usuarioId)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new NotFoundException("Usuário não encontrado.");
            }

            var empresa = new Empresa
            {
                Nome = empresaDto.Nome,
                Descricao = empresaDto.Descricao,
                SetorAtuacao = empresaDto.SetorAtuacao,
                LinksRedesSociais = empresaDto.LinksRedesSociais,
                AdministradorId = usuarioId 
            };
            await _empresaRepository.CriarAsync(empresa);
            return empresa;
        }

        public async Task<IEnumerable<Empresa>> ListarSolicitacoesAsync(string usuarioId)
        {
            
            return await _empresaRepository.BuscarTodosAsync(); // Ajuste conforme necessário
        }

        public async Task<IEnumerable<EmpresaReadDto>> ListarTodasAsEmpresasAsync()
        {
            var empresas = await _empresaRepository
            .BuscarTodosAsync(incluirParticipantes: true);
            return _mapper.Map<IEnumerable<EmpresaReadDto>>(empresas);
        }
    }
}
