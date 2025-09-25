namespace TesteMeli.Api.Dtos.Produto;

public record ProdutosResponse(
    Guid Id,
    string Nome,
    string Descricao,
    PrecoResponse Preco,
    ImagemResponse Imagem,
    ClassificacaoResponse Classificacao,
    Dictionary<string, string> Especificacoes
);

public record PrecoResponse(string Valor, string Moeda);

public record ImagemResponse(string Url);

public record ClassificacaoResponse(double Valor);