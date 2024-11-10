using AutoMapper;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Profiles
{
    public class HistoricoCampanhaProfile : Profile
    {
        public HistoricoCampanhaProfile()
        {
            // Mapeamento de HistoricoCampanhaDto para HistoricoCampanha
            CreateMap<HistoricoCampanhaDto, HistoricoCampanha>()
                .ForMember(dest => dest.ConteudoGerado, opt => opt.MapFrom(src => src.ConteudoGerado))
                .ForMember(dest => dest.Ativa, opt => opt.MapFrom(src => src.Ativa));

            CreateMap<HistoricoCampanha, HistoricoCampanhaDto>()
                .ForMember(dest => dest.ConteudoGerado, opt => opt.MapFrom(src => src.ConteudoGerado))
                .ForMember(dest => dest.Ativa, opt => opt.MapFrom(src => src.Ativa));
        }
    }
}
