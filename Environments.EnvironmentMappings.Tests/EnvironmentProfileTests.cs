using Environments.EnvironmentMappings;
using Environments.EnvironmentMappings.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Environments.EnvironmentMappings.Tests;

public class EnvironmentProfileTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void EnvironmentProfile_Throws_For_Invalid_Name(string? name)
    {
        Assert.Throws<ArgumentException>(() => new EnvironmentProfile(name!, CanonicalEnvironment.Production));
    }

    [Theory]
    [InlineData("QA", EnvironmentProfileNames.Qa, CanonicalEnvironment.Staging)]
    [InlineData("qa", EnvironmentProfileNames.Qa, CanonicalEnvironment.Staging)]
    [InlineData("Local", EnvironmentProfileNames.Local, CanonicalEnvironment.Development)]
    [InlineData("Production", EnvironmentProfileNames.Production, CanonicalEnvironment.Production)]
    public void Resolver_Uses_Default_Profiles(
        string environmentName,
        string expectedName,
        CanonicalEnvironment expectedCanonicalEnvironment)
    {
        var resolver = new EnvironmentProfileResolver(new EnvironmentProfileResolverOptions());

        var profile = resolver.Resolve(environmentName);

        Assert.Equal(expectedName, profile.Name);
        Assert.Equal(expectedCanonicalEnvironment, profile.CanonicalEnvironment);
    }

    [Fact]
    public void Resolver_Throws_For_Unknown_By_Default()
    {
        var resolver = new EnvironmentProfileResolver(new EnvironmentProfileResolverOptions());

        Assert.Throws<EnvironmentProfileResolutionException>(() => resolver.Resolve("DoesNotExist"));
    }

    [Fact]
    public void Resolver_Uses_Fallback_When_Configured()
    {
        var options = new EnvironmentProfileResolverOptions
        {
            UnknownEnvironmentBehavior = UnknownEnvironmentBehavior.UseFallbackCanonicalEnvironment,
            FallbackCanonicalEnvironment = CanonicalEnvironment.Staging
        };
        var resolver = new EnvironmentProfileResolver(options);

        var profile = resolver.Resolve("Perf");

        Assert.Equal("Perf", profile.Name);
        Assert.Equal(CanonicalEnvironment.Staging, profile.CanonicalEnvironment);
    }

    [Fact]
    public void Resolver_Uses_Custom_Profile_Overrides()
    {
        var options = new EnvironmentProfileResolverOptions();
        options.SetProfile(new EnvironmentProfile(EnvironmentProfileNames.Qa, CanonicalEnvironment.Production));
        var resolver = new EnvironmentProfileResolver(options);

        var profile = resolver.Resolve(EnvironmentProfileNames.Qa);

        Assert.Equal(CanonicalEnvironment.Production, profile.CanonicalEnvironment);
    }

    [Fact]
    public void EnvironmentProfileExtensions_Use_Profile_Name_And_Canonical_Environment()
    {
        var qaProfile = new EnvironmentProfile("qa", CanonicalEnvironment.Staging);
        var prodProfile = new EnvironmentProfile(EnvironmentProfileNames.Production, CanonicalEnvironment.Production);

        Assert.True(qaProfile.IsQa());
        Assert.False(qaProfile.IsUat());
        Assert.True(qaProfile.IsNonProduction());

        Assert.False(prodProfile.IsNonProduction());
    }

    [Fact]
    public void HostEnvironmentExtensions_Resolve_From_Environment_Name()
    {
        var resolver = new EnvironmentProfileResolver(new EnvironmentProfileResolverOptions());
        IHostEnvironment hostEnvironment = new TestHostEnvironment { EnvironmentName = "UAT" };

        var profile = hostEnvironment.GetEnvironmentProfile(resolver);

        Assert.Equal(EnvironmentProfileNames.Uat, profile.Name);
        Assert.Equal(CanonicalEnvironment.Staging, profile.CanonicalEnvironment);
    }

    private sealed class TestHostEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = EnvironmentProfileNames.Development;
        public string ApplicationName { get; set; } = "Test";
        public string ContentRootPath { get; set; } = "/";
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }
}
