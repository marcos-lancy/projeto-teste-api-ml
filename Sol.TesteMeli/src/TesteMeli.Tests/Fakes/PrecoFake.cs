using Bogus;
using TesteMeli.Business.ValueObjects;

namespace TesteMeli.Tests.Fakes;

public static class PrecoFake
{
    private static readonly Faker<Preco> _precoFaker = new Faker<Preco>("pt_BR")
            .CustomInstantiator(f => new Preco(
                valor: f.Finance.Amount(10, 1000, 2),
                moeda: f.PickRandom("BRL", "USD", "EUR")
            ));

    public static Preco Gerar()
    {
        return _precoFaker.Generate();
    }

    public static Preco GerarPrecoAlto()
    {
        return new Preco(
            valor: BogusConfig.Faker.Finance.Amount(1000, 10000, 2),
            moeda: "BRL"
        );
    }

    public static Preco GerarPrecoBaixo()
    {
        return new Preco(
            valor: BogusConfig.Faker.Finance.Amount(1, 50, 2),
            moeda: "BRL"
        );
    }

    public static Preco GerarValorZero()
    {
        return new Preco(0m, "BRL");
    }

    public static Preco GerarPrecoComMoedaEspecifica(string moeda)
    {
        return new Preco(
            valor: BogusConfig.Faker.Finance.Amount(10, 500, 2),
            moeda: moeda
        );
    }

    public static List<Preco> GerarLista(int quantidade = 5)
    {
        return _precoFaker.Generate(quantidade);
    }
}