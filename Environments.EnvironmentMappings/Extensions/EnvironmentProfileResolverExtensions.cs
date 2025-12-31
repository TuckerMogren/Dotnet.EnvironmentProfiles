using Environments.EnvironmentMappings;

namespace Environments.EnvironmentMappings.Extensions;

public static class EnvironmentProfileResolverExtensions
{
    public static EnvironmentProfile ResolveFromEnvironmentVariables(this IEnvironmentProfileResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }

        var environmentName = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DotnetEnvironment);
        if (string.IsNullOrWhiteSpace(environmentName))
        {
            environmentName = Environment.GetEnvironmentVariable(EnvironmentVariableNames.AspNetCoreEnvironment);
        }

        if (string.IsNullOrWhiteSpace(environmentName))
        {
            throw new InvalidOperationException("Neither DOTNET_ENVIRONMENT nor ASPNETCORE_ENVIRONMENT is set.");
        }

        return resolver.Resolve(environmentName);
    }
}
