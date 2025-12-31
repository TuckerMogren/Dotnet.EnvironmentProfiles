namespace Environments.EnvironmentMappings;

public sealed class EnvironmentProfileResolver(EnvironmentProfileResolverOptions options) : IEnvironmentProfileResolver
{
    private readonly EnvironmentProfileResolverOptions _options = options ?? throw new ArgumentNullException(nameof(options));

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
