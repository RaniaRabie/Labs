namespace Labs.WebApiApp.Middlewares.HandleException
{
    public static class UseHandleExceptionExtension
    {
        public static void UseHandleException(this IApplicationBuilder app) {
            app.UseMiddleware<HandleExceptionMiddleware>();
        }

    }
}
