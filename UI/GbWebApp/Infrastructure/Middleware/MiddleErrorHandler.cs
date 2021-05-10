using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GbWebApp.Infrastructure.Middleware
{
    public class MiddleErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddleErrorHandler> _logger;

        public MiddleErrorHandler(RequestDelegate next, ILogger<MiddleErrorHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        private void HandleException(Exception ex, HttpContext ctx) =>
            _logger.LogError(ex, $"Request processing error! details: {ctx.Request.Path}");

        public async Task InvokeAsync(HttpContext ctx)
        {
            try {
                await _next(ctx);
            }
            catch (Exception ex) {
                HandleException(ex, ctx);
                throw /*ex // do not rethrow an exception!*/;
            }
        }
    }
}
