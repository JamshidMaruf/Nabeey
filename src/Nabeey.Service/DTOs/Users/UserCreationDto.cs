using System.ComponentModel.DataAnnotations;

namespace Nabeey.Service.DTOs.Users;

public class UserCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Required, Phone]
    public string Phone { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
