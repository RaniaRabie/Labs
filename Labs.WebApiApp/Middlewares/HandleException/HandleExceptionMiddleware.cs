namespace Labs.WebApiApp.Middlewares.HandleException
{
    public class HandleExceptionMiddleware
    {
        RequestDelegate _next;
        public HandleExceptionMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            try
            {
                await _next(context);
            }
            catch(Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");

            }
        }
    }
}
