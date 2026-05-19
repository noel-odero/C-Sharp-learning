using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

var connString = builder.Configuration.GetConnectionString("Default") ?? "Data Source=GameStore.db";
builder.Services.AddDbContext<GameStoreContext>(options => options.UseSqlite(connString));

var app = builder.Build();

app.MapGamesEndPoint();

app.Run();

// define the code to bootstrap application
