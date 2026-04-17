using TodoApp.Models;

namespace TodoApp.Repositories;

public interface ITaskRepository
{
    void Add(TodoItem task);
    void Delete(int id);
    TodoItem? GetById(int id);
    List<TodoItem> GetAll();
    void Update(TodoItem task);
}