namespace TesteMeli.Api.Dtos.Produto;

public record ProdutosResponse(
    Guid Id,
    string Nome,
    string ImagemUrl,
    string Descricao,
    string Preco,
    double Classificacao,
    Dictionary<string, string> Especificacoes
);