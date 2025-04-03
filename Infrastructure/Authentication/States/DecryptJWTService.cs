using Labb2_Infrastructure.Authentication.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Authentication.States
{
    public static class DecryptJWTService
    {
        public static CustomUserClaim DecryptToken(string jwtToken)
        {
            try { 
            if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaim();

            var handlder = new JwtSecurityTokenHandler();
            var token = handlder.ReadJwtToken(jwtToken);

            var name = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email);
                var role = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Role);
            return new CustomUserClaim(name!.Value, email!.Value, role!.Value);
            }
            catch
            {
                return null;
            }
        }
    }
}
