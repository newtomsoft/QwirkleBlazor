namespace Qwirkle.WebApi.Shared;

public class UserModel
{
    [Required]
    public string Pseudo { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    public string Firstname { get; set; } = null!;

    [Required]
    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;


    public User ToUser() => new(Pseudo, Email, Firstname, Lastname);
}