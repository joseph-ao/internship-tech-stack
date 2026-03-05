using DummyApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DummyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _service.GetEmployees();
        return Ok(employees);
    }
}