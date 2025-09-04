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


            CreateMap<NovaDoacaoRequestDTO, Doacao>();
            CreateMap<Doacao, DoacaoResponseDTO>();


            CreateMap<NovaTransacaoRequestDTO, Transacao>().ForMember(dest=>dest.ReceiverId, opt=>opt.MapFrom(src=>src.EmpresaId));
            CreateMap<Transacao, TransacaoResponseDTO>();

        }
    }
}
