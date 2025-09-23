using TesteMeli.Business.Language;

namespace TesteMeli.Business.ValueObjects;

public record Preco
{
    /// <summary>
    /// Valor produto
    /// </summary>
    public decimal Valor { get; init; }

    /// <summary>
    /// Tipo de moeda (ISO 4217)
    /// </summary>
    public string Moeda { get; init; } = "BRL";

    public Preco(decimal valor, string moeda = "BRL")
    {
        Valor = valor;
        Moeda = moeda;

        Validation();
    }

    public override string ToString() => $"{Valor:C} ({Moeda})";

    public void Validation()
    {
        Validacoes.ValidarSeMenorQue(Valor, 1, string.Format(Mensagens.ValorZeroOuNegativo, nameof(Valor)));
    }
}
