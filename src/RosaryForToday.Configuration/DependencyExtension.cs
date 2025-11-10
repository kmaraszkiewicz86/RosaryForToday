using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RosaryForToday.Infrastructure.Data;
using RosaryForToday.Application.CommandHandlers;
using RosaryForToday.Application.QueryHandlers;

namespace RosaryForToday.Configuration;

public static class DependencyExtension
{
    // Call from your Host/Startup: services.AddRosaryForToday(Configuration)
    public static IServiceCollection AddRosaryForToday(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? "Data Source=rosary.db";
        services.AddDbContext<RosaryDbContext>(opts =>
            opts.UseSqlite(connectionString));

        // Register application handlers (concrete registration so DI can resolve handlers)
        services.AddScoped<CreateRosaryTypeCommandHandler>();
        services.AddScoped<GetRosaryForTodayQueryHandler>();

        // If you use AutoMapper, register here (optional)
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}