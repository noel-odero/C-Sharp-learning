using TodoApp.Repositories;
using TodoApp.Services;
using TodoApp.UI;
using DotNetEnv;
using Microsoft.Extensions.Configuration;

try
{


    Env.Load();
    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).AddEnvironmentVariables().Build();

    var host     = config["DATABASE_HOST"]     ?? config["Database:Host"];
    var port     = config["DATABASE_PORT"]     ?? config["Database:Port"];
    var name     = config["DATABASE_NAME"]     ?? config["Database:Name"];
    var username = config["DATABASE_USERNAME"] ?? config["Database:Username"];
    var password = config["DATABASE_PASSWORD"] ?? config["Database:Password"];

    var connectionString = $"Host={host};Port={port};Database={name};Username={username};Password={password}";
    var repository = new PostgresTodoRepository(connectionString);
    var service = new TodoService(repository);
    var ui = new ConsoleUI(service);


    ui.Run();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n[Fatal Error] The application crashed unexpectedly:\n{ex.Message}");
    Console.ResetColor();
    Environment.Exit(1);
}