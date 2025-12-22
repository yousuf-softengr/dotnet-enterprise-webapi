using EnterpriseWebApi.API.Auth;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenGenerator _tokenGenerator;

    public AuthController(JwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        // DEMO user (later replace with DB)
        var token = _tokenGenerator.GenerateToken(
            Guid.NewGuid(),
            "admin@test.com",
            "Admin");

        return Ok(new { accessToken = token });
    }
}
