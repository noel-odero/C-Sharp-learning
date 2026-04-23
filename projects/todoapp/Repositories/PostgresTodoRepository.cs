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
            using var conn = CreateConnection;
            conn.Open();
        }
        
    }
}