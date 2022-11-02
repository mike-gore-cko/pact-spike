namespace Consumer.UseCases.Greetings;

public class GreetingService
{
    private readonly GreetingServiceClient _client;

    public GreetingService(GreetingServiceClient client)
    {
        _client = client;
    }

    public Task<string> GetGreeting()
    {
        return _client.GetGreeting();
    }
}