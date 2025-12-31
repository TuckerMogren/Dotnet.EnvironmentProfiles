namespace Environments.EnvironmentMappings;

public sealed class EnvironmentProfileResolverOptions
{
    private readonly Dictionary<string, EnvironmentProfile> _profiles =
        new(StringComparer.OrdinalIgnoreCase);

    public EnvironmentProfileResolverOptions()
    {
        AddProfiles(EnvironmentProfileDefaults.Profiles);
    }

    public IReadOnlyCollection<EnvironmentProfile> Profiles => _profiles.Values;

    public UnknownEnvironmentBehavior UnknownEnvironmentBehavior { get; set; } =
        UnknownEnvironmentBehavior.Throw;

    public CanonicalEnvironment FallbackCanonicalEnvironment { get; set; } =
        CanonicalEnvironment.Production;

    public void ClearProfiles() => _profiles.Clear();

    public void SetProfile(EnvironmentProfile profile)
    {
        if (profile is null)
        {
            throw new ArgumentNullException(nameof(profile));
        }

        _profiles[profile.Name] = profile;
    }

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
