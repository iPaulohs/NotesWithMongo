using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.DataTransfer.Input.UserDataTransferInput;
using Notes.Identity;

namespace Notes.Controllers;

[Route("[controller]")]
[ApiController]
public partial class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;

    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] UserInputRegister userInput)
    {
        User user = new()
        {
            Id = $"[{userInput.Name.GetHashCode().ToString().Replace("-", "")}" +
            $"]@[" +
            $"{userInput.Email.GetHashCode().ToString().Replace("-", "")}]",
            Name = userInput.Name,
            Email = userInput.Email,
            UserName = userInput.UserName,
            Password = userInput.Password
        };

        var result = await _userManager.CreateAsync(user, userInput.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _userManager.SetLockoutEnabledAsync(user, false);
        return Ok(GenerateToken(user));
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserInputLogin userInput)
    {
        var user = await _userManager.FindByEmailAsync(userInput.Email);

        if (user == null)
        {
            return BadRequest("User not found.");
        }

        var result = await _signInManager.PasswordSignInAsync(user, userInput.Password, lockoutOnFailure: false, isPersistent: false);

        if (result.Succeeded)
        {
            return Ok(GenerateToken(user));
        }
        return BadRequest("Login failed.");
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return Ok("Logout bem sucedido." );
    }
}
