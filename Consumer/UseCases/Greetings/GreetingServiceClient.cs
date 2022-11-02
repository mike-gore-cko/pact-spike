using System.Net.Http.Json;
using System.Text.Json;

namespace Consumer.UseCases.Greetings;

public class GreetingServiceClient
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public GreetingServiceClient(HttpClient client, JsonSerializerOptions jsonSerializerOptions)
    {
        _client = client;
        _jsonSerializerOptions = jsonSerializerOptions;
    }

    public async Task<string> GetGreeting()
    {
        var response = await _client.GetAsync("/greeting");
        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadFromJsonAsync<GetGreetingResponse>(_jsonSerializerOptions);
            return responseBody!.Greeting;
        }

        return string.Empty;
    }
}