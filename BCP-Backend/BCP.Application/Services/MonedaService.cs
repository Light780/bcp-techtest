using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BCP.Application.DTOs.Moneda;
using BCP.Application.Interfaces.Common;
using BCP.Application.Interfaces.Services;
using BCP.Application.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace BCP.Application.Services
{
    public class MonedaService : IMonedaService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MonedaService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Response<IEnumerable<GetMonedaResponse>>> GetAll()
        {
            var monedas = await _context.Monedas.ToListAsync();
            var monedasResponse = _mapper.Map<IEnumerable<GetMonedaResponse>>(monedas);
            return new Response<IEnumerable<GetMonedaResponse>>(monedasResponse);
        }
    }
}