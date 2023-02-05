using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BCP.Application.DTOs.TipoCambio;
using BCP.Application.Exceptions;
using BCP.Application.Interfaces.Common;
using BCP.Application.Interfaces.Services;
using BCP.Application.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace BCP.Application.Services
{
    public class TipoCambioService : ITipoCambioService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoCambioService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Response<GetTipoCambioResponse>> Create(CreateTipoCambioRequest request)
        {
            var validator = new CreateTipoCambioValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (!validatorResult.IsValid)
                throw new ValidationException(validatorResult.Errors);

            var moneda = await _context.Monedas.FirstOrDefaultAsync(m => m.CodigoSunat == request.Moneda);
            if (moneda is null)
                throw new KeyNotFoundException("Moneda ingresada no existe en la base de datos");

            var todayRegistered =
                await _context.TipoCambios.AnyAsync(t => t.Fecha.Date == request.Fecha.Date && t.IdMoneda == moneda.Id);
            if (todayRegistered)
                throw new ApiException("Ya existe un tipo de cambio registrado en esa fecha");

            var tipoCambio = _mapper.Map<Domain.Entities.TipoCambio>(request);
            tipoCambio.Id = Guid.NewGuid();
            tipoCambio.Moneda = moneda;

            _context.TipoCambios.Add(tipoCambio);
            await _context.SaveChangesAsync(default);
            
            var response = _mapper.Map<GetTipoCambioResponse>(tipoCambio);

            return new Response<GetTipoCambioResponse>(response,"Tipo de Cambio registrado correctamente");
        }

        public async Task<Response<GetTipoCambioResponse>> Update(UpdateTipoCambioRequest request)
        {
            var validator = new UpdateTipoCambioValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (!validatorResult.IsValid)
                throw new ValidationException(validatorResult.Errors);

            var tipoCambio = await _context.TipoCambios.FindAsync(Guid.Parse(request.Id));
            if (tipoCambio is null)
                throw new KeyNotFoundException("Tipo de Cambio no existe en la base de datos");
            
            tipoCambio.Compra = request.Compra;
            tipoCambio.Venta = request.Venta;

            await _context.SaveChangesAsync(default);

            var response = _mapper.Map<GetTipoCambioResponse>(tipoCambio);

            return new Response<GetTipoCambioResponse>(response, "Tipo de Cambio actualizado correctamente");
        }

        public async Task<Response<string>> Delete(DeleteTipoCambioRequest request)
        {
            var validator = new DeleteTipoCambioValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (!validatorResult.IsValid)
                throw new ValidationException(validatorResult.Errors);

            var tipoCambio = await _context.TipoCambios.FindAsync(Guid.Parse(request.Id));
            if (tipoCambio is null)
                throw new KeyNotFoundException("Tipo de Cambio no existe en la base de datos");

            _context.TipoCambios.Remove(tipoCambio);

            await _context.SaveChangesAsync(default);
            
            return new Response<string>(message: "Tipo de Cambio eliminado correctamente");
        }

        public async Task<Response<IEnumerable<GetTipoCambioResponse>>> Get(GetTipoCambioRequest request)
        {
            var tipoCambiosQueryable = _context.TipoCambios.Include(t => t.Moneda).AsQueryable();
            if (!string.IsNullOrEmpty(request?.Moneda))
                tipoCambiosQueryable = tipoCambiosQueryable.Where(t => t.Moneda.CodigoSunat == request.Moneda);
            
            var tipoCambios = await tipoCambiosQueryable.ToListAsync();
            var responses = _mapper.Map<IEnumerable<GetTipoCambioResponse>>(tipoCambios);
            
            return new Response<IEnumerable<GetTipoCambioResponse>>(responses);
        }

        public async Task<Response<ConvertTipoCambioResponse>> ConvertAmount(ConvertTipoCambioRequest request)
        {
            var validator = new ConvertTipoCambioValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (!validatorResult.IsValid)
                throw new ValidationException(validatorResult.Errors);

            var monedaOrigen = await _context.Monedas.FirstOrDefaultAsync(m => m.CodigoSunat == request.MonedaOrigen);
            if(monedaOrigen is null)
                throw new KeyNotFoundException("Moneda de origen ingresada no existe en la base de datos");
            
            var monedaDestino = await _context.Monedas.FirstOrDefaultAsync(m => m.CodigoSunat == request.MonedaDestino);
            if(monedaDestino is null)
                throw new KeyNotFoundException("Moneda de destino ingresada no existe en la base de datos");

            var tipoCambio = await _context.TipoCambios
                .OrderByDescending(t => t.Fecha)
                .FirstOrDefaultAsync(t => t.IdMoneda == (monedaOrigen.CodigoSunat != "PEN" ? monedaOrigen.Id : monedaDestino.Id));
            
            if(tipoCambio is null)
                throw new KeyNotFoundException("No existe tipo de cambio con la moneda ingresada");

            var response = new ConvertTipoCambioResponse
            {
                Monto = request.Monto,
                MontoConvertido 
                    = Math.Round(
                        monedaOrigen.CodigoSunat != "PEN" 
                        ? request.Monto * tipoCambio.Venta 
                        : request.Monto / tipoCambio.Compra,
                        3),
                MonedaDestino = string.Concat(monedaDestino.CodigoSunat, $" ({monedaDestino.Nombre})"),
                MonedaOrigen = string.Concat(monedaOrigen.CodigoSunat, $" ({monedaOrigen.Nombre})"),
                TipoCambio = monedaOrigen.CodigoSunat != "PEN" ? tipoCambio.Venta : tipoCambio.Compra
            };

            return new Response<ConvertTipoCambioResponse>(response);
        }
    }
}