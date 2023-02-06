using System;
using System.Threading.Tasks;
using BCP.Application.DTOs.TipoCambio;
using Xunit;

namespace BCP.UnitTests.DTOs
{
    public class TipoCambioRequestTests
    {
        [Theory]
        [InlineData("PEN", "2023-02-03", "3.5", "4.0", 0)]
        [InlineData("", "2023-02-03", "3.5", "4.0", 1)]
        [InlineData("", "2023-03-03", "3.5", "4.0", 2)]
        [InlineData("", "2023-03-03", "0", "4.0", 3)]
        [InlineData("", "2023-03-03", "0", "-5", 4)]
        public async Task ValidateModel_CreateTipoCambioRequest(string moneda, string fecha, string compra, string venta
            , int expectedErrors )
        {
            var request = new CreateTipoCambioRequest
            {
                Moneda = moneda,
                Fecha = string.IsNullOrEmpty(fecha) ? DateTime.UtcNow : DateTime.Parse(fecha),
                Compra = Convert.ToDecimal(compra),
                Venta = Convert.ToDecimal(venta)
            };
            
            var validator = new CreateTipoCambioValidator();

            var validatorResult = await validator.ValidateAsync(request);
            
            Assert.Equal(expectedErrors, validatorResult.Errors.Count);
        }

        [Theory]
        [InlineData("ID", "3.5", "4.0", 0)]
        [InlineData("", "3.5", "4.0", 1)]
        [InlineData("", "0", "4.0", 2)]
        [InlineData("", "0", "-5", 3)]
        public async Task ValidateModel_UpdateTipoCambioRequest(string id, string compra, string venta, int expectedErrors )
        {
            var request = new UpdateTipoCambioRequest
            {
                Id = id,
                Compra = Convert.ToDecimal(compra),
                Venta = Convert.ToDecimal(venta)
            };
            
            var validator = new UpdateTipoCambioValidator();

            var validatorResult = await validator.ValidateAsync(request);
            
            Assert.Equal(expectedErrors, validatorResult.Errors.Count);
        }
        
        [Theory]
        [InlineData("ID", 0)]
        [InlineData(null, 1)]
        public async Task ValidateModel_DeleteTipoCambioRequest(string id, int expectedErrors)
        {
            var request = new DeleteTipoCambioRequest()
            {
                Id = id
            };
            
            var validator = new DeleteTipoCambioValidator();

            var validatorResult = await validator.ValidateAsync(request);
            
            Assert.Equal(expectedErrors, validatorResult.Errors.Count);
        }
        
        [Theory]
        [InlineData("500", "USD", "PEN", 0)]
        [InlineData("0", "USD", "PEN", 1)]
        [InlineData("0", "", "PEN", 2)]
        [InlineData("0", "", "", 3)]
        public async Task ValidateModel_ConvertTipoCambioRequest(string monto, string monedaOrigen, string monedDestino
            , int expectedErrors)
        {
            var request = new ConvertTipoCambioRequest()
            {
                Monto = Convert.ToDecimal(monto),
                MonedaDestino = monedDestino,
                MonedaOrigen = monedaOrigen
            };
            
            var validator = new ConvertTipoCambioValidator();

            var validatorResult = await validator.ValidateAsync(request);
            
            Assert.Equal(expectedErrors, validatorResult.Errors.Count);
        }
    }
}