using Microsoft.Extensions.Logging;
using TesteMeli.Business.Entity;
using TesteMeli.Business.Interfaces;
using TesteMeli.Business.ValueObjects;
using TesteMeli.Data.Repositories.Produto;
using TesteMeli.Data.Repositories.Produto.Dtos;
using TesteMeli.Shared.Enum;
using TesteMeli.Shared.Exceptions;

namespace TesteMeli.Business.Services;

public sealed class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ILogger<ProdutoService> _logger;

    public ProdutoService(IProdutoRepository produtoRepository,
        ILogger<ProdutoService> logger)
    {
        _produtoRepository = produtoRepository;
        _logger = logger;
    }

    public async Task<ProdutoEntity> AdicionarAsync(ProdutoEntity produto)
    {
        await _produtoRepository.AdicionarAsync(MapperParaDto(produto));

        return produto;
    }

    public async Task AtualizarAsync(ProdutoEntity produto)
    {
        var produtoExistente = await _produtoRepository.ObterPorIdAsync(produto.Id);
        if (produtoExistente is null)
        {
            _logger.LogWarning($"Atualizar. Produto de ID: {produto.Id} não com encontrado.");
            throw new NotFoundException();
        }

        await _produtoRepository.AtualizarAsync(MapperParaDto(produto));
    }

    public async Task RemoverAsync(Guid id)
    {
        var produtoExistente = await _produtoRepository.ObterPorIdAsync(id);
        if (produtoExistente is null)
        {
            _logger.LogWarning($"Remover. Produto de ID: {id} não com encontrado.");
            throw new NotFoundException();
        }

        await _produtoRepository.RemoverAsync(id);
    }

    public async Task<PaginacaoDto<ProdutoEntity>> ConsultarTodosAsync(int pagina, int itensPorPagina, OrdenacaoProduto ordenacao)
    {
        var resultadoPaginado = await _produtoRepository.ObterTodosAsync(pagina, itensPorPagina, ordenacao);

        var itensEntity = resultadoPaginado.Itens
        .Select(dto => new ProdutoEntity(
            id: dto.Id,
            nome: dto.Nome,
            descricao: dto.Descricao,
            preco: new Preco(dto.Preco.Valor, dto.Preco.Moeda),
            imagem: new ImagemProduto(dto.Imagem.Url),
            classificacao: new Classificacao(dto.Classificacao.Valor),
            especificacoes: new Dictionary<string, string>(dto.Especificacoes)
        ))
        .ToList()
        .AsReadOnly();

        return new PaginacaoDto<ProdutoEntity>(
            Itens: itensEntity,
            PaginaAtual: resultadoPaginado.PaginaAtual,
            ItensPorPagina: resultadoPaginado.ItensPorPagina,
            TotalItens: resultadoPaginado.TotalItens,
            TotalPaginas: resultadoPaginado.TotalPaginas,
            OrdenacaoAtual: resultadoPaginado.OrdenacaoAtual);
    }

    public async Task<ProdutoEntity> ObterPorId(Guid id)
    {
        var produtoConsultado = await _produtoRepository.ObterPorIdAsync(id);

        if (produtoConsultado is null)
        {
            _logger.LogWarning($"Obter por id. Produto de ID: {id} não com encontrado.");
            throw new NotFoundException();
        }

        return new ProdutoEntity(
            produtoConsultado.Id,
            produtoConsultado.Nome,
            produtoConsultado.Descricao,
            new Preco(produtoConsultado.Preco.Valor, produtoConsultado.Preco.Moeda),
            new ImagemProduto(produtoConsultado.Imagem.Url),
            new Classificacao(produtoConsultado.Classificacao.Valor),
            new Dictionary<string, string>(produtoConsultado.Especificacoes));
    }

    private static ProdutoDto MapperParaDto(ProdutoEntity produto)
    {
        return new ProdutoDto
        (
            produto.Id,
            produto.Nome,
            produto.Descricao,
            new PrecoDto(produto.Preco.Valor, produto.Preco.Moeda),
            new ImagemDto(produto.Imagem.Url),
            new ClassificacaoDto(produto.Classificacao.Valor),
            new Dictionary<string, string>(produto.Especificacoes)
        );
    }
}
