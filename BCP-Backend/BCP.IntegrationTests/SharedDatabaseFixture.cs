using System;
using BCP.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BCP.IntegrationTests
{
    public class SharedDatabaseFixture : IDisposable
    {
        private static readonly object _lock = new object();
        private static bool _dbInitialised; 
        private static DbContextOptions<ApplicationDbContext> _dbContextOptions 
            = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("BcpTest").Options;
        
        private ApplicationDbContext _context = null!;

        public SharedDatabaseFixture()
        {
            Seed();
        }
        
        public ApplicationDbContext CreateContext()
        {
            _context = new ApplicationDbContext(_dbContextOptions);
            return _context;
        }
        
        private void Seed()
        {
            lock (_lock)
            {
                if (_dbInitialised) return;
                
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    var initialiser = new ApplicationDbContextInitialiser(context);
                    initialiser.SeedAsync().Wait();
                }

                _dbInitialised = true;
            }
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}