using TesteMeli.Shared.Enum;

namespace TesteMeli.Data.Repositories.Produto.Dtos;

public record PaginacaoDto<T>(
    IReadOnlyList<T> Itens,
    int PaginaAtual,
    int ItensPorPagina,
    int TotalItens,
    int TotalPaginas,
    OrdenacaoProduto OrdenacaoAtual
)
{
    public bool TemPaginaAnterior => PaginaAtual > 1;
    public bool TemProximaPagina => PaginaAtual < TotalPaginas;
}