using TodoListApp;

var manager = new TodoManager();

manager.Add(new DeadlinedTask("Fix login bug", Priority.High, DateTime.Now.AddDays(2)));
manager.Add(new RecurringTast("Write tests", Priority.Low, 7, "Unit tests"));
manager.Add(new DeadlinedTask("Update docs", Priority.Medium, DateTime.Now.AddDays(5)));

Console.WriteLine("All todos:");
foreach (var todo in manager.GetAll())
{
    Console.WriteLine(todo.GetSummary());
}

Console.WriteLine();
Console.WriteLine("High priority todos:");
foreach (var todo in manager.GetByPriority(Priority.High))
{
    Console.WriteLine(todo.GetSummary());
}

Console.WriteLine();
Console.WriteLine("Todos sorted by title:");
foreach (var todo in manager.GetAll().OrderBy(todo => todo.Title))
{
    Console.WriteLine(todo.GetSummary());
}