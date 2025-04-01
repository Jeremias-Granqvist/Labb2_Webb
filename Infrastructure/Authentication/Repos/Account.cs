using BCrypt.Net;
using Labb2_Infrastructure.Authentication.DTOs;
using Labb2_Infrastructure.Authentication.Models;
using Labb2_Infrastructure.Authentication.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Labb2_Infrastructure.Authentication.Responses.CustomResponses;

namespace Labb2_Infrastructure.Authentication.Repos
{
    public class Account : IAccount
    {
        private readonly StoreContext _context;
        private readonly IConfiguration _config;

        public Account(StoreContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<LoginResponse> LoginAsync(LoginDTO model)
        {
            var findUser = await GetUser(model.Email);
            if (findUser == null) 
                return new LoginResponse(false, "User not found");

            if (!BCrypt.Net.BCrypt.Verify(model.Password, findUser.Password))
                return new LoginResponse(false, "email/password not valid");

            string jwtToken = GenerateToken(findUser);
            return new LoginResponse(true, "login successfully", jwtToken);

        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
            var findUser = await GetUser(model.Email);
            if (findUser != null) return new RegistrationResponse(false, "User already exists");

            _context.Users.Add(
                new ApplicationUser()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
                });

            await _context.SaveChangesAsync();
            return new RegistrationResponse(true, "Success");
        }

        private string GenerateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"]!,
                audience: _config["Jwt:Autdience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ApplicationUser> GetUser(string email) 
            => await _context.Users.FirstOrDefaultAsync(e => e.Email == email);

    }
}
