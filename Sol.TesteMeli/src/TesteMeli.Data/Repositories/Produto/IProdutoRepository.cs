using TesteMeli.Data.Repositories.Produto.Dtos;
using TesteMeli.Sahred.Enum;

namespace TesteMeli.Data.Repositories.Produto;

public interface IProdutoRepository
{
    Task<ProdutoDto> AdicionarAsync(ProdutoDto produto);
    Task AtualizarAsync(ProdutoDto produto);
    Task RemoverAsync(Guid id);
    Task<PaginacaoDto<ProdutoDto>> ObterTodosAsync(
        int pagina = 1,
        int itensPorPagina = 10,
        OrdenacaoProduto ordenacao = OrdenacaoProduto.NomeAscendente);
    Task<ProdutoDto> ObterPorIdAsync(Guid id);
    Task<bool> ExisteAsync(Guid id);
}
