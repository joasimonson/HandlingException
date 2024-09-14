var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapGet("/exception", () =>
{
    throw new NotImplementedException();
});

app.Run();

public partial class Program { }