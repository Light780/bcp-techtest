using System.Collections.Generic;
using System.Threading.Tasks;
using BCP.Application.DTOs.TipoCambio;
using BCP.Application.Wrappers;

namespace BCP.Application.Interfaces.Services
{
    public interface ITipoCambioService
    {
        Task<Response<GetTipoCambioResponse>> Create(CreateTipoCambioRequest request);
        Task<Response<GetTipoCambioResponse>> Update(UpdateTipoCambioRequest request);
        Task<Response<string>> Delete(DeleteTipoCambioRequest request);
        Task<Response<IEnumerable<GetTipoCambioResponse>>> Get(GetTipoCambioRequest request);
        Task<Response<ConvertTipoCambioResponse>> ConvertAmount(ConvertTipoCambioRequest request);
    }
}