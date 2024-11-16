using AutoMapper;
using FluentValidation;
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

    public class ParticipanteEmpresaService : IParticipanteEmpresaService
    {
        private readonly IParticipanteEmpresaRepository _participanteEmpresaRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ParticipanteEmpresaCreateDto> _validator;

        public ParticipanteEmpresaService(IParticipanteEmpresaRepository participanteEmpresaRepository, IMapper mapper, IValidator<ParticipanteEmpresaCreateDto> validator)
        {
            _participanteEmpresaRepository = participanteEmpresaRepository;
            _mapper = mapper;
            _validator = validator;
        }
  
        public async Task<bool> UsuarioEhAdminEmpresaAsync(string usuarioId, int empresaId)
        {
            var participante = await _participanteEmpresaRepository.ObterAdminPorEmpresaIdAsync(empresaId, usuarioId);
            return participante != null && participante.UsuarioId == usuarioId;
        }

        public async Task<IEnumerable<ParticipanteEmpresa>> ObterParticipantesPorEmpresaAsync(int empresaId)
        {
            return await _participanteEmpresaRepository.ObterParticipantesPorEmpresaIdAsync(empresaId);
        }

        public async Task AdicionarUsuarioNaEmpresaAsync(ParticipanteEmpresaCreateDto participanteEmpresaCreateDto)
        {
           
            var validationResult = await _validator.ValidateAsync(participanteEmpresaCreateDto);
            if (!validationResult.IsValid)
            {
                
                throw new ValidationException(validationResult.Errors);
            }

            var jaAssociado = await VerificarSeUsuarioEstaAssociadoEmpresa(participanteEmpresaCreateDto.UsuarioId, participanteEmpresaCreateDto.EmpresaId);

            if (jaAssociado)
                throw new InvalidOperationException("Usuario já está associado à empresa.");

            var participante = new ParticipanteEmpresa
            {
                UsuarioId = participanteEmpresaCreateDto.UsuarioId,
                EmpresaId = participanteEmpresaCreateDto.EmpresaId,
                Papel = participanteEmpresaCreateDto.Papel,
                DataEntrada = DateTime.Now
            };

            await _participanteEmpresaRepository.CriarAsync(participante);
        }

        public async Task<bool> VerificarSeUsuarioEhAdminEmpresarial(string usuarioId, int empresaId)
        {
            var participante = await _participanteEmpresaRepository.BuscarPorCondicaoAsync(p =>
                p.UsuarioId == usuarioId && p.EmpresaId == empresaId && p.Papel == "AdminEmpresarial");

            return participante != null;
        }

     
        public async Task<ParticipanteEmpresaReadDto> AdicionarParticipanteACampanhaAsync(ParticipanteEmpresaCreateDto participanteEmpresaCreateDto, int campanhaId)
        {
            
            participanteEmpresaCreateDto.CampanhaId = campanhaId;

            var participanteEmpresa = _mapper.Map<ParticipanteEmpresa>(participanteEmpresaCreateDto);

            await _participanteEmpresaRepository.CriarAsync(participanteEmpresa);

            var participanteEmpresaReadDto = _mapper.Map<ParticipanteEmpresaReadDto>(participanteEmpresa);

            return participanteEmpresaReadDto;

        }


        public async Task<bool> VerificarSeUsuarioEstaAssociadoEmpresa(string usuarioId, int empresaId)
        {
            var participante = await _participanteEmpresaRepository.BuscarPorUsuarioIdEEmpresaIdAsync(usuarioId, empresaId);
            return participante != null;
        }
    }



}
