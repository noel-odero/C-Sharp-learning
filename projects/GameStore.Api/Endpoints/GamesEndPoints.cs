using GameStore.Api.DTOs;

namespace GameStore.Api.Endpoints;

public static class GamesEndPoints

{
    const string GetGameEndPointName = "GetGame";

    private static readonly List<GameDto> games = new()
    {
        new(1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new(2, "Super Mario Bros.", "Platformer", 29.99M, new DateOnly(1985, 9, 13)),
        new(3, "The Witcher 3: Wild Hunt", "RPG", 39.99M, new DateOnly(2015, 5, 19)),
        new(4, "Portal 2", "Puzzle", 14.99M, new DateOnly(2011, 4, 19)),
        new(5, "Stardew Valley", "Simulation", 14.99M, new DateOnly(2016, 2, 26))
    };

    public static void MapGamesEndPoint(this WebApplication app)
    {

        var group = app.MapGroup("/games");
        group.MapGet("/games", () => games);


        // GET /games/Id

        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok();
        }).WithName(GetGameEndPointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
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
        group.MapPut("/{id}", (int id, UpdateNameDto updatedGame) =>
        {
            
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }
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
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return;
            Results.NoContent();
        });
    }
}