namespace Consumer.UseCases.Greetings;

public class GreetingService
{
    private GreetingServiceClient client;

    public GreetingService(GreetingServiceClient client)
    {
        this.client = client;
    }

    public Task<string> GetGreeting()
    {
        return client.GetGreeting();
    }
}