namespace Environments.EnvironmentMappings;

/// <summary>
/// Configures how environment profiles are resolved.
/// </summary>
public sealed class EnvironmentProfileResolverOptions
{
    private readonly Dictionary<string, EnvironmentProfile> _profiles =
        new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentProfileResolverOptions"/> class.
    /// </summary>
    public EnvironmentProfileResolverOptions()
    {
        AddProfiles(EnvironmentProfileDefaults.Profiles);
    }

    /// <summary>
    /// Gets the configured environment profiles.
    /// </summary>
    public IReadOnlyCollection<EnvironmentProfile> Profiles => _profiles.Values;

    /// <summary>
    /// Gets or sets the behavior when no profile matches an environment name.
    /// </summary>
    public UnknownEnvironmentBehavior UnknownEnvironmentBehavior { get; set; } =
        UnknownEnvironmentBehavior.Throw;

    /// <summary>
    /// Gets or sets the canonical environment used when falling back.
    /// </summary>
    public CanonicalEnvironment FallbackCanonicalEnvironment { get; set; } =
        CanonicalEnvironment.Production;

    /// <summary>
    /// Removes all configured profiles.
    /// </summary>
    public void ClearProfiles() => _profiles.Clear();

    /// <summary>
    /// Adds or replaces a single environment profile.
    /// </summary>
    /// <param name="profile">The profile to add or replace.</param>
    public void SetProfile(EnvironmentProfile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);

        _profiles[profile.Name] = profile;
    }

    /// <summary>
    /// Adds or replaces multiple environment profiles.
    /// </summary>
    /// <param name="profiles">The profiles to add or replace.</param>
    public void AddProfiles(IEnumerable<EnvironmentProfile> profiles)
    {
        ArgumentNullException.ThrowIfNull(profiles);

        foreach (var profile in profiles)
        {
            SetProfile(profile);
        }
    }

    internal bool TryGetProfile(string environmentName, out EnvironmentProfile profile) =>
        _profiles.TryGetValue(environmentName, out profile!);
}
