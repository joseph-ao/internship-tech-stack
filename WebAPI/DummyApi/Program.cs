using DummyApi.Repositories.Implementation;
using DummyApi.Repositories.Interface;
using DummyApi.Services.Implementation;
using DummyApi.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// These 2 lines are what might be missing:
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
