using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    /// Serviço para operações específicas relacionadas a ParticipanteEmpresa.
    /// </summary>

    public class ParticipanteEmpresaService : IParticipanteEmpresaService
    {
        private readonly IParticipanteEmpresaRepository _participanteEmpresaRepository;
        private readonly IMapper _mapper;

        public ParticipanteEmpresaService(IParticipanteEmpresaRepository participanteEmpresaRepository, IMapper mapper)
        {
            _participanteEmpresaRepository = participanteEmpresaRepository;
            _mapper = mapper;
            
        }
        /// <summary>
        /// Obtém a participação de um usuário específico em uma empresa específica.
        /// </summary>
        public async Task<bool> UsuarioEhAdminEmpresaAsync(string usuarioId, int empresaId)
        {
            var participante = await _participanteEmpresaRepository.ObterAdminPorEmpresaIdAsync(empresaId, usuarioId);
            return participante != null && participante.UsuarioId == usuarioId;
        }

        public async Task<IEnumerable<ParticipanteEmpresa>> ObterParticipantesPorEmpresaAsync(int empresaId)
        {
            return await _participanteEmpresaRepository.ObterParticipantesPorEmpresaIdAsync(empresaId);
        }

        /// <summary>
        /// Adiciona um usuário à empresa com o papel de AdminEmpresarial.
        /// </summary>
        public async Task AdicionarUsuarioNaEmpresaAsync(ParticipanteEmpresaCreateDto participanteEmpresaCreateDto)
        {
            var participante = new ParticipanteEmpresa
            {
                UsuarioId = participanteEmpresaCreateDto.UsuarioId,
                EmpresaId = participanteEmpresaCreateDto.EmpresaId,
                Papel = participanteEmpresaCreateDto.Papel,
                DataEntrada = DateTime.Now
            };

            await _participanteEmpresaRepository.CriarAsync(participante);
        }

        /// <summary>
        /// Verifica se um usuário é um AdminEmpresarial de uma empresa específica.
        /// </summary>
        public async Task<bool> VerificarSeUsuarioEhAdminEmpresarial(string usuarioId, int empresaId)
        {
            var participante = await _participanteEmpresaRepository.BuscarPorCondicaoAsync(p =>
                p.UsuarioId == usuarioId && p.EmpresaId == empresaId && p.Papel == "AdminEmpresarial");

            return participante != null;
        }

        /// <summary>
        /// Associa um participante a uma campanha.
        /// </summary>
        public async Task<ParticipanteEmpresaReadDto> AdicionarParticipanteACampanhaAsync(ParticipanteEmpresaCreateDto participanteEmpresaCreateDto, int campanhaId)
        {
            // Adiciona a campanha ao DTO
            participanteEmpresaCreateDto.CampanhaId = campanhaId;

            var participanteEmpresa = _mapper.Map<ParticipanteEmpresa>(participanteEmpresaCreateDto);

            await _participanteEmpresaRepository.CriarAsync(participanteEmpresa);

            var participanteEmpresaReadDto = _mapper.Map<ParticipanteEmpresaReadDto>(participanteEmpresa);

            // Retorna o DTO criado
            return participanteEmpresaReadDto;

        }
    }



}
