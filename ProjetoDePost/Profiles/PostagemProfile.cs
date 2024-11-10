using AutoMapper;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Profiles
{
    public class PostagemProfile : Profile
    {
        public PostagemProfile() 
        {
            // Mapeando de Postagem para PostagemReadDto
            CreateMap<Postagem, PostagemReadDto>()
              .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.DataCriacao))
              .ForMember(dest => dest.CampanhaId, opt => opt.MapFrom(src => src.CampanhaId))
              .ForMember(Dest => Dest.ConteudoGerado, opt => opt.MapFrom(src => src.ConteudoGerado));
            // Mapeando de PostagemCreateDto para Postagem
            CreateMap<PostagemCreateDto, Postagem>()
              .ForMember(dest => dest.CampanhaId, opt => opt.MapFrom(src => src.CampanhaId));
            // Mapeando de PostagemUpdateDto para Postagem
            CreateMap<PostagemUpdateDto, Postagem>();

            CreateMap<Campanha, PostagemCreateDto>();
                
        }
    }
}
