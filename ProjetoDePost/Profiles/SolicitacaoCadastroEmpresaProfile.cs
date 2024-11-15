using AutoMapper;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Profiles
{
    public class SolicitacaoCadastroEmpresaProfile : Profile
    {
        public SolicitacaoCadastroEmpresaProfile()
        {
            CreateMap<SolicitacaoCadastroEmpresaDto, SolicitacaoCadastroEmpresa>();

            CreateMap<SolicitacaoCadastroEmpresa, SolicitacaoCadastroEmpresaRespostaDto>()
                .ForMember(dest => dest.SolicitacaoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Mensagem, opt => opt.MapFrom(src => "Solicitação pendente de aprovação."));
        }
    }
}
