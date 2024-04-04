using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndPoints();

// app.MapGet("/", () => "Hello World!");

app.Run();
