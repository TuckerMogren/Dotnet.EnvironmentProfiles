using Environments.EnvironmentMappings.Abstractions;
using Environments.EnvironmentMappings.Exceptions;
using Environments.EnvironmentMappings.Models;
using Environments.EnvironmentMappings.Options;

namespace Environments.EnvironmentMappings.Resolvers;

/// <summary>
/// Resolves environment names to configured profiles.
/// </summary>
/// <param name="options">The resolver options to use.</param>
public sealed class EnvironmentProfileResolver(EnvironmentProfileResolverOptions options) : IEnvironmentProfileResolver
{
    private readonly EnvironmentProfileResolverOptions _options = options ?? throw new ArgumentNullException(nameof(options));

    /// <summary>
    /// Resolves an environment name to an <see cref="EnvironmentProfile"/>.
    /// </summary>
    /// <param name="environmentName">The environment name to resolve.</param>
    /// <returns>The matching environment profile.</returns>
    public EnvironmentProfile Resolve(string environmentName)
    {
        if (string.IsNullOrWhiteSpace(environmentName))
        {
            throw new ArgumentException("Environment name cannot be null or whitespace.", nameof(environmentName));
        }

        if (_options.TryGetProfile(environmentName, out var profile))
        {
            return profile;
        }

        if (_options.UnknownEnvironmentBehavior == UnknownEnvironmentBehavior.Throw)
        {
            throw new EnvironmentProfileResolutionException(environmentName);
        }

        return new EnvironmentProfile(environmentName, _options.FallbackCanonicalEnvironment);
    }
}
