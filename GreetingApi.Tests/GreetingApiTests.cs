using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using PactNet.Verifier;
using Xunit.Abstractions;
using PactNet.Infrastructure.Outputters;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;

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
        var client = fixture.Client;
        var config = new PactVerifierConfig {
            Outputters = new[] {
                new XUnitOutput(output)
            }
        };        

        var verifier = new PactVerifier(config);
        verifier
            .ServiceProvider("Greeting API", client.BaseAddress)
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

    public HttpClient Client { get; private set; }

    public GreetingApiFixture()
    {
        var builder = WebApplication.CreateBuilder(Array.Empty<string>());
        application = Program.BuildApplication(builder);
        application.RunAsync("http://localhost:5001");

        Client = new HttpClient();
        Client.BaseAddress = new Uri("http://localhost:5001");
    }

    public async ValueTask DisposeAsync()
    {
        Client.Dispose();
        await application.DisposeAsync();
    }
}