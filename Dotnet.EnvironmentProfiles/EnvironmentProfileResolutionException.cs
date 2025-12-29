namespace Dotnet.EnvironmentProfiles;

public sealed class EnvironmentProfileResolutionException : InvalidOperationException
{
    public EnvironmentProfileResolutionException(string environmentName)
        : base($"No environment profile is configured for '{environmentName}'.")
    {
        EnvironmentName = environmentName;
    }

    public string EnvironmentName { get; }
}
