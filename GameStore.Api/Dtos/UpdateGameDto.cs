using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class UpdateGameDto(
    [Required][StringLength(30)]string Name, 
    [Required][StringLength(30)]string Genre, 
    [Required][Range(1, 200)]decimal Price, 
    DateOnly ReleaseDate
);
