using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Notes.Identity;

public class User : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }

    [Required]
    public string? Password { get; set; }
}
