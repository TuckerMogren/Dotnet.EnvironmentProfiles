using Environments.EnvironmentMappings;

namespace Environments.EnvironmentMappings.Extensions;

/// <summary>
/// Extension methods for <see cref="EnvironmentProfile"/>.
/// </summary>
public static class EnvironmentProfileExtensions
{
    /// <summary>
    /// Determines whether the profile represents QA.
    /// </summary>
    /// <param name="profile">The profile to evaluate.</param>
    /// <returns><see langword="true"/> if the profile is QA; otherwise, <see langword="false"/>.</returns>
    public static bool IsQa(this EnvironmentProfile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);

        return string.Equals(profile.Name, EnvironmentProfileNames.Qa, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Determines whether the profile represents UAT.
    /// </summary>
    /// <param name="profile">The profile to evaluate.</param>
    /// <returns><see langword="true"/> if the profile is UAT; otherwise, <see langword="false"/>.</returns>
    public static bool IsUat(this EnvironmentProfile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);

        return string.Equals(profile.Name, EnvironmentProfileNames.Uat, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Determines whether the profile maps to a non-production canonical environment.
    /// </summary>
    /// <param name="profile">The profile to evaluate.</param>
    /// <returns><see langword="true"/> if the profile is not production; otherwise, <see langword="false"/>.</returns>
    public static bool IsNonProduction(this EnvironmentProfile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);

        return profile.CanonicalEnvironment != CanonicalEnvironment.Production;
    }
}
