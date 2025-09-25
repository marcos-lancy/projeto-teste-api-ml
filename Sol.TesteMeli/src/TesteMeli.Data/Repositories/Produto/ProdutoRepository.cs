using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TesteMeli.Data.Repositories.Produto.Dtos;

namespace TesteMeli.Data.Repositories.Produto;

public sealed class ProdutoRepository : RepositoryBase<ProdutoDto>, IProdutoRepository
{
    private const int ITENS_POR_PAGINA_DEFAULT = 10;
    private const int PAGINA_DEFAULT = 1;

    public ProdutoRepository(IConfiguration configuration, ILogger<ProdutoRepository> logger)
        : base(ObterCaminhoArquivo(configuration), logger) { }
    
    private static string ObterCaminhoArquivo(IConfiguration configuration)
    {
        var dataPath = configuration["DataPath"] ?? "/app/data";
        var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

        if (isDocker)
            return dataPath.StartsWith("/") ? Path.Combine(dataPath, "produtos.json") : $"/app/{dataPath}/produtos.json";

        var basePath = Directory.GetCurrentDirectory();
        return Path.Combine(basePath, dataPath.Replace("/app/", ""), "produtos.json");
    }

    public async Task<ProdutoDto> AdicionarAsync(ProdutoDto produto)
    {
        var produtoComId = produto with { Id = produto.Id };
        Dados.Add(produtoComId);
        await Task.Run(() => SalvarDados());
        return produtoComId;
    }

    public async Task AtualizarAsync(ProdutoDto produto)
    {
        var index = Dados.FindIndex(p => p.Id == produto.Id);
        if (index == -1)
            throw new KeyNotFoundException($"Produto com ID {produto.Id} não encontrado");

        Dados[index] = produto;
        await Task.Run(() => SalvarDados());
    }

    public async Task RemoverAsync(Guid id)
    {
        var produto = Dados.FirstOrDefault(p => p.Id == id);

        Dados.Remove(produto);
        await Task.Run(() => SalvarDados());
    }

    /// <summary>
    /// Foco no desempenho para grandes volumes de dados.
    /// Consulta todos os produtos com paginação e ordenação.
    /// </summary>
    /// <param name="pagina">Número da página</param>
    /// <param name="itensPorPagina">Quantidade de itens por página</param>
    /// <param name="ordenacao">Tipo de ordenação</param>
    /// <returns></returns>
    public async Task<PaginacaoDto<ProdutoDto>> ObterTodosAsync(
        int pagina = 1,
        int itensPorPagina = 10,
        OrdenacaoProduto ordenacao = OrdenacaoProduto.NomeAscendente)
    {
        if (pagina < 1) pagina = PAGINA_DEFAULT;
        if (itensPorPagina < 1) itensPorPagina = ITENS_POR_PAGINA_DEFAULT;
        if (itensPorPagina > 100) itensPorPagina = 100;

        var totalItens = Dados.Count;
        var totalPaginas = (int)Math.Ceiling(totalItens / (double)itensPorPagina);

        if (pagina > totalPaginas && totalPaginas > 0)
            pagina = totalPaginas;

        // Aplica ordenação
        var queryOrdenada = AplicarOrdenacao(Dados, ordenacao);

        // Aplica paginação
        var itensPaginados = queryOrdenada
            .Skip((pagina - 1) * itensPorPagina)
            .Take(itensPorPagina)
            .ToList()
            .AsReadOnly();

        return await Task.FromResult(new PaginacaoDto<ProdutoDto>(
            Itens: itensPaginados,
            PaginaAtual: pagina,
            ItensPorPagina: itensPorPagina,
            TotalItens: totalItens,
            TotalPaginas: totalPaginas,
            OrdenacaoAtual: ordenacao
        ));
    }

    // Método auxiliar para aplicar ordenação
    private IOrderedEnumerable<ProdutoDto> AplicarOrdenacao(List<ProdutoDto> produtos, OrdenacaoProduto ordenacao)
    {
        return ordenacao switch
        {
            OrdenacaoProduto.NomeAscendente => produtos.OrderBy(p => p.Nome),
            OrdenacaoProduto.NomeDescendente => produtos.OrderByDescending(p => p.Nome),
            _ => produtos.OrderBy(p => p.Nome)
        };
    }

    // Método auxiliar para ordenação simples
    public async Task<IReadOnlyList<ProdutoDto>> ObterTodosOrdenadosAsync(OrdenacaoProduto ordenacao)
    {
        var produtosOrdenados = AplicarOrdenacao(Dados, ordenacao).ToList().AsReadOnly();
        return await Task.FromResult(produtosOrdenados);
    }

    public async Task<ProdutoDto> ObterPorIdAsync(Guid id)
        => await Task.FromResult(Dados.FirstOrDefault(p => p.Id == id));

    public async Task<bool> ExisteAsync(Guid id)
        => await Task.FromResult(Dados.Any(p => p.Id == id));
}