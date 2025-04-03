using Labb2_Infrastructure.Authentication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labb2_Infrastructure.Authentication.Responses.CustomResponses;

namespace Labb2_Infrastructure.Authentication.Repos
{
    public interface IAccount
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDTO model);
        Task<LoginResponse> LoginAsync(LoginDTO model);

        LoginResponse RefreshToken(UserSession userSession);
    }
}
