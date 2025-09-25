namespace TesteMeli.Api.Dtos.Auth;

public class LoginResponse
{
    public string Token { get; init; }

    public LoginResponse(string token)
    {
        Token = token;
    }
}
