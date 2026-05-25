using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Author { get; set; } = string.Empty;

    [Range(1, 2026)]
    public int PublishedYear { get; set; }

    [Range(0, 1000)]
    public int CopiesAvailable { get; set; }
}