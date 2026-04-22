using TodoApp.Models;
using TodoApp.Repositories;
using TodoApp.Services;

class Program
{
    static void Main()

    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=yourpassword;Database=todo_app";
        var repository = new PostgresTaskRepository(connectionString);
        var manager = new TaskManager(repository);

        Console.WriteLine("=== TODO CLI APP ===");

        while (true)
        {
            Console.Write("\nEnter what you want to do: add, list, done, undone, delete, pending, completed, overdue, exit ");
            var input = Console.ReadLine()?.Trim().ToLower();

            switch (input)
            {
                case "add":
                    Console.Write("Title: ");
                    var title = Console.ReadLine();

                    Console.Write("Priority (low, medium, high): ");
                    var priorityInput = Console.ReadLine()?.ToLower();

                    Priority priority = priorityInput switch
                    {
                        "low" => Priority.Low,
                        "medium" => Priority.Medium,
                        "high" => Priority.High,
                        _ => Priority.Low
                    };

                    Console.Write("Due date (yyyy-mm-dd) or leave blank: ");
                    var dueInput = Console.ReadLine();

                    DateTime? dueDate = null;
                    if (DateTime.TryParse(dueInput, out var parsedDate))
                    {
                        dueDate = parsedDate;
                    }

                    manager.AddTask(title!, priority, dueDate);
                    break;

                case "list":
                    manager.ShowAllTasks();
                    break;

                case "done":
                    Console.Write("Task ID: ");
                    manager.MarkComplete(int.Parse(Console.ReadLine()!));
                    break;

                case "undone":
                    Console.Write("Task ID: ");
                    manager.MarkIncomplete(int.Parse(Console.ReadLine()!));
                    break;

                case "delete":
                    Console.Write("Task ID: ");
                    manager.DeleteTask(int.Parse(Console.ReadLine()!));
                    break;

                case "pending":
                    manager.ShowPendingTasks();
                    break;

                case "completed":
                    manager.ShowCompletedTasks();
                    break;

                case "overdue":
                    manager.ShowOverdueTasks();
                    break;

                case "exit":
                    return;

                default:
                    Console.WriteLine("Commands: add, list, done, undone, delete, pending, completed, overdue, exit");
                    break;
            }
        }
    }
}