using System.Diagnostics;

namespace Student_Management_API.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly RequestDelegate _next;
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await _next(context);
            stopWatch.Stop();
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var method = context.Request.Method;
            var path = context.Request.Path;
            var status = context.Response.StatusCode;
            var timeMs = stopWatch.ElapsedMilliseconds;
            _logger.LogInformation($"[{time}] {method} {path} -> {status} {(System.Net.HttpStatusCode)status} (took {timeMs}ms)");
        }
    }
}

