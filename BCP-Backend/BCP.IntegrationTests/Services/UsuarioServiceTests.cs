using System.Threading.Tasks;
using AutoMapper;
using BCP.Application.DTOs.Usuario;
using BCP.Application.Exceptions;
using BCP.Application.Interfaces.Authentication;
using BCP.Application.Interfaces.Common;
using BCP.Application.Interfaces.Services;
using BCP.Application.Mappings;
using BCP.Application.Services;
using BCP.Domain.Entities;
using Moq;
using Xunit;

namespace BCP.IntegrationTests.Services
{
    public class UsuarioServiceTests : IClassFixture<SharedDatabaseFixture>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<IJwtGenerator> _mockJwtGenerator;
        private readonly Mock<IUsuarioSession> _mockUsuarioSession;
        
        public UsuarioServiceTests(SharedDatabaseFixture fixture)
        {
            _context = fixture.CreateContext();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mockJwtGenerator = new Mock<IJwtGenerator>();
            _mockUsuarioSession = new Mock<IUsuarioSession>();
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task Register_ShouldCreateNewUser_AndReturnUsuarioWithToken()
        {
            IUsuarioService usuarioService =
                new UsuarioService(_context, _mapper, _mockJwtGenerator.Object, _mockUsuarioSession.Object);
            
            _mockJwtGenerator.Setup(_ => _.GenerateToken(It.IsAny<Usuario>())).Returns("TokenGenerado");
            var request = new RegisterUsuarioRequest
            {
                NombreCompleto = "Bruno Ramos",
                Correo = "brunorlm88@gmail.com",
                Password = "12345678"
            };
            
            var response = await usuarioService.Register(request);
            
            Assert.NotNull(response.Data);
            Assert.Equal("Te has registrado exitosamente", response.Message);
            Assert.Equal("TokenGenerado", response.Data.Token);
            Assert.Equal(request.NombreCompleto, response.Data.NombreCompleto);
            Assert.Equal(request.Correo, response.Data.Correo);
            Assert.True(response.Succeeded);
        }
        
        [Fact]
        public async Task Register_ShouldThrowApiException()
        {
            IUsuarioService usuarioService =
                new UsuarioService(_context, _mapper, _mockJwtGenerator.Object, _mockUsuarioSession.Object);
            
            var request = new RegisterUsuarioRequest
            {
                NombreCompleto = "Bruno Ramos",
                Correo = "brunorlm88@gmail.com",
                Password = "12345678"
            };
            
            await Assert.ThrowsAsync<ApiException>(async () => await usuarioService.Register(request));
        }
        
        [Fact]
        public async Task Register_ShouldThrowValidationException()
        {
            IUsuarioService usuarioService =
                new UsuarioService(_context, _mapper, _mockJwtGenerator.Object, _mockUsuarioSession.Object);
            
            var request = new RegisterUsuarioRequest
            {
                NombreCompleto = null,
                Correo = string.Empty,
                Password = null
            };

            await Assert.ThrowsAsync<ValidationException>(async () => await usuarioService.Register(request));
        }
        
        [Fact]
        public async Task Login_ShouldReturnUsuarioWithToken()
        {
            _mockJwtGenerator.Setup(_ => _.GenerateToken(It.IsAny<Usuario>())).Returns("TokenGenerado");

            IUsuarioService usuarioService =
                new UsuarioService(_context, _mapper, _mockJwtGenerator.Object, _mockUsuarioSession.Object);
            
            var request = new LoginUsuarioRequest()
            {
                Correo = "brunorlm88@gmail.com",
                Password = "12345678"
            };
            
            var response = await usuarioService.Login(request);
            
            Assert.NotNull(response.Data);
            Assert.Equal("TokenGenerado", response.Data.Token);
            Assert.Equal(request.Correo, response.Data.Correo);
            Assert.True(response.Succeeded);
        }
        
        [Fact]
        public async Task Login_ShouldThrowApiException()
        {
            IUsuarioService usuarioService =
                new UsuarioService(_context, _mapper, _mockJwtGenerator.Object, _mockUsuarioSession.Object);
            
            var request = new LoginUsuarioRequest()
            {
                Correo = "brunorlm888@gmail.com",
                Password = "12345678"
            };
            
            await Assert.ThrowsAsync<ApiException>(async () 
                => await usuarioService.Login(request));
        }
        
        [Fact]
        public async Task Login_ShouldThrowValidationException()
        {
            IUsuarioService usuarioService =
                new UsuarioService(_context, _mapper, _mockJwtGenerator.Object, _mockUsuarioSession.Object);
            
            var request = new LoginUsuarioRequest()
            {
                Correo = string.Empty,
                Password = null
            };
            
            await Assert.ThrowsAsync<ValidationException>(async () 
                => await usuarioService.Login(request));
        }
        
        [Fact]
        public async Task GetByToken_ShouldReturnUsuarioWithToken()
        {
            _mockJwtGenerator.Setup(_ => _.GenerateToken(It.IsAny<Usuario>())).Returns("TokenGenerado");
            _mockUsuarioSession.Setup(_ => _.GetUsuarioSession()).Returns("brunorlm88@gmail.com");

            IUsuarioService usuarioService =
                new UsuarioService(_context, _mapper, _mockJwtGenerator.Object, _mockUsuarioSession.Object);
            
            var response = await usuarioService.GetByToken();
            
            Assert.NotNull(response.Data);
            Assert.Equal("TokenGenerado", response.Data.Token);
            Assert.Equal("brunorlm88@gmail.com", response.Data.Correo);
        }
        
        [Fact]
        public async Task GetByToken_ShouldThrowApiException()
        {
            _mockUsuarioSession.Setup(_ => _.GetUsuarioSession()).Returns("brunorlm888@gmail.com");

            IUsuarioService usuarioService =
                new UsuarioService(_context, _mapper, _mockJwtGenerator.Object, _mockUsuarioSession.Object);
            
            await Assert.ThrowsAsync<ApiException>(async () 
                => await usuarioService.GetByToken());
        }
        
    }
}