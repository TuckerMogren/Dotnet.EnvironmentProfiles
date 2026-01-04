using Environments.EnvironmentMappings.Models;

namespace Environments.EnvironmentMappings.Abstractions;

/// <summary>
/// Resolves environment names to canonical profiles.
/// </summary>
public interface IEnvironmentProfileResolver
{
    /// <summary>
    /// Resolves an environment name to an <see cref="EnvironmentProfile"/>.
    /// </summary>
    /// <param name="environmentName">The environment name to resolve.</param>
    /// <returns>The matching environment profile.</returns>
    EnvironmentProfile Resolve(string environmentName);
}
