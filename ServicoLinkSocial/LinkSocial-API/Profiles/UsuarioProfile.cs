using AutoMapper;
using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.Models;

namespace LinkSocial_API.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<NovoUsuarioRequestDTO, Usuario>();
        }
    }
}
