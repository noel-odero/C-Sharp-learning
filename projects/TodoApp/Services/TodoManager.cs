using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp.Services;

public class TaskManager
{
    private readonly ITaskRepository _repository;
    private int _nextId = 1;

    public TaskManager(ITaskRepository repository)
    {
        _repository = repository;
    }

    public void AddTask(string title, Priority priority, DateTime? dueDate)
    {
        var task = new TodoItem(_nextId++, title, priority, DateTime.Now, dueDate);
        _repository.Add(task);
    }

    public void DeleteTask(int id)
    {
        _repository.Delete(id);
    }

    public void MarkComplete(int id)
    {
        var task = _repository.GetById(id);
        task?.MarkComplete();
    }

    public void MarkIncomplete(int id)
    {
        var task = _repository.GetById(id);
        task?.MarkIncomplete();
    }

    public void ShowAllTasks()
    {
        var tasks = _repository.GetAll();

        if (!tasks.Any())
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }

    public void ShowCompletedTasks()
    {
        var tasks = _repository.GetAll().Where(t => t.IsCompleted);

        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }

    public void ShowPendingTasks()
    {
        var tasks = _repository.GetAll().Where(t => !t.IsCompleted);

        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }

    public void ShowOverdueTasks()
    {
        var tasks = _repository.GetAll().Where(t => t.IsOverdue());

        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }
}