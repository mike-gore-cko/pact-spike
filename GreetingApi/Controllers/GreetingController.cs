using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace GreetingApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GreetingController : ControllerBase
{
    private readonly ILogger<GreetingController> _logger;

    public GreetingController(ILogger<GreetingController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetGreeting")]
    public GetGreetingResponse Get()
    {
        return new GetGreetingResponse {
            Greeting = "Hello World"
        };
    }
}

public class GetGreetingResponse
{
    [JsonPropertyName("greeting")]
    public string? Greeting {get;set;}
}
