
using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Models;
using ThriveKid.API.Services;

namespace ThriveKid.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ThriveKidContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register the ChildService for dependency injection
            // This allows us to use IChildService in controllers or other services
            // This line tells .NET: “Whenever I need IChildService, give me ChildService.”
            builder.Services.AddScoped<IChildService, ChildService>();
            builder.Services.AddScoped<IMilestoneService, MilestoneService>();

            // Register the DbContext with the service container
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            try
            {
                // 👇 Seed the database safely (catch errors during startup)
                SeedData.Initialize(app.Services);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Seeding error: {ex.Message}");
            }

            app.Run();
        }
    }
}
