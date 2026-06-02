using LibraryAPI.Endpoints;
using LibraryAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("LibraryDB")));

var app = builder.Build();
var group = app.MapGroup("/books");

group.MapBooksEndpoints();

app.Run();