using Environments.EnvironmentMappings;

var resolver = new EnvironmentProfileResolver(new EnvironmentProfileResolverOptions());

var dotnetEnvironment = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DotnetEnvironment);
var aspnetEnvironment = Environment.GetEnvironmentVariable(EnvironmentVariableNames.AspNetCoreEnvironment);

Console.WriteLine($"DOTNET_ENVIRONMENT: {dotnetEnvironment ?? "(not set)"}");
Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {aspnetEnvironment ?? "(not set)"}");

try
{
    EnvironmentProfile profile;

    if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
    {
        profile = resolver.Resolve(args[0]);
    }
    else
    {
        profile = resolver.ResolveFromEnvironmentVariables();
    }

    Console.WriteLine($"Resolved profile: {profile.Name}");
    Console.WriteLine($"Canonical environment: {profile.CanonicalEnvironment}");
    Console.WriteLine($"Is non-production: {profile.IsNonProduction()}");
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.Message);
    Console.Error.WriteLine("Provide an environment name argument or set DOTNET_ENVIRONMENT/ASPNETCORE_ENVIRONMENT.");
    Environment.ExitCode = 1;
}
