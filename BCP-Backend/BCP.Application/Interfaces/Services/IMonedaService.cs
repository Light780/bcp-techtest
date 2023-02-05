using System.Collections.Generic;
using System.Threading.Tasks;
using BCP.Application.DTOs.Moneda;
using BCP.Application.Wrappers;

namespace BCP.Application.Interfaces.Services
{
    public interface IMonedaService
    {
        Task<Response<IEnumerable<GetMonedaResponse>>> GetAll();
    }
}