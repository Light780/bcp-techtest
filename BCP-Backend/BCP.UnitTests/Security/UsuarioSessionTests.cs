using System.Collections.Generic;
using System.Security.Claims;
using BCP.Application.Interfaces.Security;
using BCP.Application.Security;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace BCP.UnitTests.Security
{
    public class UsuarioSessionTests
    {
        [Fact]
        public void GetUsuarioSession_ShouldReturnNotNull()
        {
            var mockHttpContextAccesor = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, "bruno@gmail.com")
                }))
            };

            mockHttpContextAccesor.Setup(_ => _.HttpContext).Returns(context);

            IUsuarioSession usuarioSession = new UsuarioSession(mockHttpContextAccesor.Object);
            
            Assert.NotNull(usuarioSession.GetUsuarioSession());
        }
        
        [Fact]
        public void GetUsuarioSession_ShouldReturnNull()
        {
            var mockHttpContextAccesor = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();

            mockHttpContextAccesor.Setup(_ => _.HttpContext).Returns(context);

            IUsuarioSession usuarioSession = new UsuarioSession(mockHttpContextAccesor.Object);
            
            Assert.Null(usuarioSession.GetUsuarioSession());
        }
    }
}