using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.UI
{
    public class ConsoleUI
    {
        private readonly TodoService _service;

        public ConsoleUI(TodoService service)
        {
            _service = service;
        }

        public void Run()
        {
            Console.WriteLine("=== Todo App ===\n");

            while (true)
            {
                ShowMenu();
                var choice = Console.ReadLine()?.Trim();

                try
                {
                    switch (choice)
                    {
                        case "1": AddTodo(); break;
                        case "2": ListTodos(); break;
                        case "3": ToggleTodo(); break;
                        case "4": UpdateTitle(); break;
                        case "5": UpdatePriority(); break;
                        case "6": DeleteTodo(); break;
                        case "7": FilterTodos(); break;
                        case "0":
                            Console.WriteLine("Goodbye!");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please enter a number from the menu.");
                            break;
                    }
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine($"[Not Found] {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Invalid Input] {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"[Error] {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Unexpected Error] {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        // Menu

        private void ShowMenu()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("1. Add todo");
            Console.WriteLine("2. List all todos");
            Console.WriteLine("3. Toggle status");
            Console.WriteLine("4. Update title");
            Console.WriteLine("5. Update priority");
            Console.WriteLine("6. Delete todo");
            Console.WriteLine("7. Filter todos");
            Console.WriteLine("0. Exit");
            Console.WriteLine("---------------------");
            Console.Write("Choose an option: ");
        }

        // Actions 

        private void AddTodo()
        {
            var title = PromptRequired("Title: ");
            
            Console.Write("Description (optional): ");
            var description = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(description)) description = null;

            var priority = PromptPriority();
            var dueDate = PromptDueDate();

            _service.AddTodo(title, description, priority, dueDate);
            Console.WriteLine("Todo added.");
        }

        private void ListTodos()
        {
            var todos = _service.GetAll().ToList();

            if (todos.Count == 0)
            {
                Console.WriteLine("No todos yet.");
                return;
            }

            Console.WriteLine();
            for (int i = 0; i < todos.Count; i++)
                Console.WriteLine($"  {i + 1}. {todos[i]}");
        }

        private void ToggleTodo()
        {
            var todo = PickTodo("toggle");
            if (todo == null) return;
            
            bool wasCompleted = todo.IsCompleted;

            _service.ToggleStatus(todo.Id);
            var newStatus = wasCompleted ? "pending" : "completed"; 
            Console.WriteLine($"Marked as {newStatus}.");
        }

        private void UpdateTitle()
        {
            var todo = PickTodo("update");
            if (todo == null) return;

            var newTitle = PromptRequired("New title: ");
            _service.UpdateTitle(todo.Id, newTitle);
            Console.WriteLine("Title updated.");
        }

        private void UpdatePriority()
        {
            var todo = PickTodo("reprioritise");
            if (todo == null) return;

            var priority = PromptPriority();
            _service.UpdatePriority(todo.Id, priority);
            Console.WriteLine("Priority updated.");
        }

        private void DeleteTodo()
        {
            var todo = PickTodo("delete");
            if (todo == null) return;

            Console.Write($"Are you sure you want to delete \"{todo.Title}\"? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() != "y")
            {
                Console.WriteLine("Cancelled.");
                return;
            }

            _service.Delete(todo.Id);
            Console.WriteLine("Todo deleted.");
        }

        private void FilterTodos()
        {
            Console.WriteLine("Filter by: 1. Pending  2. Completed");
            Console.Write("Choice: ");
            var input = Console.ReadLine()?.Trim();

            var todos = input switch
            {
                "1" => _service.GetPending().ToList(),
                "2" => _service.GetCompleted().ToList(),
                _   => throw new ArgumentException("Invalid filter option. Enter 1 or 2.")
            };

            if (todos.Count == 0)
            {
                Console.WriteLine("No todos found.");
                return;
            }

            Console.WriteLine();
            for (int i = 0; i < todos.Count; i++)
                Console.WriteLine($"  {i + 1}. {todos[i]}");
        }

        // Helpers

        private TodoItem? PickTodo(string action)
        {
            var todos = _service.GetAll().ToList();

            if (todos.Count == 0)
            {
                Console.WriteLine("No todos available.");
                return null;
            }

            Console.WriteLine();
            for (int i = 0; i < todos.Count; i++)
                Console.WriteLine($"  {i + 1}. {todos[i]}");

            Console.Write($"\nEnter number to {action}: ");
            var input = Console.ReadLine()?.Trim();

            if (!int.TryParse(input, out int index) || index < 1 || index > todos.Count)
                throw new ArgumentException("Invalid selection. Enter a number from the list.");

            return todos[index - 1];
        }

        private string PromptRequired(string label)
        {
            Console.Write(label);
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("This field cannot be empty.");

            return input;
        }

        private Priority PromptPriority()
        {
            Console.Write("Priority (1=Low, 2=Medium, 3=High) [default: Medium]: ");
            var input = Console.ReadLine()?.Trim();

            return input switch
            {
                "1" => Priority.Low,
                "3" => Priority.High,
                _   => Priority.Medium
            };
        }

        private DateTime? PromptDueDate()
        {
            Console.Write("Due date (yyyy-MM-dd, optional): ");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input)) return null;

            if (!DateTime.TryParse(input, out var date))
                throw new ArgumentException("Invalid date format. Use yyyy-MM-dd.");

            return date;
        }
    }
}