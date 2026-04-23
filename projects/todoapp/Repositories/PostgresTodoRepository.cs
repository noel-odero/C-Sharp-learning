using Npgsql;
using TodoApp.Models;

namespace TodoApp.Repositories
{
    public class PostgresTodoRepository : ITodoRepository
    {
        private readonly string _connectionString;

        public PostgresTodoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private NpgsqlConnection CreateConnection()=> new NpgsqlConnection(_connectionString);

        public void Add(TodoItem item)
        {
            using var conn = CreateConnection();
            conn.Open();

            using var cmd = new NpgsqlCommand(
                @"INSERT INTO todos (id, title, description, is_completed, priority, created_at, due_date)
                  VALUES (@id, @title, @description, @isCompleted, @priority, @createdAt, @dueDate)",
                conn
            );
            cmd.Parameters.AddWithValue("id", item.Id);
            cmd.Parameters.AddWithValue("title", item.Title);
            cmd.Parameters.AddWithValue("description", (object?)item.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("isCompleted", item.IsCompleted);
            cmd.Parameters.AddWithValue("priority", item.Priority.ToString());
            cmd.Parameters.AddWithValue("createdAt", item.CreatedAt);
            cmd.Parameters.AddWithValue("dueDate", (object?)item.DueDate ?? DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public IEnumerable<TodoItem> GetAll()
        {
            using var conn = CreateConnection();
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM todos ORDER BY created_at DESC", conn);
            using var reader = cmd.ExecuteReader();

            var todos = new List<TodoItem>();
            while (reader.Read())
                todos.Add(MapRow(reader));

            return todos;
        }
        public TodoItem? GetById(Guid id)
        {
            using var conn = CreateConnection();
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM todos WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapRow(reader) : null;
        }
        public void Update(TodoItem item)
        {
            using var conn = CreateConnection();
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
                UPDATE todos
                SET title        = @title,
                    description  = @description,
                    is_completed = @isCompleted,
                    priority     = @priority,
                    due_date     = @dueDate
                WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("id", item.Id);
            cmd.Parameters.AddWithValue("title", item.Title);
            cmd.Parameters.AddWithValue("description", (object?)item.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("isCompleted", item.IsCompleted);
            cmd.Parameters.AddWithValue("priority", item.Priority.ToString());
            cmd.Parameters.AddWithValue("dueDate", (object?)item.DueDate ?? DBNull.Value);

            var affected = cmd.ExecuteNonQuery();
            if (affected == 0)
                throw new KeyNotFoundException($"Todo with ID {item.Id} not found.");
        }

        public void Delete(Guid id)
        {
            using var conn = CreateConnection();
            conn.Open();

            using var cmd = new NpgsqlCommand("DELETE FROM todos WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            var affected = cmd.ExecuteNonQuery();
            if (affected == 0)
                throw new KeyNotFoundException($"Todo with ID {id} not found.");
        }

        private static TodoItem MapRow(NpgsqlDataReader reader)
        {
            var priority = Enum.Parse<Priority>(reader.GetString("priority"));
            var dueDate = reader.IsDBNull("due_date") ? (DateTime?)null : reader.GetDateTime("due_date");

            return TodoItem.Reconstitute(
                id:          reader.GetGuid("id"),
                title:       reader.GetString("title"),
                description: reader.IsDBNull("description") ? null : reader.GetString("description"),
                isCompleted: reader.GetBoolean("is_completed"),
                priority:    priority,
                createdAt:   reader.GetDateTime("created_at"),
                dueDate:     dueDate
            );
        }


    }
}