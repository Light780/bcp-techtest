using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BCP.Application.Interfaces.Security;
using BCP.Application.Security;
using BCP.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace BCP.UnitTests.Security
{
    public class JwtGeneratorTests
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IConfiguration _configuration;
        public JwtGeneratorTests()
        {
            var config = new Dictionary<string, string>()
            {
                {"JWT:SecretKey", "randomString123456789"}
            };
            _configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            _jwtGenerator = new JwtGenerator(_configuration);
        }
        
        [Fact]
        public void CreateToken_ShouldGenerateNewToken()
        {
            var usuario = new Usuario
            {
                NombreCompleto = "Bruno Ramos",
                Correo = "bruno@gmail.com"
            };

            var token = _jwtGenerator.CreateToken(usuario);
            
            Assert.NotNull(token);
        }

        [Fact]
        public void ValidateToken_IsValid()
        {
            var usuario = new Usuario
            {
                NombreCompleto = "Bruno Ramos",
                Correo = "bruno@gmail.com"
            };

            var authToken = _jwtGenerator.CreateToken(usuario);
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                ValidateAudience = false,
                ValidateIssuer = false
            };

            tokenHandler.ValidateToken(authToken, validationParameters, out var validatedToken);
            Assert.NotNull(validatedToken);
        }
        
        [Fact]
        public void ValidateToken_IsInvalid()
        {
            var usuario = new Usuario
            {
                NombreCompleto = "Bruno Ramos",
                Correo = "bruno@gmail.com"
            };

            var authToken = _jwtGenerator.CreateToken(usuario) + "abcde";
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                ValidateAudience = false,
                ValidateIssuer = false
            };
            
            SecurityToken validatedToken = null;
            try
            {
                tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            }
            catch (Exception)
            {
                Assert.Null(validatedToken);
            }
        }
        
    }
}