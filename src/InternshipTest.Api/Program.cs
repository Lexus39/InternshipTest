using InternshipTest.API.Mappings;
using InternshipTest.API.Middleware;
using InternshipTest.Application.Interfaces;
using InternshipTest.Application.Services;
using IntertnshipTest.DAL;
using IntertnshipTest.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Xml;
using System.Runtime.CompilerServices;

namespace InternshipTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version= "v1",
                    Title = "Internship test API",
                    Description = "API, которое позволяет добавлять, получать, изменять и удалять пользователей"
                });
                var basePath = Directory.GetCurrentDirectory();
                var path = Path.Combine(basePath, @"obj\Debug\net6.0\InternshipTest.API.xml");
                options.IncludeXmlComments(path);
            });

            var connectionString =  builder.Configuration.GetConnectionString("Default");

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddDbContext<InternshipContext>(options =>
             options.UseNpgsql(connectionString));

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCustomExceptionHandler();

            app.MapControllers();

            app.Run();
        }
    }
}