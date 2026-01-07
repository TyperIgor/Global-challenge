using Device.API.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDependencies(builder.Configuration); // inject application dependencies 
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
