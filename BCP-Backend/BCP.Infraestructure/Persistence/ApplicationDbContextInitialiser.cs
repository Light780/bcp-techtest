using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCP.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BCP.Infraestructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception e)
            {
                throw new Exception("An error ocurred while seeding database");
            }
        }

        private async Task TrySeedAsync()
        {
            var monedas = new List<Moneda>()
            {
                new Moneda{Id = Guid.NewGuid(), CodigoSunat = "PEN", Nombre = "Soles Peruanos"},
                new Moneda{Id = Guid.NewGuid(), CodigoSunat = "CLP", Nombre = "Peso Chileno"},
                new Moneda{Id = Guid.NewGuid(), CodigoSunat = "USD", Nombre = "Dólares Americanos"},
                new Moneda{Id = Guid.NewGuid(), CodigoSunat = "MXN", Nombre = "Peso Mexicano"},
                new Moneda{Id = Guid.NewGuid(), CodigoSunat = "ARS", Nombre = "Peso Argentino"},
            };
            _context.Monedas.AddRange(monedas);
            
            monedas.ForEach(moneda =>
            {
                if (moneda.CodigoSunat != "PEN")
                {
                    _context.TipoCambios.Add(new TipoCambio
                    {
                        Id = Guid.NewGuid(),
                        Moneda = moneda,
                        Fecha = DateTime.Today,
                        Compra = 3.830m,
                        Venta = 3.835m
                    });
                }
                    
            });

            await _context.SaveChangesAsync();
        }
    }
}