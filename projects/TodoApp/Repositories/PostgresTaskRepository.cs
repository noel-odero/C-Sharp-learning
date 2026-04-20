using TodoApp.Models;
namespace TodoApp.Repositories;


public class PostgresTaskRepository : ITaskRepository
{
    private readonly string _connectionString;

    public PostgresTaskRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Add(TodoItem task)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public TodoItem? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public List<TodoItem> GetAll()
    {
        throw new NotImplementedException();
    }
    public void Update(TodoItem task)
    {
        throw new NotImplementedException();
    }

}