using Dotnet.EnvironmentProfiles;
using Microsoft.Extensions.Hosting;

namespace Dotnet.EnvironmentProfiles.Extensions;

public static class HostEnvironmentExtensions
{
    public static EnvironmentProfile GetEnvironmentProfile(
        this IHostEnvironment hostEnvironment,
        IEnvironmentProfileResolver resolver)
    {
        if (hostEnvironment is null)
        {
            throw new ArgumentNullException(nameof(hostEnvironment));
        }

        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }

        return resolver.Resolve(hostEnvironment.EnvironmentName);
    }
}
