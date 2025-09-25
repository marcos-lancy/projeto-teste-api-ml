using Swashbuckle.AspNetCore.Annotations;

namespace TesteMeli.Api.Dtos.Produto;

public class AtualizarProdutoRequest
{
    public string Nome { get; init; }

    public string Descricao { get; init; }

    public decimal Preco { get; init; }

    public string ImagemUrl { get; init; }

    public double Classificacao { get; init; }

    [SwaggerSchema("Especificações do produto como pares chave-valor")]
    public EspecificacoesDictionaryContract Especificacoes { get; set; } = new();
}