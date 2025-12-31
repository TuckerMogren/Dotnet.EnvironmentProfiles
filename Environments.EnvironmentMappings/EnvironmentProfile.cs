namespace Environments.EnvironmentMappings;

public sealed record EnvironmentProfile
{
    public EnvironmentProfile(string name, CanonicalEnvironment canonicalEnvironment)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Profile name cannot be null or whitespace.", nameof(name));
        }

        Name = name;
        CanonicalEnvironment = canonicalEnvironment;
    }

    public string Name { get; }
    public CanonicalEnvironment CanonicalEnvironment { get; }
}
