# Dotnet.EnvironmentProfiles

[![Publish NuGet](https://github.com/TuckerMogren/Dotnet.EnvironmentProfiles/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/TuckerMogren/Dotnet.EnvironmentProfiles/actions/workflows/publish-nuget.yml)

Dotnet.EnvironmentProfiles provides deterministic environment mapping for .NET applications.
It lets you keep real-world environment names (QA, UAT, PreProd, etc.) while mapping them to
canonical ASP.NET Core behavior (Development, Staging, Production).

## NuGet Package

Package metadata is defined in `Dotnet.EnvironmentProfiles.nuspec`. The changelog is in
`CHANGELOG.md` and each release should update both the package version and `releaseNotes`.

## Core Concepts

- EnvironmentProfile is the source of truth for environment semantics.
- IEnvironmentProfileResolver resolves raw environment names into profiles.
- Extension methods are thin conveniences and rely on resolved profiles.

## Default Profiles

The resolver ships with the following defaults. You can override or replace them via DI.

| Profile Name | Canonical Environment |
| :----------- | :-------------------- |
| CDE.         | Development           |
| Development  | Development           |
| Local        | Development           |
| Staging      | Staging               |
| QA           | Staging               |
| UAT          | Staging               |
| NonProd      | Staging               |
| PreProd      | Staging               |
| Production   | Production            |

## Usage

```csharp
using Dotnet.EnvironmentProfiles;
using Dotnet.EnvironmentProfiles.Extensions;

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
```

## Host Environment Integration

```csharp
using Dotnet.EnvironmentProfiles.Extensions;
using Microsoft.Extensions.Hosting;

IHostEnvironment hostEnvironment = ...;
IEnvironmentProfileResolver resolver = ...;

var profile = hostEnvironment.GetEnvironmentProfile(resolver);
```
