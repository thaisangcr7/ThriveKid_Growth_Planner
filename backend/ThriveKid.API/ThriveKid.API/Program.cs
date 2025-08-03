
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Data;
using ThriveKid.API.Services.Implementations;
using ThriveKid.API.Services.Interfaces;
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
            builder.Services.AddScoped<IFeedingLogService, FeedingLogService>();
            builder.Services.AddScoped<ISleepLogService, SleepLogService>();
            builder.Services.AddScoped<IReminderService, ReminderService>();


            // Register the SleepLogService for dependency injection
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            // Register the DbContext with the service container
            var app = builder.Build();
                       // <-- Required for Swagger

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
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    SeedData.Initialize(services); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Seeding error: {ex.Message}");
            }

            // Run the application
            app.MapGet("/", () => "ThriveKid API is running!");
            app.Run();
        }
    }
}
