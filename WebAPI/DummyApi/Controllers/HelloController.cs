using Microsoft.AspNetCore.Mvc;

namespace DummyApi.Controllers;
//inherit class ControllerBase controller to make the normal class a controller and add ApiController, Route
[ApiController] //make class a controller class
[Route("api/[controller]")] // specify route, which url will execute this controller
public class HelloController : ControllerBase
{
    //adding first endpoint
    [HttpGet("message1")] //ActionMethodget to specify that this is a GET method //added message1 because 2 endpoints in one controller
    public IActionResult GetMessage()
    {
        return Ok(("Hello, World!")); // when everything is Ok (200) send hello world
    }
    //adding second endpoint
    [HttpGet("message2")] 
    public IActionResult GetMessage2()
    {
        return Ok(("Hello, World2!")); 
    }
}