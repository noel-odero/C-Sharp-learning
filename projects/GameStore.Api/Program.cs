using System;

using GameStore.Api.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndPointName = "GetGame";

List<GameDto> games = new()
{
    new(1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
    new(2, "Super Mario Bros.", "Platformer", 29.99M, new DateOnly(1985, 9, 13)),
    new(3, "The Witcher 3: Wild Hunt", "RPG", 39.99M, new DateOnly(2015, 5, 19)),
    new(4, "Portal 2", "Puzzle", 14.99M, new DateOnly(2011, 4, 19)),
    new(5, "Stardew Valley", "Simulation", 14.99M, new DateOnly(2016, 2, 26))
};

app.MapGet("/games", () => games);


// GET /games/Id

app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndPointName);

// POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
        );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
});

// PUT /games/Id
app.MapPut("/games/{id}", (int id, UpdateNameDto updatedGame) =>
{
    var index = games.FindIndex(game => game.Id == id);
    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent();

});

// DELETE /games/id
app.MapDelete("/games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);

    return;
    Results.NoContent();
});

app.Run();

// define the code to bootstrap application
