var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/games", () => "Hello World!");
;
app.Run();

// define the code to bootstrap application
