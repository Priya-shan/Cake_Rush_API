using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Cake_Rush_API.Data;
namespace Cake_Rush_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Cake_Rush_APIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Cake_Rush_APIContext") ?? throw new InvalidOperationException("Connection string 'Cake_Rush_APIContext' not found.")));
           
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:7090")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

            var app = builder.Build();
            app.UseCors("MyPolicy");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}