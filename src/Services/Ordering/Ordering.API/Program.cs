var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddAPIServices();



var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
