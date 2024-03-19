using BookCollect.Data;
using BookCollect.Exceptions;
using BookCollect.Models;
using BookCollect.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace BookCollect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            //Serilog configuration file
            Serilog.Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(Configuration)
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

            //Add Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //Add Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //Add Jwt bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Secret"])),

                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JWT:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:Audience"]

                };
            });

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

            //Authentication and Authorization
            app.UseAuthentication();
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
