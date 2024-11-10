using AutoMapper;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Profiles
{
    public class EmpresaProfile : Profile
    {
        public EmpresaProfile()
        {
            CreateMap<EmpresaCreateDto, Empresa>();
            CreateMap<Empresa, EmpresaReadDto>()
                .ForMember(dest => dest.Participantes, opt => opt.MapFrom(src => src.Participantes));
            CreateMap<ParticipanteEmpresa, ParticipanteEmpresaReadDto>()
                .ForMember(dest => dest.UsuarioNome, opt => opt.MapFrom(src => src.Usuario.Nome));
        }
    }
}
