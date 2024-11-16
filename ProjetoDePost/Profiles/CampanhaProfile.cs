using AutoMapper;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Profiles
{
    public class CampanhaProfile : Profile
    {
        public CampanhaProfile()
        {
            CreateMap<Campanha, CampanhaReadDto>()
              .ForMember(dest => dest.Participantes, opt => opt.MapFrom(src => src.Participantes))
              .ForMember(dest => dest.TemaPrincipal, opt => opt.MapFrom(src => src.TemaPrincipal));

            // Mapear CampanhaCreateDto para Campanha
            CreateMap<CampanhaCreateDto, Campanha>()
               .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.TemaPrincipal, opt => opt.MapFrom(src => src.TemaPrincipal))
                .ForMember(dest => dest.Frequencia, opt => opt.MapFrom(src => src.Frequencia))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
               
            // Mapeamento CampanhaCreateDto para SolicitacaoCampanha
            CreateMap<CampanhaCreateDto, SolicitacaoCampanha>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.TemaPrincipal, opt => opt.MapFrom(src => src.TemaPrincipal))
                .ForMember(dest => dest.Frequencia, opt => opt.MapFrom(src => src.Frequencia))
                .ForMember(dest => dest.FrequenciaMaxima, opt => opt.MapFrom(src => 10))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.Aprovada, opt => opt.MapFrom(src => false));

            // Mapear SolicitacaoCampanha para Campanha     
            CreateMap<SolicitacaoCampanha, Campanha>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Postagens, opt => opt.Ignore())
                .ForMember(dest => dest.Participantes, opt => opt.Ignore())
                .ForMember(dest => dest.Aprovada, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Ativa, opt => opt.MapFrom(src => true));

            // Mapear SolicitacaoCampanhaCreateDto para SolicitacaoCampanha
            CreateMap<SolicitacaoCampanhaCreateDto, SolicitacaoCampanha>();

            // Mapear SolicitacaoCampanha para SolicitacaoCampanhaCreateDto
            CreateMap<SolicitacaoCampanha, SolicitacaoCampanhaCreateDto>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.TemaPrincipal, opt => opt.MapFrom(src => src.TemaPrincipal))
                .ForMember(dest => dest.Frequencia, opt => opt.MapFrom(src => src.Frequencia))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<SolicitacaoCampanha, SolicitacaoCampanhaReadDto>();
            CreateMap<CampanhaUpdateDto, Campanha>();

            // Mapear ParticipanteEmpresa para ParticipanteEmpresaReadDto
            CreateMap<ParticipanteEmpresa, ParticipanteEmpresaReadDto>()
                .ForMember(dest => dest.UsuarioNome, opt => opt.MapFrom(src => src.Usuario.Nome));

            CreateMap<Campanha, HistoricoCampanha>()
             .ForMember(dest => dest.CampanhaId, opt => opt.MapFrom(src => src.Id))  // Associa a campanha
             .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom(src => src.EmpresaId)) // Associa à empresa
             .ForMember(dest => dest.NomeCampanha, opt => opt.MapFrom(src => src.Nome))
             .ForMember(dest => dest.TemaPrincipal, opt => opt.MapFrom(src => src.TemaPrincipal))
             .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow))  // Define a data de criação do histórico
             .ForMember(dest => dest.Aprovada, opt => opt.MapFrom(src => src.Aprovada))  // Marca como aprovada
             .ForMember(dest => dest.Ativa, opt => opt.MapFrom(src => src.Ativa)) // Marca como ativa
             .ForMember(dest => dest.ConteudoGerado, opt => opt.MapFrom(src => GetConteudoGerado(src)));
        }
        private string GetConteudoGerado(Campanha campanha)
        {
            return campanha.Postagens?.FirstOrDefault()?.ConteudoGerado ?? string.Empty;
        }
    }
}