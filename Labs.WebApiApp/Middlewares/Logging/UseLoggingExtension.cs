namespace Labs.WebApiApp.Middlewares.Logging
{
    public static class UseLoggingExtinsion
    {
        public static void UseLogging(this IApplicationBuilder app) {
            app.UseMiddleware<LoggingMiddleware>();
        }
    }
}
