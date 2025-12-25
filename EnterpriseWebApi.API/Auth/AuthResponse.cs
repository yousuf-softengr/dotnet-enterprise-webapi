namespace EnterpriseWebApi.API.Auth;

public record AuthResponse(
    string AccessToken,
    string RefreshToken
);
