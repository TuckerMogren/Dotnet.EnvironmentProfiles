using Environments.EnvironmentMappings;
using Microsoft.Extensions.DependencyInjection;

namespace Environments.EnvironmentMappings.Extensions;

public static class ServiceCollectionExtensions
{
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
