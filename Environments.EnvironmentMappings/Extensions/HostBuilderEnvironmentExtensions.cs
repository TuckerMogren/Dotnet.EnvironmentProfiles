using System.Collections.Generic;
using Environments.EnvironmentMappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Environments.EnvironmentMappings.Extensions;

public static class HostBuilderEnvironmentExtensions
{
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
