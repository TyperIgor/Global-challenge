using Device.API.Application.Middleware;
using Device.API.Infrastructure.DI;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration); // inject application dependencies 
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Device API",
        Version = "v0",
        Description = "Api that creates devices lol",
        Contact = new OpenApiContact()
        {
            Name = "Igor Lima",
            Url = new Uri("https://github.com/TyperIgor/Global-challenge"),
        }
    });
});

var app = builder.Build();

app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
