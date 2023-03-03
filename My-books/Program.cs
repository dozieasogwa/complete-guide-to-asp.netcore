using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using My_books.Data;
using My_books.Data.Services;
using My_books.Exceptions;
using Serilog;

namespace My_books
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
           // try
           // {
           //     Log.Logger = new LoggerConfiguration()
           //         .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
           //         .CreateLogger();
           // }
           //finally
           // {
           //     Log.CloseAndFlush();
           // }
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<BooksService>();
            builder.Services.AddTransient<AuthorsService>();
            builder.Services.AddTransient<PublishersService>();
            builder.Services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;

                //config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");
                //config.ApiVersionReader = new MediaTypeApiVersionReader();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();

            app.UseAuthorization();

            //Execption Handling
            app.ConfigureBuildInExceotionHandler();
            //app.configureCustomExceptionHandler();


            app.MapControllers();

           //AppDbInitializer.Seed(app);

            app.Run();
        }

        
    }
}