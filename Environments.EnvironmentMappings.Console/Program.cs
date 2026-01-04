using Environments.EnvironmentMappings.Extensions;
using Environments.EnvironmentMappings.Models;
using Environments.EnvironmentMappings.Options;
using Environments.EnvironmentMappings.Resolvers;
using Microsoft.Extensions.Configuration;

var resolver = new EnvironmentProfileResolver(new EnvironmentProfileResolverOptions());


var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables()
    .Build();

var configuredName = configBuilder["EnvironmentMappings:EnvironmentName"] ?? "";

try
{
    
    EnvironmentProfile profile = resolver.Resolve(configuredName);
    profile.IsDevelopment();
    profile.IsStaging();
    profile.IsProduction();
    profile.IsNonProduction();
    profile.IsUat();
    profile.IsQa();
    Console.WriteLine($"Resolved profile: {profile.Name}");
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.Message);
    Console.Error.WriteLine("Provide an environment name argument or set DOTNET_ENVIRONMENT/ASPNETCORE_ENVIRONMENT.");
    Environment.ExitCode = 1;
}
