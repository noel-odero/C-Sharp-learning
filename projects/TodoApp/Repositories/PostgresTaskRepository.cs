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
    var tasks = new List<TodoItem>();

    using var conn = new NpgsqlConnection(_connectionString);
    conn.Open();

    const string sql = @"
        SELECT id, title, is_completed, created_at, due_date, priority
        FROM todos
        ORDER BY id;";

    using var cmd = new NpgsqlCommand(sql, conn);
    using var reader = cmd.ExecuteReader();

    while (reader.Read())
    {
        var id = reader.GetInt32(0);
        var title = reader.GetString(1);
        var isCompleted = reader.GetBoolean(2);
        var createdAt = reader.GetDateTime(3);
        DateTime? dueDate = reader.IsDBNull(4) ? null : reader.GetDateTime(4);
        var priority = (Priority)reader.GetInt16(5);

        var task = new TodoItem(id, title, priority, createdAt, dueDate);
        if (isCompleted)
        {
            task.MarkComplete();
        }

        tasks.Add(task);
    }

    return tasks;
}
    public void Update(TodoItem task)
    {
        throw new NotImplementedException();
    }

}