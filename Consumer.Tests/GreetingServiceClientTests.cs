using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Consumer.UseCases.Greetings;

using PactNet;
using PactNet.Matchers;

using Xunit;

namespace Consumer.Tests;

public class GreetingServiceClientTests
{
    private readonly IPactBuilderV3 _pactBuilder;

    public GreetingServiceClientTests()
    {
        var pact = Pact.V3("Consumer", "Greeting API", new PactConfig());
        _pactBuilder = pact.WithHttpInteractions();
    }


    [Fact]
    public async Task Bob()
    {        
        _pactBuilder
            .UponReceiving("A GET request to obtain the greeting")
                .WithRequest(HttpMethod.Get, "/greeting")
                .WithHeader("Accept", "application/json")
            .WillRespond()
                .WithStatus(HttpStatusCode.OK)
                .WithHeader("Content-Type", "application/json")
                .WithJsonBody(new
                {
                    greeting = Match.Type("Hello there!")
                });

        await _pactBuilder.VerifyAsync(async ctx =>
        {
            var client = new GreetingServiceClient(new HttpClient
            {
                BaseAddress = ctx.MockServerUri,
                DefaultRequestHeaders = { { "Accept", "application/json" } }
            });
            var greeting = await client.GetGreeting();

            Assert.NotNull(greeting);
            Assert.NotEmpty(greeting);
        });
    }
}