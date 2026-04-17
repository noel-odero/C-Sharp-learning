using TodoApp.Models;
namespace TodoApp.Repositories;

public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TodoItem> _tasks = new();

    public void Add(TodoItem task)
    {
        _tasks.Add(task);
    }

    public void Delete(int id)
    {
        var task = GetById(id);
        if(task != null)
        {
            _tasks.Remove(task);
        }
    }

    public TodoItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(x => x.Id == id);
    }

    public List<TodoItem> GetAll()
    {
        return _tasks.ToList();
    }

    public void Update(TodoItem task)
    {
        
    }
    
}