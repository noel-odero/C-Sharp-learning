using TodoApp.Repositories;
using TodoApp.Services;
using TodoApp.UI;

try
{
    var repository = new InMemoryTodoRepository();
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