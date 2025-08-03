using AutoMapper;
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Models;

namespace LinkSocial_API.Profiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<NovoUsuarioRequestDTO, Usuario>();
            CreateMap<NovoBeneficioRequestDTO, Beneficio>();
            CreateMap<NovaDoacaoRequestDTO, Doacao>();
            CreateMap<Doacao, DoacaoResponseDTO>();
            CreateMap<Beneficio, BeneficioResponseDTO>()
                .ForMember(dest => dest.OngId, opt => opt.MapFrom(src => src.UsuarioId));

        }
    }
}
