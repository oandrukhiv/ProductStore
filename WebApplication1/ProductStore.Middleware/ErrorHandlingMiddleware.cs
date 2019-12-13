using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProductStore.Services.Commands.ProductRelated;
using Serilog;

namespace ProductStore.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;
        private readonly ILogger _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is InvalidProductException)
            {
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                await context.Response.WriteAsync("Bad request bro. Try with normal request data. Hello from Taras. chair in your face :)"+ exception.Message);
            }
            var response = context.Response;
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Bad request bro. Try with normal request data. Hello from Taras. chair :)");
        }
        
    }
}
