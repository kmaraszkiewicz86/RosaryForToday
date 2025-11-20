using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RosaryForToday.ApplicationLayer.QueryHandlers;
using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Infrastructure.Data;
using RosaryForToday.Infrastructure.DbQueries;
using SimpleCqrs;

namespace RosaryForToday.Configuration;

public static class DependencyExtension
{
    /// <summary>
    /// Registers required application services, including database context, query interfaces, mediator, and optional
    /// AutoMapper, into the specified service collection.
    /// </summary>
    /// <remarks>This method configures the application's dependency injection container with essential
    /// services for database access, query handling, and mediation. If AutoMapper is used in the application, it is
    /// also registered with all loaded assemblies. The database context is configured to use a SQLite connection,
    /// defaulting to 'Data Source=rosary.db' if no connection string is provided.</remarks>
    /// <param name="services">The service collection to which the required classes and dependencies will be added.</param>
    /// <param name="configuration">The application configuration used to retrieve the database connection string.</param>
    /// <returns>The updated service collection containing the registered services.</returns>
    public static IServiceCollection AddRequiredClasses(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? "Data Source=rosary.db";
        services.AddDbContext<RosaryDbContext>(opts =>
            opts.UseSqlite(connectionString));

        // Register Rosary DB query
        services.AddScoped<IRosaryDbQuery, RosaryDbQuery>();

        // Register SimpleCqrs mediator using the application handlers assembly
        services.AddScoped<ISimpleMediator>(scope => new SimpleMediator(typeof(GetRosaryForTodayQueryHandler).Assembly));

        // If you use AutoMapper, register here (optional)
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}