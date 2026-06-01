namespace LibraryAPI.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public int CopiesAvailable { get; set; }

    // Foreign keys
    public int AuthorId { get; set; }
    public int GenreId { get; set; }

    // Navigation properties
    public Author? Author { get; set; }
    public Genre? Genre { get; set; }
}