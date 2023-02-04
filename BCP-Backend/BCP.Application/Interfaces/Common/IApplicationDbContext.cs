using System.Threading;
using System.Threading.Tasks;
using BCP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BCP.Application.Interfaces.Common
{
    public interface IApplicationDbContext
    {
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<TipoCambio> TipoCambios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}