using System;
using Xunit;
using PactNet.Verifier;
using Xunit.Abstractions;
using PactNet.Infrastructure.Outputters;

namespace GreetingApi.Tests;

public class GreetingApiTests
{
    private readonly ITestOutputHelper output;

    public GreetingApiTests(ITestOutputHelper output)
    {
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

        // TODO: Tests would need to be provided the provider variables via env vars
        var providerVersion = "1.0.0";
        var providerBranch = "main";
        
        var verifier = new PactVerifier(config);
        verifier
            .ServiceProvider("Greeting API", new Uri("http://localhost:5001"))
            .WithPactBrokerSource(new Uri("http://localhost:9292/"), options =>
            {
                // See https://docs.pact.io/pact_broker/advanced_topics/pending_pacts
                options.EnablePending()
                    .ConsumerVersionSelectors(new ConsumerVersionSelector { MainBranch = true })
                    .PublishResults(providerVersion, publish => publish.ProviderBranch(providerBranch));
            })
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