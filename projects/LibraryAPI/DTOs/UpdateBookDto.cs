using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs;

public class UpdateBookDto
{
    [StringLength(200, MinimumLength = 1)]
    public string? Title { get; set; }

    [StringLength(100, MinimumLength = 1)]
    public string? Author { get; set; }

    [Range(1, 2026)]
    public int? PublishedYear { get; set; }

    [Range(0, 1000)]
    public int? CopiesAvailable { get; set; }
}