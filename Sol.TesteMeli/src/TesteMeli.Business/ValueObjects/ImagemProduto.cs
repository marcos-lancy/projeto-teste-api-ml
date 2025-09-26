using TesteMeli.Shared.Exceptions;
using TesteMeli.Shared.Language;

namespace TesteMeli.Business.ValueObjects;

public record ImagemProduto
{
    /// <summary>
    /// Url da imagem do produto
    /// </summary>
    public string Url { get; init; }

    public ImagemProduto(string url)
    {
        Url = url;

        Validation();
    }

    internal ImagemProduto AdicionarImagem(string url)
    {
        if(string.IsNullOrEmpty(url))
            throw new CustomExceptionBase(string.Format(Mensagens.StrNulaOuVazia, nameof(url)));

        return this with
        {
            Url = url.Trim()
        };
    }

    public void Validation()
    {
        Validacoes.ValidarSeVazio(Url, string.Format(Mensagens.StrNulaOuVazia, nameof(Url)));
    }
}