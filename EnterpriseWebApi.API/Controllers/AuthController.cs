using EnterpriseWebApi.API.Auth;
using EnterpriseWebApi.Domain.Entities;
using EnterpriseWebApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenGenerator _tokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;


    public AuthController(
       JwtTokenGenerator tokenGenerator,
       IRefreshTokenRepository refreshTokenRepository)
    {
        _tokenGenerator = tokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        // Demo user
        var userId = Guid.NewGuid();
        var role = "Admin";
        var email = "admin@test.com";

        var accessToken = _tokenGenerator.GenerateToken(userId, email, role);
        var refreshTokenValue = _tokenGenerator.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = refreshTokenValue,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _refreshTokenRepository.AddAsync(refreshToken);

        return Ok(new AuthResponse(accessToken, refreshTokenValue));
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetAsync(refreshToken);

        if (storedToken is null ||
            storedToken.IsRevoked ||
            storedToken.ExpiresAt < DateTime.UtcNow)
        {
            return Unauthorized();
        }

        // Rotate token (VERY IMPORTANT)
        storedToken.IsRevoked = true;
        await _refreshTokenRepository.UpdateAsync(storedToken);

        var newAccessToken = _tokenGenerator.GenerateToken(
            storedToken.UserId,
            "admin@test.com",
            "Admin");

        var newRefreshTokenValue = _tokenGenerator.GenerateRefreshToken();

        await _refreshTokenRepository.AddAsync(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = storedToken.UserId,
            Token = newRefreshTokenValue,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        });

        return Ok(new AuthResponse(newAccessToken, newRefreshTokenValue));
    }



}
