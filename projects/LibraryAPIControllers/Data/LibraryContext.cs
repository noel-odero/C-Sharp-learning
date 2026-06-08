using LibraryAPIControllers.Models;
using Microsoft.EntityFrameworkCore;


namespace LibraryAPIControllers.Data;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
        
    }

    public DbSet<Book> BooksTable {get; set;}
}