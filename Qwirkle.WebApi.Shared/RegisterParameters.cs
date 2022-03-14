namespace Qwirkle.WebApi.Shared;

public class RegisterParameters
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string PasswordConfirm { get; set; }


    public string Firstname { get; set; }
    public string Lastname { get; set; }



    public User ToUser() => new(UserName, Email, Firstname, Lastname);
}