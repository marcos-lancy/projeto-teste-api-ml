using Bogus;
using TesteMeli.Business.ValueObjects;

namespace TesteMeli.Tests.Fakes;

public static class ClassificacaoFake
{
    private static readonly Faker<Classificacao> _classificacaoFaker = new Faker<Classificacao>("pt_BR")
            .CustomInstantiator(f => new Classificacao(
                valor: Math.Round(f.Random.Double(1, 5))
            ));

    public static Classificacao Gerar()
    {
        return _classificacaoFaker.Generate();
    }

    public static Classificacao GerarClassificacaoAlta()
    {
        return new Classificacao(
            valor: Math.Round(BogusConfig.Faker.Random.Double(4.5, 5.0), 1)
        );
    }

    public static Classificacao GerarClassificacaoBaixa()
    {
        return new Classificacao(
            valor: Math.Round(BogusConfig.Faker.Random.Double(1.0, 2.5), 1)
        );
    }

    public static Classificacao GerarSemAvaliacoes()
    {
        return new Classificacao(0);
    }
}