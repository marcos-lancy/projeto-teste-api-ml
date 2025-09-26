using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Globalization;
using System.Net;
using TesteMeli.Api.Dtos.Produto;
using TesteMeli.Business.Entity;
using TesteMeli.Business.Interfaces;
using TesteMeli.Business.ValueObjects;
using TesteMeli.Shared.Enum;
using TesteMeli.Shared.Exceptions;

namespace TesteMeli.Api.Controllers.v1;

[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class ProdutosController : MainController
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    /// <summary>
    /// Obtém uma lista paginada de todos os produtos cadastrados no sistema.
    /// </summary>
    [SwaggerOperation(
        Summary = "Lista todos os produtos de forma paginada.",
        Description = "Retorna uma lista paginada com todos os produtos cadastrados no sistema, incluindo metadados de paginação."
    )]
    [ProducesResponseType(typeof(ProdutosPaginadosResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> ObterTodos(
        [FromQuery] int pagina = 1,
        [FromQuery] int itensPorPagina = 10,
        [FromQuery] OrdenacaoProduto ordenacao = OrdenacaoProduto.NomeAscendente)
    {
        var resultadoPaginado = await _produtoService.ConsultarTodosAsync(pagina, itensPorPagina, ordenacao);

        var response = new ProdutosPaginadosResponse(
            Itens: resultadoPaginado.Itens.Select(p => new ProdutosResponse(
                p.Id,
                p.Nome,
                p.Imagem.Url,
                p.Descricao,
                p.Preco.Valor.ToString("C", new CultureInfo("pt-BR")),
                p.Classificacao.Valor,
                p.Especificacoes
            )).ToList().AsReadOnly(),
            PaginaAtual: resultadoPaginado.PaginaAtual,
            ItensPorPagina: resultadoPaginado.ItensPorPagina,
            TotalItens: resultadoPaginado.TotalItens,
            TotalPaginas: resultadoPaginado.TotalPaginas,
            OrdenacaoAtual: resultadoPaginado.OrdenacaoAtual,
            TemPaginaAnterior: resultadoPaginado.TemPaginaAnterior,
            TemProximaPagina: resultadoPaginado.TemProximaPagina
        );

        return Ok(response);
    }

    /// <summary>
    /// Consulta um produto pelo seu ID.
    /// </summary>
    /// <param name="id">ID para consulta</param>
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
        var produto = await _produtoService.ObterPorId(id);
        var produtoResult = new ProdutosResponse
        (
            produto.Id,
            produto.Nome,
            produto.Imagem.Url,
            produto.Descricao,
            produto.Preco.Valor.ToString("C", new CultureInfo("pt-BR")),
            produto.Classificacao.Valor,
            produto.Especificacoes
        );

        return Ok(produtoResult);
    }

    /// <summary>
    /// Cadastra um novo produto no sistema.
    /// </summary>
    /// <param name="dto">Dados do novo produto</param>
    [SwaggerOperation(
        summary: "Cadastrar novo produto", 
        Description = "Adiciona novo produto ao sistema")]
    [ProducesResponseType(typeof((string, ProdutosResponse)), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] CriarProdutoRequest dto)
    {
        var produto = await _produtoService.AdicionarAsync(new ProdutoEntity
            (dto.Nome.Trim(),
            dto.Descricao.Trim(),
            new Preco(dto.Preco),
            new ImagemProduto(dto.ImagemUrl),
            new Classificacao(dto.Classificacao),
            new Dictionary<string, string>(dto.Especificacoes)
        ));

        var produtoResult = new ProdutosResponse
        (
            produto.Id,
            produto.Nome,
            produto.Imagem.Url,
            produto.Descricao,
            produto.Preco.Valor.ToString("C", new CultureInfo("pt-BR")),
            produto.Classificacao.Valor,
            produto.Especificacoes
        );

        return Created($"api/v1/produtos/{produto.Id}", produtoResult);
    }

    /// <summary>
    /// Atualiza os dados de um produto existente.
    /// </summary>
    /// <param name="id">ID do produto.</param>
    /// <param name="dto">Novo dados do produto.</param>
    [SwaggerOperation(
        summary: "Atualiza um produto",
        Description = "Atualiza as informações de um produto existente")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] AtualizarProdutoRequest dto)
    {
        await _produtoService.AtualizarAsync(new ProdutoEntity
            (id,
            dto.Nome,
            dto.Descricao,
            new Preco(dto.Preco),
            new ImagemProduto(dto.ImagemUrl),
            new Classificacao(dto.Classificacao),
            new Dictionary<string, string>(dto.Especificacoes)
        ));

        return NoContent();
    }

    /// <summary>
    /// Remove um produto do sistema.
    /// </summary>
    /// <param name="id">ID do produto.</param>
    [SwaggerOperation(
            Summary = "Remove um produto.",
            Description = "Remove um produto do sistema pelo seu identificador"
        )]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover([FromRoute] Guid id)
    {
        await _produtoService.RemoverAsync(id);
        return NoContent();
    }
}
