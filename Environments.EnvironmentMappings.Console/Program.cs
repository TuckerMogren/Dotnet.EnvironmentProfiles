using Environments.EnvironmentMappings;
using Environments.EnvironmentMappings.Extensions;
using Microsoft.Extensions.Configuration;

var resolver = new EnvironmentProfileResolver(new EnvironmentProfileResolverOptions());

var dotnetEnvironment = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DotnetEnvironment);
var aspnetEnvironment = Environment.GetEnvironmentVariable(EnvironmentVariableNames.AspNetCoreEnvironment);
var configEnvironment = dotnetEnvironment ?? aspnetEnvironment;

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

if (!string.IsNullOrWhiteSpace(configEnvironment))
{
    configBuilder.AddJsonFile($"appsettings.{configEnvironment}.json", optional: true, reloadOnChange: false);
}

var configuration = configBuilder
    .AddEnvironmentVariables()
    .Build();

var configuredName = configuration["EnvironmentMappings:EnvironmentName"];

try
{
    EnvironmentProfile profile;

    if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
    {
        profile = resolver.Resolve(args[0]);
    }
    else if (!string.IsNullOrWhiteSpace(configuredName))
    {
        profile = resolver.Resolve(configuredName);
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
