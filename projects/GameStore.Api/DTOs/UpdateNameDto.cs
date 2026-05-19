namespace GameStore.Api.DTOs;

public record UpdateNameDto(string Name, string Genre, decimal Price, DateOnly ReleaseDate);