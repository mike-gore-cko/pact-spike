using Xunit;
using System.Threading.Tasks;
using PactNet;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Consumer.Tests;

public class GreetingApiConsumerTests
{
    private readonly IPactBuilderV3 pactBuilder;
    
    public GreetingApiConsumerTests()
    {
        var pact = Pact.V3("Test consumer", "Greeting API", new PactConfig());

        this.pactBuilder = pact.WithHttpInteractions();
    }


    [Fact]
    public async Task Bob()
    {
        this.pactBuilder
            .UponReceiving("A GET request to obtain the greeting")
                .WithRequest(HttpMethod.Get, "/greeting")
                .WithHeader("Accept", "application/json")
            .WillRespond()
                .WithStatus(HttpStatusCode.OK)
                .WithHeader("Content-Type", "application/json")
                .WithJsonBody(new {
                    greeting = "Hello World"
                });

        await this.pactBuilder.VerifyAsync(async ctx => 
        {
            // In a real test we'd be checking our class that invokes the API
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await client.GetAsync(ctx.MockServerUri + "greeting");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var greeting = JsonSerializer.Deserialize<GetGreetingResponse>(jsonResponse);

            Assert.NotNull(greeting);
            Assert.Equal("Hello World", greeting!.Greeting);
        });
    }
}

public class GetGreetingResponse
{
    [JsonPropertyName("greeting")]
    public string Greeting {get;set;}
}