using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    [Required][StringLength(30)] string Name, 
    [Required][StringLength(30)] string Genre, 
    [Range(1, 200)] decimal Price, 
    DateOnly ReleaseDate
);