using DummyApi.Models;

namespace DummyApi.Services.Interface;

public interface IEmployeeService
{
    public Task<IEnumerable<Employee>> GetEmployees();
}