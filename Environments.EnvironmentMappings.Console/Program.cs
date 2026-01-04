using Environments.EnvironmentMappings.Abstractions;
using Environments.EnvironmentMappings.Extensions;
using Environments.EnvironmentMappings.Models;
using Environments.EnvironmentMappings.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddEnvironmentProfiles(options =>
        {
            options.UnknownEnvironmentBehavior = UnknownEnvironmentBehavior.Throw;
        });
    })
    .Build();

var configuration = host.Services.GetRequiredService<IConfiguration>();
var resolver = host.Services.GetRequiredService<IEnvironmentProfileResolver>();
var configuredName = configuration["EnvironmentMappings:EnvironmentName"];

try
{
    EnvironmentProfile profile = string.IsNullOrWhiteSpace(configuredName)
        ? resolver.ResolveFromEnvironmentVariables()
        : resolver.Resolve(configuredName);

    Console.WriteLine($"Resolved profile: {profile.Name}");
    Console.WriteLine($"Canonical environment: {profile.CanonicalEnvironment}");
    Console.WriteLine($"Is non-production: {profile.IsNonProduction()}");
    Console.WriteLine($"Is development: {profile.IsDevelopment()}");
    Console.WriteLine($"Is staging: {profile.IsStaging()}");
    Console.WriteLine($"Is production: {profile.IsProduction()}");
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.Message);
    Console.Error.WriteLine("Provide EnvironmentMappings:EnvironmentName or set DOTNET_ENVIRONMENT/ASPNETCORE_ENVIRONMENT.");
    Environment.ExitCode = 1;
}
