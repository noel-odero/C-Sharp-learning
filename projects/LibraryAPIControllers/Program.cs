using LibraryAPIControllers.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// builder.Services is an IServiceCollection - it is the object you register everything
// into during the builder phase. It s the DI container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<LibraryContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("LibraryDBX")));



// When you call this, this is the moment the builder.Services gets compiled into an actual DI container(IServiceProvider) that the 
// app uses at runtime to resolve dependencies
var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();




