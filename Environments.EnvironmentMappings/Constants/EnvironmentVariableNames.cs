
using System.Collections.Specialized;
using System.Reflection.Metadata;
using System.Text;

namespace Environments.EnvironmentMappings.Constants;

/// <summary>
/// Provides environment variable names used to resolve environment profiles.
/// </summary>
public static class EnvironmentVariableNames
{
    /// <summary>
    /// The .NET environment variable name.
    /// </summary>
    public const string DotnetEnvironment = "DOTNET_ENVIRONMENT";
    /// <summary>
    /// The ASP.NET Core environment variable name.
    /// </summary>
    public const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
}
