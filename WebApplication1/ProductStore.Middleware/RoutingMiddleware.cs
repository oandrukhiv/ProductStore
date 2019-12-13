using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Serilog;

namespace ProductStore.Middleware
{
    public class RoutingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        public RoutingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.StartsWith("/api"))
            {
                var method = context.Request.Method;
                var route = context.Request.Path.Value;

                var watch = new Stopwatch();
                watch.Start();
      
                context.Response.OnCompleted(() => {
                    watch.Stop();
                    var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;

                    _logger.Information($"{method} {route}. Request run {responseTimeForCompleteRequest}");

                    return Task.CompletedTask;
                });
            }
            
            await _next.Invoke(context);
        }
    }
}
