using AutoMapper;
using KarapinhaAPI.Models;
using KarapinhaAPI.DTO;
namespace KarapinhaAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {


           CreateMap<Utilizador, UtilizadorDTO>();
            CreateMap<UtilizadorDTO, Utilizador>();
            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<CategoriaDTO, Categoria>();
            CreateMap<Servico, ServicoDTO>();
            CreateMap<ServicoDTO, Servico>();
            CreateMap<MarcacaoDTO, Marcacao>();
            CreateMap<Profissional, ProfissionalDTO>();
            CreateMap<ProfissionalDTO, Profissional>();
            CreateMap<Profissional, CategoriaDTO>();
            CreateMap<ServicoMarcacaoDTO, ServicoMarcacao>();
            CreateMap<Marcacao, MarcacaoDTO>()
             .ForMember(dest => dest.Hora, opt => opt.MapFrom(src => src.Hora.ToString()))
             .ReverseMap();
            CreateMap<ServicoMarcacao, ServicoMarcacaoDTO>()
                .ForMember(dest => dest.Hora, opt => opt.MapFrom(src => src.Hora.ToString()))
                .ReverseMap();



        }    


    }
}
