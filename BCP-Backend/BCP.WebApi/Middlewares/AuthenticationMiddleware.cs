using System.Threading.Tasks;
using BCP.Application.Wrappers;
using Microsoft.AspNetCore.Http;

namespace BCP.WebApi.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            if (context.Response.StatusCode == 401)
            {
                var responseModel = new Response<string> { Succeeded = false, Message = "Usuario no autenticado" };
                await context.Response.WriteAsync(responseModel.ToString());
            }
        }
    }
}