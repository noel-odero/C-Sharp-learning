using LibraryAPi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data;

public class LibraryContext : DBContext
{
    public LibraryContext(DBContextOptions<LibraryContext> options) : base(options)
    {
        
    }

    public DbSet<Book> Books {get; set;}
}