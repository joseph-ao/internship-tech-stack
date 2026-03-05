using DummyApi.Models;

namespace DummyApi.Repositories.Interface;

public interface IEmployeeRepository
{
    public Task<IEnumerable<Employee>> GetEmployees();
}