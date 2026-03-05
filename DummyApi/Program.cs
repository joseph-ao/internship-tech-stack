//necessary stuff to run the program(no swagger)
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build(); 
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();