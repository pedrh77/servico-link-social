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
            CreateMap<Usuario, UsuarioPorTipoResponseDTO>();
            CreateMap<AtualizaDadosUsuarioRequestDTO, Usuario>().ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone))
                .ForMember(dest => dest.Comentario, opt => opt.MapFrom(src => src.Comentario));


            CreateMap<NovaDoacaoRequestDTO, Doacao>();
            CreateMap<Doacao, DoacaoResponseDTO>()
            .ForMember(
                dest => dest.NomeDoador,
                opt => opt.MapFrom(src => src.Anonima ? null : src.Doador.Nome)
            ).ForMember(
                dest => dest.NomeOng,
                opt => opt.MapFrom(src => src.Ong.Nome )
            );



            CreateMap<NovaTransacaoRequestDTO, Transacao>().ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.EmpresaId));
            CreateMap<Transacao, TransacaoResponseDTO>();




            CreateMap<Carteira, CarteiraResponseDTO>()
                .ForMember(dest => dest.Saldo, opt => opt.MapFrom(src => src.SaldoAprovado.ToString()))
                .ForMember(dest => dest.SaldoPendente, opt => opt.MapFrom(src => src.SaldoPendente.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                ;

            CreateMap<Transacao, TransacaoDto>()
               .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Pedido, PedidosValidacaoResponseDTO>()
           .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo))
           .ForMember(dest => dest.TransacaoId, opt => opt.MapFrom(src => src.TransacaoId))
           .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Transacao.Valor))
           .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.Criado_em))
           .ForMember(dest => dest.NomeDoador, opt => opt.MapFrom(src => src.Doador.Nome))
           .ForMember(dest => dest.NomeTransacao, opt => opt.MapFrom(src => src.Transacao.NomeTransacao))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Transacao.Status));


        }
    }
}
