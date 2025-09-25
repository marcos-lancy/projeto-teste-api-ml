using Microsoft.Extensions.Logging;
using System.Security.Authentication;
using TesteMeli.Business.Interfaces;

namespace TesteMeli.Business.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IJwtService _jwtService;
    private readonly ILogger<UsuarioService> _logger;

    public UsuarioService(IJwtService jwtService,
        ILogger<UsuarioService> logger)
    {
        _jwtService = jwtService;
        _logger = logger;
    }

    public async Task<string> EfetuarLoginAsync(string email, string senha)
    {
        _logger.LogInformation($"Tentativa de efetuar o login. E-mail: {email}");

        // Aqui implementaria a lógica para validar o usuário.

        // Apenas um exemplo de erro de autenticação
        if (email != "admin@gmail.com")
            throw new AuthenticationException("Houve um erro ao efetuar login, verifique os dados e tente novamente.");

        // Apenas um exemplo se trabalharmos com Roles
        string role = "ADMIN";

        return _jwtService.GerarToken(email, role);
    }
}
