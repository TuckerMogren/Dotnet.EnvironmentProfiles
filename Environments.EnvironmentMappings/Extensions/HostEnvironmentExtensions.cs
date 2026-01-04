using Environments.EnvironmentMappings.Abstractions;
using Environments.EnvironmentMappings.Models;
using Microsoft.Extensions.Hosting;

namespace Environments.EnvironmentMappings.Extensions;

/// <summary>
/// Extension methods for <see cref="IHostEnvironment"/>.
/// </summary>
public static class HostEnvironmentExtensions
{
    /// <summary>
    /// Resolves the environment profile for the current host environment.
    /// </summary>
    /// <param name="hostEnvironment">The host environment to read.</param>
    /// <param name="resolver">The resolver to use.</param>
    /// <returns>The resolved environment profile.</returns>
    public static EnvironmentProfile GetEnvironmentProfile(
        this IHostEnvironment hostEnvironment,
        IEnvironmentProfileResolver resolver)
    {
        ArgumentNullException.ThrowIfNull(hostEnvironment);

        ArgumentNullException.ThrowIfNull(resolver);

        return resolver.Resolve(hostEnvironment.EnvironmentName);
    }
}
