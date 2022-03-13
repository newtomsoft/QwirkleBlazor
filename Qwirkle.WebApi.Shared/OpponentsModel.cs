namespace Qwirkle.WebApi.Shared;

public class OpponentsModel
{
    [Required]
    public string Opponent1 { get; set; } = null!;

    public string? Opponent2 { get; set; }
    public string? Opponent3 { get; set; }
}
