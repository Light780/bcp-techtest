using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BCP.Application.DTOs.TipoCambio;
using BCP.Application.Exceptions;
using BCP.Application.Interfaces.Common;
using BCP.Application.Interfaces.Services;
using BCP.Application.Mappings;
using BCP.Application.Services;
using Xunit;

namespace BCP.IntegrationTests.Services
{
    public class TipoCambioServiceTests : IClassFixture<SharedDatabaseFixture>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoCambioServiceTests(SharedDatabaseFixture fixture)
        {
            _context = fixture.CreateContext();
            var configuration = new MapperConfiguration(options =>
            {
                options.AddProfile<AutoMapperProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task Create_ShouldCreateNewTipoCambio_AndReturnIt()
        {
            var request = new CreateTipoCambioRequest
            {
                Moneda = "USD",
                Fecha = DateTime.Now.AddDays(-1),
                Compra = 3.90m,
                Venta = 4.0m
            };

            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);
            var response = await tipoCambioService.Create(request);
            
            Assert.NotNull(response.Data);
            Assert.Equal("Tipo de Cambio registrado correctamente", response.Message);
            Assert.True(response.Succeeded);
        }
        
        [Fact]
        public async Task Create_ShouldTrowValidationException()
        {
            var request = new CreateTipoCambioRequest
            {
                Moneda = "",
                Fecha = DateTime.MinValue,
                Compra = 0,
                Venta = -5
            };

            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);
            
            await Assert.ThrowsAsync<ValidationException>(async () 
                => await tipoCambioService.Create(request));
        }
        
        [Theory]
        [InlineData("AAA")]
        [InlineData("BBB")]
        [InlineData("CCC")]
        public async Task Create_ShouldThrowKeyNotFoundException(string moneda)
        {
            var request = new CreateTipoCambioRequest
            {
                Moneda = moneda,
                Fecha = DateTime.Now.AddDays(-1),
                Compra = 3.90m,
                Venta = 4.0m
            };

            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);
            
            await Assert.ThrowsAsync<KeyNotFoundException>(async ()
                => await tipoCambioService.Create(request));
        }
        
        [Fact]
        public async Task Create_ShouldThrowApiException()
        {
            var request = new CreateTipoCambioRequest
            {
                Moneda = "USD",
                Fecha = DateTime.Now,
                Compra = 3.90m,
                Venta = 4.0m
            };

            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);
            
            await Assert.ThrowsAsync<ApiException>(async () 
                => await tipoCambioService.Create(request));
        }
        
        [Fact]
        public async Task Update_ShouldUpdateTipoCambio_AndReturnIt()
        {
            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);

            var id = (await tipoCambioService.Get(null)).Data.First().Id;
            
            var request = new UpdateTipoCambioRequest()
            {
                Id = id,
                Compra = 3.90m,
                Venta = 4.0m
            };

            var response = await tipoCambioService.Update(request);
            
            Assert.NotNull(response.Data);
            Assert.Equal("Tipo de Cambio actualizado correctamente", response.Message);
            Assert.True(response.Succeeded);
        }
        
        [Fact]
        public async Task Update_ShouldTrowValidationException()
        {
            var request = new UpdateTipoCambioRequest
            {
                Id = string.Empty,
                Compra = 0,
                Venta = -5
            };

            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);
            
            await Assert.ThrowsAsync<ValidationException>(async () 
                => await tipoCambioService.Update(request));
        }
        
        [Theory]
        [InlineData("b33a4daf-e763-4de5-bafe-5bfc33831d34")]
        [InlineData("fd3c73b7-0099-404a-b462-5f468aed789f")]
        [InlineData("d870c75e-4f25-4be5-8e2c-0c4720656336")]
        public async Task Update_ShouldThrowKeyNotFoundException(string id)
        {
            var request = new UpdateTipoCambioRequest
            {
                Id = id,
                Compra = 3.90m,
                Venta = 4.0m
            };

            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);
            
            await Assert.ThrowsAsync<KeyNotFoundException>(async () 
                => await tipoCambioService.Update(request));
        }
        
        [Fact]
        public async Task Delete_ShouldDeleteTipoCambio()
        {
            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);

            var id = (await tipoCambioService.Get(null)).Data.First().Id;
            
            var request = new DeleteTipoCambioRequest()
            {
                Id = id,
            };

            var response = await tipoCambioService.Delete(request);
            
            Assert.Equal("Tipo de Cambio eliminado correctamente", response.Message);
            Assert.True(response.Succeeded);
        }
        
        [Fact]
        public async Task Delete_ShouldTrowValidationException()
        {
            var request = new DeleteTipoCambioRequest
            {
                Id = string.Empty,
            };

            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);
            
            await Assert.ThrowsAsync<ValidationException>(async () 
                => await tipoCambioService.Delete(request));
        }
        
        [Theory]
        [InlineData("b33a4daf-e763-4de5-bafe-5bfc33831d34")]
        [InlineData("fd3c73b7-0099-404a-b462-5f468aed789f")]
        [InlineData("d870c75e-4f25-4be5-8e2c-0c4720656336")]
        public async Task Delete_ShouldThrowKeyNotFoundException(string id)
        {
            var request = new DeleteTipoCambioRequest
            {
                Id = id
            };

            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);
            
            await Assert.ThrowsAsync<KeyNotFoundException>(async () 
                => await tipoCambioService.Delete(request));
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("USD")]
        [InlineData("MXN")]
        [InlineData("CLP")]
        public async Task Get_ShouldReturnMonedas(string moneda)
        {
            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);

            var request = new GetTipoCambioRequest
            {
                Moneda = moneda
            };

            var response = await tipoCambioService.Get(request);
            
            Assert.NotNull(response.Data);
            Assert.True(response.Succeeded);
        }

        [Theory]
        [InlineData("500", "PEN", "USD")]
        [InlineData("1000", "USD", "PEN")]
        [InlineData("1000", "PEN", "MXN")]
        public async Task ConvertAmount_ShouldReturnAmountConverted(string monto, string monedaOrigen
            , string monedaDestino)
        {
            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);

            var request = new ConvertTipoCambioRequest
            {
                Monto = Convert.ToDecimal(monto),
                MonedaOrigen = monedaOrigen,
                MonedaDestino = monedaDestino
            };

            var response = await tipoCambioService.ConvertAmount(request);
            
            Assert.NotNull(response.Data);
            Assert.True(response.Succeeded);
        }
        
        [Fact]
        public async Task ConvertAmount_ShouldThrowValidationException()
        {
            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);

            var request = new ConvertTipoCambioRequest
            {
                Monto = 0,
                MonedaOrigen = null,
                MonedaDestino = string.Empty
            };

            await Assert.ThrowsAsync<ValidationException>(async () 
                => await tipoCambioService.ConvertAmount(request));
        }
        
        [Theory]
        [InlineData("WWW", "USD")]
        [InlineData("PEN", "CCC")]
        public async Task ConvertAmount_ShouldThrowKeyNotFoundException(string monedaOrigen, string monedaDestino)
        {
            ITipoCambioService tipoCambioService = new TipoCambioService(_context, _mapper);

            var request = new ConvertTipoCambioRequest
            {
                Monto = 500,
                MonedaOrigen = monedaOrigen,
                MonedaDestino = monedaDestino
            };

            await Assert.ThrowsAsync<KeyNotFoundException>(async () 
                => await tipoCambioService.ConvertAmount(request));
        }
        
    }
}