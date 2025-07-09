using AutoMapper;
using Otogalimoveis.Domain.Model;
using Otogalimoveis.Application.DTOs;

namespace Otogalimoveis.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Imovel mappings
            CreateMap<Imovel, ImovelDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateImovelDTO, Imovel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ImovelStatus.Disponivel));

            CreateMap<UpdateImovelDTO, Imovel>();

            CreateMap<Imovel, ImovelDetailDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // Locatario mappings
            CreateMap<Locatario, LocatarioDTO>();

            CreateMap<CreateLocatarioDTO, Locatario>()
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => true));

            CreateMap<UpdateLocatarioDTO, Locatario>();

            CreateMap<Locatario, LocatarioDetailDTO>();

            // Aluguel mappings
            CreateMap<Aluguel, AluguelDTO>()
                .ForMember(dest => dest.ImovelEndereco, opt => opt.MapFrom(src => src.Imovel.Endereco))
                .ForMember(dest => dest.LocatarioNome, opt => opt.MapFrom(src => src.Locatario.Nome))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.DuracaoEmDias, opt => opt.MapFrom(src => 
                    src.DataFim.HasValue ? (int)(src.DataFim.Value - src.DataInicio).TotalDays : 
                    (int)(DateTime.Today - src.DataInicio).TotalDays));

            CreateMap<CreateAluguelDTO, Aluguel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AluguelStatus.Ativo));

            CreateMap<UpdateAluguelDTO, Aluguel>();

            CreateMap<Aluguel, AluguelDetailDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.DuracaoEmDias, opt => opt.MapFrom(src => 
                    src.DataFim.HasValue ? (int)(src.DataFim.Value - src.DataInicio).TotalDays : 
                    (int)(DateTime.Today - src.DataInicio).TotalDays));
        }
    }
} 