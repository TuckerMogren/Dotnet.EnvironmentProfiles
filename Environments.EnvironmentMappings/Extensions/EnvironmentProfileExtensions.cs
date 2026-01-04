using Environments.EnvironmentMappings.Constants;
using Environments.EnvironmentMappings.Models;

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

    /// <summary>
    /// Determines whether the profile maps to the development canonical environment.
    /// </summary>
    /// <param name="profile">The profile to evaluate.</param>
    /// <returns><see langword="true"/> if the profile is development; otherwise, <see langword="false"/>.</returns>
    public static bool IsDevelopment(this EnvironmentProfile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);

        return profile.CanonicalEnvironment == CanonicalEnvironment.Development;
    }

    /// <summary>
    /// Determines whether the profile maps to the staging canonical environment.
    /// </summary>
    /// <param name="profile">The profile to evaluate.</param>
    /// <returns><see langword="true"/> if the profile is staging; otherwise, <see langword="false"/>.</returns>
    public static bool IsStaging(this EnvironmentProfile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);

        return profile.CanonicalEnvironment == CanonicalEnvironment.Staging;
    }

    /// <summary>
    /// Determines whether the profile maps to the production canonical environment.
    /// </summary>
    /// <param name="profile">The profile to evaluate.</param>
    /// <returns><see langword="true"/> if the profile is production; otherwise, <see langword="false"/>.</returns>
    public static bool IsProduction(this EnvironmentProfile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);

        return profile.CanonicalEnvironment == CanonicalEnvironment.Production;
    }

    /// <summary>
    /// Determines whether the profile maps to the performance canonical environment.
    /// </summary>
    /// <param name="profile">The profile to evaluate.</param>
    /// <returns><see langword="true"/> if the profile is performance; otherwise, <see langword="false"/>.</returns>
    public static bool IsPerformance(this EnvironmentProfile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);

        return profile.CanonicalEnvironment == CanonicalEnvironment.Performance;
    }

    /// <summary>
    /// Determines whether the profile matches the specified environment name.
    /// </summary>
    /// <param name="profile">The profile to evaluate.</param>
    /// <param name="environmentName">
    /// The environment name to compare. This matches either the profile name or the canonical environment name.
    /// </param>
    /// <returns><see langword="true"/> if the profile matches the environment name; otherwise, <see langword="false"/>.</returns>
    public static bool IsEnvironment(this EnvironmentProfile profile, string environmentName)
    {
        ArgumentNullException.ThrowIfNull(profile);
        ArgumentNullException.ThrowIfNull(environmentName);

        return string.Equals(profile.Name, environmentName, StringComparison.OrdinalIgnoreCase)
            || string.Equals(profile.CanonicalEnvironment.ToString(), environmentName, StringComparison.OrdinalIgnoreCase);
    }
}
