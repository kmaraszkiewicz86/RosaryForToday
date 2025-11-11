using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RosaryForToday.Application.QueryHandlers;
using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Infrastructure.Data;
using RosaryForToday.Infrastructure.DbQueries;
using SimpleCqrs;

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

        // Register Rosary DB query
        services.AddScoped<IRosaryDbQuery, RosaryDbQuery>();
        services.AddScoped<ISimpleMediator>(scope => new SimpleMediator(typeof(GetRosaryForTodayQueryHandler).Assembly));

        // If you use AutoMapper, register here (optional)
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}