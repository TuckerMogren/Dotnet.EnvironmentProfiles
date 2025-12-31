namespace Environments.EnvironmentMappings;

public interface IEnvironmentProfileResolver
{
    EnvironmentProfile Resolve(string environmentName);
}
