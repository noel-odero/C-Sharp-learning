using System;

using GameStore.Api.DTOs;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndPoint();

app.Run();

// define the code to bootstrap application
