var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/books", ()=>
{
    return "Hello from the Library API!";
});

app.Run();