using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TesteMeli.Api.Dtos.Auth;
using TesteMeli.Business.Exceptions;

namespace TesteMeli.Api.Controllers.v1;

/// <summary>
/// Responsável pelos endpoints de autenticação e registro de usuários.
/// </summary>
[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : MainController
{
    /// <summary>
    /// Realiza o login de um usuário.
    /// </summary>
    /// <param name="request">Dados de login do usuário.</param>
    /// <returns>Token de autenticação.</returns>
    [SwaggerOperation(
        Summary = "Autentica o usuário. [login: \"admin@gmail.com\" - senha: \"Admin@123\"]",
        Description = "Realiza o login do usuário e retorna um token JWT para autenticação."
    )]
    [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpPost("entrar")]
    public async Task<IActionResult> Entrar([FromBody] LoginRequest request)
    {
        return Ok();
    }
}
