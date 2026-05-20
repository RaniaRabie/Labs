using System.Diagnostics;

namespace Labs.WebApiApp.Middlewares.Logging
{
    public class LoggingMiddleware
    {
        RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            Console.WriteLine($"Request Path: {context.Request.Path}, Method: {context.Request.Method}");
            await _next(context);

            stopwatch.Stop();
            Console.WriteLine($"Status Code: {context.Response.StatusCode}, Time Taken: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
