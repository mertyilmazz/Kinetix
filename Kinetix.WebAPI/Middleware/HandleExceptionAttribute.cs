using Kinetix.Business.ExceptionResult;
using Kinetix.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Kinetix.WebAPI.Middleware
{
    public class HandleExceptionAttribute
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;


        public HandleExceptionAttribute(RequestDelegate next, ILogger<HandleExceptionAttribute> logService)
        {
            _next = next;
            _logger = logService;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var code = HttpStatusCode.InternalServerError;
                if (ex is ValidationException)
                {
                    code = HttpStatusCode.BadRequest;
                    _logger.LogWarning(ex.Message);
                }
                else if (ex is NotFoundException)
                {
                    code = HttpStatusCode.NotFound;
                }
                else if (ex is NoStockException)
                {
                    code = HttpStatusCode.BadRequest;
                    _logger.LogWarning(ex.Message);
                }
                else if (ex is FormatingException)
                {
                    code = HttpStatusCode.BadRequest;
                    _logger.LogError(ex.Message);
                }
                else if (ex is Exception)
                {
                    code = HttpStatusCode.InternalServerError;
                    _logger.LogCritical(ex, ex.ToString());
                }
                await HandleExceptionAsync(httpContext, ex, code);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var model = new ErrorDetails
            {
                Message = exception.Message,
                StatusCode = (int)code
            };
            var result = JsonConvert.SerializeObject(model);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
