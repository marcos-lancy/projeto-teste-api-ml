using Bogus;
using TesteMeli.Business.ValueObjects;

namespace TesteMeli.Tests.Fakes;

public static class ImagemProdutoFake
{
    private static readonly Faker<ImagemProduto> _imagemFaker = new Faker<ImagemProduto>("pt_BR")
            .CustomInstantiator(f => new ImagemProduto(
                url: f.Image.PicsumUrl()
            ));

    public static ImagemProduto Gerar()
    {
        return _imagemFaker.Generate();
    }

    public static ImagemProduto GerarComTextoAlternativo(string textoAlt)
    {
        return new ImagemProduto(
            url: BogusConfig.Faker.Image.PicsumUrl()
        );
    }

    public static ImagemProduto GerarImagemLocal()
    {
        return new ImagemProduto(
            url: $"/images/{BogusConfig.Faker.System.FileName("jpg")}"
        );
    }
}