using TodoApp.Models;
using Npgsql;
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
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        const string sql = @"
            INSERT INTO todos (title, is_completed, created_at, due_date, priority)
            VALUES (@title, @isCompleted, @createdAt, @dueDate, @priority);";

        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("title", task.Title);
        cmd.Parameters.AddWithValue("isCompleted", task.IsCompleted);
        cmd.Parameters.AddWithValue("createdAt", task.CreatedAt);
        cmd.Parameters.AddWithValue("dueDate", task.DueDate ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("priority", (short)task.Priority);

        cmd.ExecuteNonQuery();
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