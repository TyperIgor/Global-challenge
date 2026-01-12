using Device.API.Application.Middleware;
using Device.API.Infrastructure.DI;
using Device.API.Infrastructure.Logging;
using Microsoft.OpenApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration, builder.Environment); // inject application dependencies 
builder.Host.UseSerilog();
builder.Services.ConfigureLogger(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Device API",
        Version = "v1",
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
app.UseHealthChecks("/health");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
