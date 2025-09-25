namespace TesteMeli.Data.Repositories.Produto.Dtos;

public record ProdutoDto(
    Guid Id,
    string Nome,
    string Descricao,
    PrecoDto Preco,
    ImagemDto Imagem,
    ClassificacaoDto Classificacao,
    Dictionary<string, string> Especificacoes
);

public record PrecoDto(decimal Valor, string Moeda);

public record ImagemDto(string Url);

public record ClassificacaoDto(double Valor);