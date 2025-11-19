using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Infrastructure.Data;
using RosaryForToday.Infrastructure.DbQueries;
using SimpleCqrs;
using RosaryForToday.Application.QueryHandlers;
using RosaryForToday.Presentation.ViewModels;
using RosaryForToday.Presentation.Views;

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

        // Register application handler for direct use in ViewModel
        services.AddTransient<RosaryForToday.Application.QueryHandlers.GetRosaryForTodayQueryHandler>();

        // Register SimpleCqrs mediator using the application handlers assembly
        services.AddScoped<ISimpleMediator>(scope => new SimpleMediator(typeof(GetRosaryForTodayQueryHandler).Assembly));

        // Register presentation viewmodels and views
        services.AddTransient<RosaryListViewModel>();
        services.AddTransient<RosaryListView>();

        // If you use AutoMapper, register here (optional)
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}