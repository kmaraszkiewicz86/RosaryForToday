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
    /// Configures the database context and applies pending migrations automatically.
    /// </summary>
    /// <param name="services">The service collection to which the database context will be added.</param>
    /// <param name="configuration">The application configuration used to retrieve the database connection string.</param>
    /// <returns>The updated service collection with the configured database context.</returns>
    /// <remarks>
    /// This method registers the RosaryDbContext with SQLite using either the connection string from configuration
    /// or a default "Data Source=rosary.db" value. After registration, it automatically applies any pending EF Core migrations.
    /// </remarks>
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string dbPath)
    {
        var connectionString = $"Data Source={dbPath}";

        services.AddDbContext<RosaryDbContext>(opts =>
            opts.UseSqlite(connectionString));

        services.MigrateDatabase();

        return services;
    }

    /// <summary>
    /// Registers application-level services including database queries, mediator, and AutoMapper into the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to which the application services will be added.</param>
    /// <returns>The updated service collection containing the registered application services.</returns>
    public static IServiceCollection AddRequiredClasses(this IServiceCollection services)
    {
        // Register Rosary DB query
        services.AddScoped<IRosaryDbQuery, RosaryDbQuery>();

        // Register SimpleCqrs mediator using the application handlers assembly
        services.AddScoped<ISimpleMediator>(scope => new SimpleMediator(typeof(GetRosaryForTodayQueryHandler).Assembly));

        // If you use AutoMapper, register here (optional)
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }

    private static IServiceCollection MigrateDatabase(this IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<RosaryDbContext>();
        db.Database.Migrate();

        return services;
    }
}