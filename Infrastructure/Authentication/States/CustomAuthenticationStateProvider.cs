using Labb2_Infrastructure.Authentication.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Labb2_Infrastructure.Authentication.States
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());



        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            try
            {

                if (string.IsNullOrEmpty(Constants.JWTToken))
                {
                    return await Task.FromResult(new AuthenticationState(anonymous));
                }

                var getUserClaims = DecryptJWTService.DecryptToken(Constants.JWTToken);
                if (getUserClaims == null)
                {
                    return await Task.FromResult(new AuthenticationState(anonymous));
                }

                var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch (Exception)
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public async void UpdateAuthenticationState(string jwtToken)
        {
            var claimsPrincipal = new ClaimsPrincipal();

            if (!string.IsNullOrEmpty(jwtToken))
            {
                Constants.JWTToken = jwtToken;
                var getUserClaims = DecryptJWTService.DecryptToken(jwtToken);
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                Constants.JWTToken = null!;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaim claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();

            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.Name, claims.Email!),
                    new(ClaimTypes.Email, claims.Email),
                    new(ClaimTypes.Role, claims.Role)

                }, "JwtAuth"));
        }


    }
}
