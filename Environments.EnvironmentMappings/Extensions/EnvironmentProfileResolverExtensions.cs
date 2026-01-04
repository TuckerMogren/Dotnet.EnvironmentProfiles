using Environments.EnvironmentMappings;

namespace Environments.EnvironmentMappings.Extensions;

/// <summary>
/// Extension methods for <see cref="IEnvironmentProfileResolver"/>.
/// </summary>
public static class EnvironmentProfileResolverExtensions
{
    /// <summary>
    /// Resolves the environment profile from well-known environment variables.
    /// </summary>
    /// <param name="resolver">The resolver to use.</param>
    /// <returns>The resolved environment profile.</returns>
    public static EnvironmentProfile ResolveFromEnvironmentVariables(this IEnvironmentProfileResolver resolver)
    {
        ArgumentNullException.ThrowIfNull(resolver);

        var environmentName = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DotnetEnvironment);
        if (string.IsNullOrWhiteSpace(environmentName))
        {
            environmentName = Environment.GetEnvironmentVariable(EnvironmentVariableNames.AspNetCoreEnvironment);
        }

        if (string.IsNullOrWhiteSpace(environmentName))
        {
            throw new InvalidOperationException("Neither DOTNET_ENVIRONMENT nor ASPNETCORE_ENVIRONMENT is set.");
        }

        return resolver.Resolve(environmentName);
    }
}
