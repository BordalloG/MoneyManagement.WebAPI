using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMWebAPI.WebAPI.Middelware
{
    /// <summary>
    /// Middleware para interceptar exceções
    /// </summary>
    public class ErrorHandlerMiddleware
    {

        /// <summary>
        /// Handler para manipular exceções não esperadas
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, Func<Task> next)
        {
            try
            {
                await next.Invoke();
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var exceptionObject = new ExceptionResponse(exception);
            var exceptionSerialized = JsonConvert.SerializeObject(exceptionObject);

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)exceptionObject.Error.Code;

                await context.Response.WriteAsync(exceptionSerialized);
            }
        }
    }
}
