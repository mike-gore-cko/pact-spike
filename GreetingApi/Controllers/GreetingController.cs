using Microsoft.AspNetCore.Mvc;

namespace GreetingApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GreetingController : ControllerBase
{
    [HttpGet(Name = "GetGreeting")]
    public GetGreetingResponse Get()
    {
        return new GetGreetingResponse("Hello World");
    }
}

public record GetGreetingResponse(string Greeting);
