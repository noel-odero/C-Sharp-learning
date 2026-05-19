using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOs;

public record CreateGameDto([Required] [StringLength(50)]string Name, string Genre, decimal Price, DateOnly ReleaseDate);