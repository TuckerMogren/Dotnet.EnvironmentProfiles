using Environments.EnvironmentMappings;
using Microsoft.Extensions.DependencyInjection;

namespace Environments.EnvironmentMappings.Extensions;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/> to register environment profile services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers environment profile resolution services in the container.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configure">An optional callback to configure resolver options.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection AddEnvironmentProfiles(
        this IServiceCollection services,
        Action<EnvironmentProfileResolverOptions>? configure = null)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var options = new EnvironmentProfileResolverOptions();
        configure?.Invoke(options);

        services.AddSingleton(options);
        services.AddSingleton<IEnvironmentProfileResolver, EnvironmentProfileResolver>();

        return services;
    }
}
