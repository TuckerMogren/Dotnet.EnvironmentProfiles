namespace Environments.EnvironmentMappings.Options;

/// <summary>
/// Defines how unknown environment names are handled.
/// </summary>
public enum UnknownEnvironmentBehavior
{
    /// <summary>
    /// Throw an exception when an environment name is not configured.
    /// </summary>
    Throw,
    /// <summary>
    /// Use the fallback canonical environment when an environment name is not configured.
    /// </summary>
    UseFallbackCanonicalEnvironment
}
