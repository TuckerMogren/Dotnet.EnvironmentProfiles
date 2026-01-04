namespace Environments.EnvironmentMappings.Models;

/// <summary>
/// Defines an environment profile name and its canonical environment mapping.
/// </summary>
public sealed record EnvironmentProfile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentProfile"/> record.
    /// </summary>
    /// <param name="name">The profile name.</param>
    /// <param name="canonicalEnvironment">The canonical environment for the profile.</param>
    public EnvironmentProfile(string name, CanonicalEnvironment canonicalEnvironment)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Profile name cannot be null or whitespace.", nameof(name));
        }

        Name = name;
        CanonicalEnvironment = canonicalEnvironment;
    }

    /// <summary>
    /// Gets the profile name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the canonical environment for this profile.
    /// </summary>
    public CanonicalEnvironment CanonicalEnvironment { get; }
}
