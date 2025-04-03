using Labb2_Infrastructure.Authentication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labb2_Infrastructure.Authentication.Responses.CustomResponses;

namespace Labb2_Infrastructure.Authentication.Services
{
    public interface IAccountService
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDTO model);
        Task<LoginResponse> RefreshToken(UserSession userSession);
        Task<LoginResponse> LoginAsync(LoginDTO model);
        public Task Logout();
    }
}
