using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BCP.Application.Exceptions;
using BCP.Application.Wrappers;
using Microsoft.AspNetCore.Http;

namespace BCP.WebApi.Middlewares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == 401)
                {
                    var responseModel = new Response<string> { Succeeded = false, Message = "Usuario no autenticado" };
                    await context.Response.WriteAsync(responseModel.ToString());
                }
            }
            catch (Exception ex)
            {
                await AsyncExceptionHandler(context, ex);
            }
        }

        private static async Task AsyncExceptionHandler(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            
            var responseModel = new Response<string> { Succeeded = false, Message = ex.Message };

            switch (ex)
            {
                case ApiException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ValidationException e:
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    responseModel.Errors = e.Errors;
                    break;
                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)(HttpStatusCode.InternalServerError);
                    break;
            }

            await response.WriteAsync(responseModel.ToString());
        }
    }
}