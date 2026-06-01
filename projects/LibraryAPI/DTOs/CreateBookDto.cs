using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs;

public class CreateBookDto
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Range(1, 2026)]
    public int PublishedYear { get; set; }

    [Range(0, 1000)]
    public int CopiesAvailable { get; set; }

    [Required]
    public int AuthorId { get; set; }

    [Required]
    public int GenreId { get; set; }
}