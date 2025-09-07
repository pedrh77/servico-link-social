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
            CreateMap<Usuario, UsuarioResponseDTO>();
            CreateMap<AtualizaDadosUsuarioRequestDTO, Usuario>().ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone))
                .ForMember(dest => dest.Comentario, opt => opt.MapFrom(src => src.Comentario));


            CreateMap<NovaDoacaoRequestDTO, Doacao>();
            CreateMap<Doacao, DoacaoResponseDTO>()
     .ForMember(dest => dest.NomeDoador,
         opt => opt.MapFrom(src => src.Anonima ? null : src.Doador.Nome))
     .ForMember(dest => dest.NomeDoador,
         opt => opt.MapFrom(src => src.Anonima));



            CreateMap<NovaTransacaoRequestDTO, Transacao>().ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.EmpresaId));
            CreateMap<Transacao, TransacaoResponseDTO>();

        }
    }
}
