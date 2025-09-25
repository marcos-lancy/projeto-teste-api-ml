using System.ComponentModel;

namespace TesteMeli.Api.Dtos.Auth;

public class LoginRequest
{
    [DefaultValue("admin@gmail.com")] // Apenas para facilitar os testes via Swagger vindo preenchido
    public string Email { get; init; } = string.Empty;

    [DefaultValue("Admin@123")] // Apenas para facilitar os testes via Swagger vindo preenchido
    public string Senha { get; init; } = string.Empty;
}
