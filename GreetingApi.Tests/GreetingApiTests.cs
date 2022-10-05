using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using PactNet.Verifier;
using Xunit.Abstractions;
using PactNet.Infrastructure.Outputters;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using System.Threading;

namespace GreetingApi.Tests;

public class GreetingApiTests : IClassFixture<GreetingApiFixture>
{
    private readonly GreetingApiFixture fixture;
    private readonly ITestOutputHelper output;

    public GreetingApiTests(GreetingApiFixture fixture, ITestOutputHelper output)
    {
        this.fixture = fixture;
        this.output = output;
    }

    [Fact]
    public void RunContractTests()
    {
        var config = new PactVerifierConfig {
            Outputters = new[] {
                new XUnitOutput(output)
            }
        };        

        var verifier = new PactVerifier(config);
        verifier
            .ServiceProvider("Greeting API", new Uri("http://localhost:5001"))
            .WithPactBrokerSource(new Uri("http://localhost:9292/"))
            .Verify();
    }
}

public class XUnitOutput : IOutput
{
    private readonly ITestOutputHelper output;

    public XUnitOutput(ITestOutputHelper output)
    {
        this.output = output;
    }

    public void WriteLine(string line)
    {
        this.output.WriteLine(line);
    }
}

public class GreetingApiFixture : IAsyncDisposable
{
    private WebApplication application;

    public GreetingApiFixture()
    {
        var builder = WebApplication.CreateBuilder(Array.Empty<string>());
        application = Program.BuildApplication(builder);
        application.RunAsync("http://localhost:5001");
        Thread.Sleep(TimeSpan.FromSeconds(10)); // Handle this better!
    }

    public async ValueTask DisposeAsync()
    {
        await application.DisposeAsync();
    }
}