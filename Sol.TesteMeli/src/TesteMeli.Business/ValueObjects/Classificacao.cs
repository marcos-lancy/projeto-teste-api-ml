using TesteMeli.Business.Exceptions;
using TesteMeli.Business.Language;

namespace TesteMeli.Business.ValueObjects;

public record Classificacao
{
    /// <summary>
    /// Valor da classificação
    /// </summary>
    public double Valor { get; init; }

    public Classificacao(double valor)
    {
        Valor = valor;

        Validation();
    }

    internal Classificacao AdicionarAvaliacao(double novaNota)
    {
        if (novaNota < 0 || novaNota > 5)
            throw new CustomExceptionBase(Mensagens.NotaInvalida);

        return this with
        {
            Valor = novaNota
        };
    }

    public void Validation()
    {
        Validacoes.ValidarMinimoMaximo(Valor, 0, 5, Mensagens.NotaInvalida);
    }
}