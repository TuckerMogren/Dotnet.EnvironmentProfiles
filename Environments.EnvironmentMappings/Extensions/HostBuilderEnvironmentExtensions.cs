using System.Collections.Generic;
using Environments.EnvironmentMappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Environments.EnvironmentMappings.Extensions;

/// <summary>
/// Extension methods for <see cref="IHostBuilder"/> to apply canonical environment mappings.
/// </summary>
public static class HostBuilderEnvironmentExtensions
{
    /// <summary>
    /// Maps the current environment name to a canonical environment and applies it to the host configuration.
    /// </summary>
    /// <param name="hostBuilder">The host builder to configure.</param>
    /// <param name="configure">An optional callback to configure resolver options.</param>
    /// <returns>The configured host builder.</returns>
    public static IHostBuilder UseCanonicalEnvironmentMappings(
        this IHostBuilder hostBuilder,
        Action<EnvironmentProfileResolverOptions>? configure = null)
    {
        if (hostBuilder is null)
        {
            throw new ArgumentNullException(nameof(hostBuilder));
        }

        var options = new EnvironmentProfileResolverOptions();
        configure?.Invoke(options);

        var resolver = new EnvironmentProfileResolver(options);
        var environmentName = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DotnetEnvironment);
        if (string.IsNullOrWhiteSpace(environmentName))
        {
            environmentName = Environment.GetEnvironmentVariable(EnvironmentVariableNames.AspNetCoreEnvironment);
        }

        if (string.IsNullOrWhiteSpace(environmentName))
        {
            return hostBuilder;
        }

        var canonicalName = resolver.Resolve(environmentName).CanonicalEnvironment.ToString();

        hostBuilder.ConfigureHostConfiguration(builder =>
        {
            var settings = new Dictionary<string, string?>
            {
                [HostDefaults.EnvironmentKey] = canonicalName
            };

            builder.AddInMemoryCollection(settings);
        });

        return hostBuilder;
    }
}
