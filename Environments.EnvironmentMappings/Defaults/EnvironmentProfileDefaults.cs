using Environments.EnvironmentMappings.Constants;
using Environments.EnvironmentMappings.Models;

namespace Environments.EnvironmentMappings.Defaults;

/// <summary>
/// Provides default environment profile mappings.
/// </summary>
public static class EnvironmentProfileDefaults
{
    /// <summary>
    /// Gets the default profiles that map to canonical environments.
    /// </summary>
    public static IReadOnlyList<EnvironmentProfile> Profiles { get; } =
        new List<EnvironmentProfile>
        {
            new(EnvironmentProfileNames.Development, CanonicalEnvironment.Development),
            new(EnvironmentProfileNames.Local, CanonicalEnvironment.Development),
            new(EnvironmentProfileNames.Cde, CanonicalEnvironment.Development),
            new(EnvironmentProfileNames.Staging, CanonicalEnvironment.Staging),
            new(EnvironmentProfileNames.Qa, CanonicalEnvironment.Staging),
            new(EnvironmentProfileNames.QualityAssurance, CanonicalEnvironment.Staging),
            new(EnvironmentProfileNames.Uat, CanonicalEnvironment.Staging),
            new(EnvironmentProfileNames.PreProd, CanonicalEnvironment.Staging),
            new(EnvironmentProfileNames.Production, CanonicalEnvironment.Production)
        }.AsReadOnly();
}
