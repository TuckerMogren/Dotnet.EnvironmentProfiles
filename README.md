# Environments.EnvironmentMappings

[![Publish NuGet](https://github.com/TuckerMogren/Dotnet.EnvironmentProfiles/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/TuckerMogren/Dotnet.EnvironmentProfiles/actions/workflows/publish-nuget.yml)

Environments.EnvironmentMappings provides deterministic environment mapping for .NET applications.
It lets you keep real-world environment names (QA, UAT, PreProd, etc.) while mapping them to
canonical ASP.NET Core behavior (Development, Staging, Production).

## NuGet Package

Package ID: `Environments.EnvironmentMappings`. Namespaces: `Environments.EnvironmentMappings.*`.
Package metadata is defined in `Environments.EnvironmentMappings.nuspec`. The changelog is in
`CHANGELOG.md` and each release should update both the package version and `releaseNotes`.
Targets: `net6.0` and `net10.0`.

## Core Concepts

- EnvironmentProfile is the source of truth for environment semantics.
- IEnvironmentProfileResolver resolves raw environment names into profiles.
- Extension methods are thin conveniences and rely on resolved profiles.

## Default Profiles

The resolver ships with the following defaults. You can override or replace them via DI.

| Profile Name | Canonical Environment |
| :----------- | :-------------------- |
| CDE          | Development           |
| Development  | Development           |
| Local        | Development           |
| Staging      | Staging               |
| QA           | Staging               |
| QualityAssurance | Staging           |
| UAT          | Staging               |
| PreProd      | Staging               |
| Production   | Production            |

## Usage

```csharp
using Environments.EnvironmentMappings.Abstractions;
using Environments.EnvironmentMappings.Extensions;
using Environments.EnvironmentMappings.Models;
using Environments.EnvironmentMappings.Options;

var services = new ServiceCollection();
services.AddEnvironmentProfiles(options =>
{
    options.SetProfile(new EnvironmentProfile("Perf", CanonicalEnvironment.Staging));
    options.UnknownEnvironmentBehavior = UnknownEnvironmentBehavior.UseFallbackCanonicalEnvironment;
    options.FallbackCanonicalEnvironment = CanonicalEnvironment.Production;
});

var provider = services.BuildServiceProvider();
var resolver = provider.GetRequiredService<IEnvironmentProfileResolver>();

var profile = resolver.Resolve("QA");
var isNonProd = profile.IsNonProduction();
var isStaging = profile.IsStaging();
var isQa = profile.IsEnvironment("QA");
var isCanonical = profile.IsEnvironment("Staging");
```

## Environment Variable Resolution

If you're not using `IHostEnvironment`, you can resolve directly from environment variables. The resolver
checks `DOTNET_ENVIRONMENT` first, then falls back to `ASPNETCORE_ENVIRONMENT`.

```csharp
using Environments.EnvironmentMappings.Extensions;

var profile = resolver.ResolveFromEnvironmentVariables();
```

## Canonical Host Environment

If you want ASP.NET Core's built-in environment checks (like `IsDevelopment()`) to behave according to
your profile mappings, you can set the host environment to the canonical value before building the host.

```csharp
using Environments.EnvironmentMappings.Extensions;

var builder = Host.CreateDefaultBuilder(args)
    .UseCanonicalEnvironmentMappings();
```

## Environment Profile Checks

Environment profiles also expose convenience checks that mirror the host environment extensions. These
operate on the profile's canonical environment and can also match the profile name when using
`IsEnvironment`.

```csharp
var profile = resolver.Resolve("QA");

if (profile.IsStaging())
{
    // Canonical environment check.
}

if (profile.IsEnvironment("QA"))
{
    // Matches the profile name.
}
```

## Host Environment Integration

```csharp
using Environments.EnvironmentMappings.Abstractions;
using Environments.EnvironmentMappings.Extensions;
using Environments.EnvironmentMappings.Models;
using Microsoft.Extensions.Hosting;

IHostEnvironment hostEnvironment = ...;
IEnvironmentProfileResolver resolver = ...;

var profile = hostEnvironment.GetEnvironmentProfile(resolver);
```
