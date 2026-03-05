using Microsoft.AspNetCore.Mvc;

namespace DummyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetEmployees()
    {
        return Ok("employeecontroller works");
    }
}