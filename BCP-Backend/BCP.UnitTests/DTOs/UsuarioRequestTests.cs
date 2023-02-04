using System.Linq;
using System.Threading.Tasks;
using BCP.Application.DTOs.Usuario;
using FluentValidation;
using Xunit;

namespace BCP.UnitTests.DTOs
{
    public class UsuarioRequestTests
    {
        [Theory]
        [InlineData("bruno@gmail.com", "123456", 0)]
        [InlineData("bruno@gmail.com", null, 1)]
        [InlineData("", null, 2)]
        public async Task ValidateModel_LoginUsuarioRequest(string correo, string password, int expectedErrors)
        {
            var request = new LoginUsuarioRequest
            {
                Correo = correo,
                Password = password
            };

            var validator = new LoginUsuarioValidator();

            var validatorResult = await validator.ValidateAsync(request);
            
            Assert.Equal(expectedErrors, validatorResult.Errors.Count);
        }
        
        [Theory]
        [InlineData("Bruno Ramos","bruno@gmail.com", "12345678", 0)]
        [InlineData("Bruno Ramos","bruno@gmail.com", null, 1)]
        [InlineData("","bruno@gmail.com", null, 2)]
        [InlineData("", null, null, 3)]
        public async Task ValidateModel_RegisterUsuarioRequest(string nombreCompleto, string correo, string password, int expectedErrors)
        {
            var request = new RegisterUsuarioRequest()
            {
                NombreCompleto = nombreCompleto,
                Correo = correo,
                Password = password
            };

            var validator = new RegisterUsuarioValidator();

            var validatorResult = await validator.ValidateAsync(request);
            
            Assert.Equal(expectedErrors, validatorResult.Errors.Count);
        }
    }
}