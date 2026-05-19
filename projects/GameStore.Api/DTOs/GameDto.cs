namespace GameStore.Api.DTOs;

using System;

// A DTO is a contract between client and server: a shared agreement
// about how data will be transferred and used.
public record GameDto(int Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate);