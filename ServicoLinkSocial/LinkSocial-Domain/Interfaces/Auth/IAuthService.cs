using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkSocial_Domain.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> AutenticarAsync(LoginRequestDTO login);
    }
}
