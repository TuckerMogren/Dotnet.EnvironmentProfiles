namespace Environments.EnvironmentMappings;

public sealed class EnvironmentProfileResolutionException(string environmentName) : InvalidOperationException($"No environment profile is configured for '{environmentName}'.")
{
    public string EnvironmentName { get; } = environmentName;
}
