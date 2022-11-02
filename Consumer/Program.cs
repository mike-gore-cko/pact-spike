using Consumer.UseCases.Greetings;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5001"), 
    DefaultRequestHeaders = { {"Accept", "application/json"} }
};

var greetingServiceClient = new GreetingServiceClient(httpClient);
var greetingService = new GreetingService(greetingServiceClient);

var greeting = await greetingService.GetGreeting();
Console.WriteLine(greeting);