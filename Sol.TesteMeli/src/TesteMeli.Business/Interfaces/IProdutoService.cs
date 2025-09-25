using TesteMeli.Business.Entity;
using TesteMeli.Data.Repositories.Produto.Dtos;
using TesteMeli.Sahred.Enum;

namespace TesteMeli.Business.Interfaces;

public interface IProdutoService
{
    Task<ProdutoEntity> AdicionarAsync(ProdutoEntity produto);
    Task AtualizarAsync(ProdutoEntity produto);
    Task RemoverAsync(Guid id);
    Task<PaginacaoDto<ProdutoEntity>> ConsultarTodosAsync(int pagina, int itensPorPagina, OrdenacaoProduto ordenacao);
    Task<ProdutoEntity> ObterPorId(Guid id);
}
