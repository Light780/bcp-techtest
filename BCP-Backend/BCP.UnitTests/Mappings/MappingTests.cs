using System;
using System.Runtime.Serialization;
using AutoMapper;
using BCP.Application.DTOs.Moneda;
using BCP.Application.DTOs.TipoCambio;
using BCP.Application.DTOs.Usuario;
using BCP.Application.Mappings;
using BCP.Domain.Entities;
using Xunit;

namespace BCP.UnitTests.Mappings
{
    public class MappingsTest
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;
        
        public MappingsTest()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldBeValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(Moneda), typeof(GetMonedaResponse))]
        [InlineData(typeof(CreateTipoCambioRequest), typeof(TipoCambio))]
        [InlineData(typeof(TipoCambio), typeof(GetTipoCambioResponse))]
        [InlineData(typeof(RegisterUsuarioRequest), typeof(Usuario))]
        public void Map_SourceToDestination_ExistConfiguration(Type source, Type destination)
        {
            var instance = FormatterServices.GetUninitializedObject(source);
            _mapper.Map(instance, source, destination);
        }
    }
}