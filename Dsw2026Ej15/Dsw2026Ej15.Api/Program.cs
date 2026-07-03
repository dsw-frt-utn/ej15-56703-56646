using Dsw2026Ej15.Data;
using Microsoft.EntityFrameworkCore;
namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHealthChecks();
            builder.Services.AddScoped<IPersistence, PersistenceEf>();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            
            
            builder.Services.AddDbContext<Dsw2026Ej15DbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            var app = builder.Build();


            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.MapHealthChecks("/health-check");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
