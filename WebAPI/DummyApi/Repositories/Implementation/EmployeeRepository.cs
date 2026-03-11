using Dapper;
using DummyApi.Models;
using DummyApi.Repositories.Interface;
using Microsoft.Data.SqlClient;

namespace DummyApi.Repositories.Implementation;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IConfiguration _config;

    public EmployeeRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        var sql = "SELECT Id, Name, Department FROM Employee";
        return await connection.QueryAsync<Employee>(sql);
    }
}