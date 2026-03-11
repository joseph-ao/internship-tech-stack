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
    public async Task<IActionResult> GetEmployeesAsync()
    {
        var employees = await _service.GetEmployeesAsync();
        return Ok(employees);
    }
    [HttpGet("calculate")]
    public async Task<IActionResult> RunCalculation()
    {
        var employees = await _service.GetEmployeesAsync();

        Task<int> empcount = Task.Run(() =>
        {
            return employees.Count();
        });

        Task<string> formattedTask = empcount.ContinueWith(previousTask =>
        {
            int nbemp = previousTask.Result;
            return $"The number of employees is: {nbemp}";
        });

        string finalResult = await formattedTask;

        return Ok(finalResult);
    }
}
