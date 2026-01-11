using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Device.API.Application.Middleware
{
    public class GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionMessageAsync(context);
            }
        }

        public static Task HandleExceptionMessageAsync(HttpContext context)
        {

            string response = JsonSerializer.Serialize(new ValidationProblemDetails()
            {
                Title = "Something went wrong",
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = context.Request.Path
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(response);
        }
    }
}
