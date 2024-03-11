using BookCollect.Data;
using BookCollect.Exceptions;
using BookCollect.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BookCollect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
                //Serilog configuration file
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
          

            //Serilog Configuration
            //Log.Logger = new LoggerConfiguration()
            //   .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day )
            //    .CreateLogger();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
            )); 
            builder.Services.AddTransient<BookService>();
            builder.Services.AddTransient<AuthorService>(); 
            builder.Services.AddTransient<PublisherService>();
            builder.Services.AddSerilog();

           
            // Inject Version of Apiversioning
            //builder.Services.AddApiVersioning(config =>
            //{
            //    config.DefaultApiVersion = new ApiVersion(1, 0);
            //    config.AssumeDefaultVersionWhenUnspecified = true;

            //    config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");
            //}
            //)


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Serilog
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //Exception Handling
            app.ConfigurationBuildInExceptionHandler();
            //app.ConfigureCustomExceptionHandler();

            //Added seed to container
            AppDbInitalizer.Seed(app);

            app.MapControllers();


            app.Run();
        }
    }
}
