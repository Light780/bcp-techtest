using System.Threading;
using System.Threading.Tasks;
using BCP.Application.Interfaces.Common;
using BCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BCP.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Moneda>().HasIndex(m => m.CodigoSunat).IsUnique();
        }

        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<TipoCambio> TipoCambios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}