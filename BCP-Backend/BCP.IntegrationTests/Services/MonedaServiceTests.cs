using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BCP.Application.Interfaces.Common;
using BCP.Application.Interfaces.Services;
using BCP.Application.Mappings;
using BCP.Application.Services;
using Xunit;

namespace BCP.IntegrationTests.Services
{
    public class MonedaServiceTests : IClassFixture<SharedDatabaseFixture>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MonedaServiceTests(SharedDatabaseFixture fixture)
        {
            _context = fixture.CreateContext();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllMonedas()
        {
            IMonedaService monedaService = new MonedaService(_context, _mapper);
            var response = await monedaService.GetAll();
            Assert.True(response.Data.Any());
        }
    }
}