using TesteMeli.Shared.Enum;

namespace TesteMeli.Api.Dtos.Produto;

public record ProdutosPaginadosResponse(
    IReadOnlyList<ProdutosResponse> Itens,
    int PaginaAtual,
    int ItensPorPagina,
    int TotalItens,
    int TotalPaginas,
    OrdenacaoProduto OrdenacaoAtual,
    bool TemPaginaAnterior,
    bool TemProximaPagina
);