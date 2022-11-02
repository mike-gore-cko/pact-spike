using System.Text.Json;
using Consumer.UseCases.Greetings;

var jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5001") };
var greetingServiceClient = new GreetingServiceClient(httpClient, jsonSerializerOptions);
var greetingService = new GreetingService(greetingServiceClient);

var greeting = await greetingService.GetGreeting();

Console.WriteLine(greeting);