namespace Dotnet.EnvironmentProfiles;

public interface IEnvironmentProfileResolver
{
    EnvironmentProfile Resolve(string environmentName);
}
