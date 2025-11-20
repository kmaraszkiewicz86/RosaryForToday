using RosaryForToday.Presentation.ViewModels;
using RosaryForToday.Presentation.Views;

namespace RosaryForToday.Configuration;

public static class DependencyExtension
{
    /// <summary>
    /// Adds presentation view models and views required for the Rosary feature to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which the view models and views will be registered. Cannot be null.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance with the Rosary view models and views registered.</returns>
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddTransient<RosaryListViewModel>();
        services.AddTransient<RosaryListView>();

        return services;
    }
}