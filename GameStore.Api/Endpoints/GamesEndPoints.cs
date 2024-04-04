using GameStore.Api.Dtos;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

namespace GameStore.Api.Endpoints;

//static class that will have extension methods
public static class GamesEndPoints
{
    const string GetGameEndpointName = "GetGame";

    private static List<GameDto> games = [
        new(
            1,
            "Road Rash",
            "Racing",
            19.99M,
            new DateOnly(2010, 9, 30)),
        new(
            2,
            "Power Rangers",
            "Fighting",
            29.99M,
            new DateOnly(2011, 4, 30)),
        new(
            3,
            "Tekken 4",
            "Fighting",
            49.99M,
            new DateOnly(2015, 7, 25)),
        new(
            4,
            "The Witcher III",
            "Racing",
            39.99M,
            new DateOnly(2017, 1, 10)),
        new(
            5,
            "Hollow Knight",
            "PLatformer",
            9.99M,
            new DateOnly(2018, 5, 15))
    ];

    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {
        //using route groups
        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games web API -- Read ALL
        group.MapGet("/", () => games);

        // Get /games/{id} -- Read Specifics
        group.MapGet("/{id}", (int id) => 
        {
            GameDto? game = games.Find(game => game.Id == id);
            //if the game doesn't exists
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpointName);

        // POST /games -- Create
        group.MapPost("/", (CreateGameDto newGame) => 
        {
            //what if the name is empty - not possible to do for all fields
            // if(string.IsNullOrEmpty(newGame.Name))
            // {
            //     return Results.BadRequest("Name is Required");
            // }

            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
        });

        // PUT /games -- Update
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => {
            var index = games.FindIndex(game => game.Id == id);

            //if the game doesn't exist
            if(index == -1)
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

        //DELETE game/{id}
        group.MapDelete("/{id}", (int id) =>{
            games.RemoveAll(games => games.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
