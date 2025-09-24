using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TesteMeli.Api.Dtos.Produto;
using TesteMeli.Business.Exceptions;

namespace TesteMeli.Api.Controllers.v1;

[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
//[Authorize]
public class ProdutosController : MainController
{
    /// <summary>
    /// Obtém uma lista de todos os produtos cadastrados no sistema.
    /// </summary>
    [SwaggerOperation(
            Summary = "Lista todos os produtos.",
            Description = "Retorna uma lista com todos os produtos cadastrados no sistema."
        )]
    [ProducesResponseType(typeof(List<ProdutosResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [SwaggerOperation(
           Summary = "Obtém um produto por ID.",
           Description = "Retorna os detalhes de um produto específico a partir do seu identificador."
       )]
    [ProducesResponseType(typeof(ProdutosResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
    {
        return Ok();
    }

    /// <summary>
    /// Cadastra um novo produto no sistema.
    /// </summary>
    /// <param name="dto">Dados do novo produto</param>
    [SwaggerOperation(
        summary: "Cadastrar novo produto", 
        Description = "Adiciona novo produto ao sistema. Requer permissão de administrador.")]
    [ProducesResponseType(typeof((string, ProdutosResponse)), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] CriarProdutoRequest dto)
    {
        return Ok();
    }

    /// <summary>
    /// Atualiza os dados de um produto existente.
    /// </summary>
    /// <param name="id">ID do produto.</param>
    /// <param name="dto">Novo dados do produto.</param>
    [SwaggerOperation(
        summary: "Atualiza um produto",
        Description = "Atualiza as informações de um produto existente. Requer permissão de administrador.")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] AtualizarProdutoRequest dto)
    {
        return Ok();
    }

    [SwaggerOperation(
            Summary = "Remove um produto.",
            Description = "Remove um produto do sistema pelo seu identificador. Requer permissão de administrador."
        )]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover([FromRoute] Guid id)
    {
        return Ok();
    }
}
