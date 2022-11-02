using System.Net.Http.Json;
using System.Text.Json;

namespace Consumer.UseCases.Greetings;

public class GreetingServiceClient
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    
    private readonly HttpClient _client;

    public GreetingServiceClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> GetGreeting()
    {
        var response = await _client.GetAsync("/greeting");
        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadFromJsonAsync<GetGreetingResponse>(JsonSerializerOptions);
            return responseBody!.Greeting;
        }

        return string.Empty;
    }
}