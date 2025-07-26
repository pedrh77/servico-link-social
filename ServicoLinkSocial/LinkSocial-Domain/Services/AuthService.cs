using LinkSocial_Domain.DTO.Request;
using LinkSocial_Domain.DTO.Response;
using LinkSocial_Domain.Interfaces.Auth;
using LinkSocial_Domain.Interfaces.Usuarios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkSocial_Domain.Services
{
    public class AuthService(IConfiguration _config, IUsuarioService _usuarioService) : IAuthService
    {
        public async Task<LoginResponseDTO> AutenticarAsync(LoginRequestDTO login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var usuario = await _usuarioService.ValidaDadosLoginUsario(login);

            if (usuario != null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, usuario.Nome),
                        new Claim(ClaimTypes.Role, usuario.TipoUsuario.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new LoginResponseDTO
                {
                    Token = tokenHandler.WriteToken(token),
                    ExpiraEm = tokenDescriptor.Expires.Value
                };
            }
            throw new UnauthorizedAccessException("Usuário ou senha inválidos.");
        }
    }
}

