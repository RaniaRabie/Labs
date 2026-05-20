
using Labs.WebApiApp.Context;
using Labs.WebApiApp.Filters;
using Labs.WebApiApp.Middlewares.HandleException;
using Labs.WebApiApp.Middlewares.Logging;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Labs.WebApiApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<MyExceptionFilter>();
                //options.Filters.Add<MyResultFilter>();
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            /* ------------------------------------------------------------------------------- */

            var connectionString = builder.Configuration.GetConnectionString("cs");

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

            /* ------------------------------------------------------------------------------- */

            builder.Services.AddOpenApi();

            /* ------------------------------------------------------------------------------- */

            // Add CROS Services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();

                });

            });

            /* ------------------------------------------------------------------------------- */


            var app = builder.Build();


            // Configure the HTTP request pipeline.

            app.UseHandleException();

            //app.UseMiddleware<LoggingMiddleware>(); // 
            //app.UseLogging();


            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            //app.UseHttpsRedirection();

            /* ------------------------------------------------------------------------------- */

            //UseCors must be placed after UseRouting but before UseAuthorization
            app.UseCors("AllowAll");

            /* ------------------------------------------------------------------------------- */

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
