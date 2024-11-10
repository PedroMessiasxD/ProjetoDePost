using AutoMapper;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Profiles
{
    public class ParticipanteEmpresaProfile : Profile
    {
        public ParticipanteEmpresaProfile()
        {
            // Mapeamento para leitura (Read)
            CreateMap<ParticipanteEmpresa, ParticipanteEmpresaReadDto>()
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.Papel, opt => opt.MapFrom(src => src.Papel))
                .ForMember(dest => dest.DataEntrada, opt => opt.MapFrom(src => src.DataEntrada))
                .ForMember(dest => dest.CampanhaId, opt => opt.MapFrom(src => src.CampanhaId));
            // Mapeamento para criação (Create)
            CreateMap<ParticipanteEmpresaCreateDto, ParticipanteEmpresa>()
                .ForMember(dest => dest.Papel, opt => opt.MapFrom(src => src.Papel))
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.Papel, opt => opt.MapFrom(src => src.Papel));
            // Mapeamento para atualização (Update)
            CreateMap<ParticipanteEmpresaUpdateDto, ParticipanteEmpresa>()
                .ForMember(dest => dest.Papel, opt => opt.MapFrom(src => src.Papel));
        }
    }
}
