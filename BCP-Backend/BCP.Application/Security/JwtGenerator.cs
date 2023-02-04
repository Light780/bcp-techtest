using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCP.Application.Interfaces.Security;
using BCP.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BCP.Application.Security
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly IConfiguration _configuration;
        
        public JwtGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string CreateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, usuario.NombreCompleto),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Correo)
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires =  DateTime.UtcNow.AddHours(2),
                SigningCredentials = credentials
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}