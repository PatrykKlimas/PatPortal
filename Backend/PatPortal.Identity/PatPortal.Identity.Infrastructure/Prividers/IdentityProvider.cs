﻿using Microsoft.IdentityModel.Tokens;
using PatPortal.Identity.Domain.Entities;
using PatPortal.Identity.Domain.Repositories;
using PatPortal.Identity.Infrastructure.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PatPortal.Identity.Infrastructure.Prividers
{
    public class IdentityProvider : IIdentityProvider
    {
        private readonly ApplicationConfiguration _applicationConfiguration;

        public IdentityProvider(ApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_applicationConfiguration.JwtConfig.Secret));

            var credentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _applicationConfiguration.JwtConfig.Issuer,
                audience: _applicationConfiguration.JwtConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
