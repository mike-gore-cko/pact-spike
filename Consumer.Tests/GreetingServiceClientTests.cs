using Xunit;
using System.Threading.Tasks;
using PactNet;
using System.Net.Http;
using System.Net;
using Consumer.UseCases.Greetings;

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
                .WithJsonBody(new {
                    greeting = "Hello World"
                });

        await _pactBuilder.VerifyAsync(async ctx =>
        {
            var client = new GreetingServiceClient(new HttpClient
            {
                BaseAddress = ctx.MockServerUri,
                DefaultRequestHeaders = { {"Accept", "application/json"} }
            });
            var greeting = await client.GetGreeting();

            Assert.NotNull(greeting);
            Assert.Equal("Hello World", greeting);
        });
    }
}