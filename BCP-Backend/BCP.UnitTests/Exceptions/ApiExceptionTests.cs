using BCP.Application.Exceptions;
using Xunit;

namespace BCP.UnitTests.Exceptions
{
    public class ApiExceptionTests
    {
        [Fact]
        public void DefaultConstructor_CreatesDefaultMessage()
        {
            var exception = new ApiException();
            Assert.NotNull(exception.Message);
        }

        [Theory]
        [InlineData("Error al registrar usuario")]
        [InlineData("Error al registrar moneda")]
        [InlineData("Error al registrar tipo de cambio")]
        public void MessageFromParam_EqualToExceptionMessage(string message)
        {
            var exception = new ApiException(message);
            Assert.Equal(message, exception.Message);
        }
    }
}