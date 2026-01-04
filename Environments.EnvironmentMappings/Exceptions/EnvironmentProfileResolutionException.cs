namespace Environments.EnvironmentMappings;

/// <summary>
/// The exception thrown when an environment name cannot be resolved to a profile.
/// </summary>
/// <param name="environmentName">The environment name that failed to resolve.</param>
public sealed class EnvironmentProfileResolutionException(string environmentName)
    : InvalidOperationException($"No environment profile is configured for '{environmentName}'.")
{
    /// <summary>
    /// Gets the environment name that failed to resolve.
    /// </summary>
    public string EnvironmentName { get; } = environmentName;
}
