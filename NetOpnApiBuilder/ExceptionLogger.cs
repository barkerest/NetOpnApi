using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace NetOpnApiBuilder
{
    public class ExceptionLogger
    {
        private readonly RequestDelegate _next;
        private readonly ILogger         _logger;

        public ExceptionLogger(RequestDelegate next, ILogger<ExceptionLogger> logger)
        {
            _next   = next ?? throw new ArgumentException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"({e.GetType()})\n{e.Message}\n{e.StackTrace}");
                
                throw;
            }
        }
    }
}
