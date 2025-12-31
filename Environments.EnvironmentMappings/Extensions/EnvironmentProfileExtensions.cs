using Environments.EnvironmentMappings;

namespace Environments.EnvironmentMappings.Extensions;

public static class EnvironmentProfileExtensions
{
    public static bool IsQa(this EnvironmentProfile profile)
    {
        if (profile is null)
        {
            throw new ArgumentNullException(nameof(profile));
        }

        return string.Equals(profile.Name, EnvironmentProfileNames.Qa, StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsUat(this EnvironmentProfile profile)
    {
        if (profile is null)
        {
            throw new ArgumentNullException(nameof(profile));
        }

        return string.Equals(profile.Name, EnvironmentProfileNames.Uat, StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsNonProduction(this EnvironmentProfile profile)
    {
        if (profile is null)
        {
            throw new ArgumentNullException(nameof(profile));
        }

        return profile.CanonicalEnvironment != CanonicalEnvironment.Production;
    }
}
