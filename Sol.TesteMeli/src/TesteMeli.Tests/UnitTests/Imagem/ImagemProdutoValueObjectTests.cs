using FluentAssertions;
using TesteMeli.Business.Exceptions;
using TesteMeli.Business.Language;
using TesteMeli.Business.ValueObjects;
using TesteMeli.Tests.Fakes;

namespace TesteMeli.Tests.UnitTests.Imagem;

public class ImagemProdutoValueObjectTests
{
    [Fact(DisplayName = "Dado URL Válida, Quando Criado, Então Deve Conter URL")]
    public void DadoUrlValida_QuandoCriado_EntaoDeveConterUrl()
    {
        // Arrange
        var imagem = ImagemProdutoFake.Gerar();

        // Act
        var url = imagem.Url;

        // Assert
        url.Should().NotBeNullOrEmpty();
    }

    [Theory(DisplayName = "Dado URL Nula ou Vazia, Quando Criado, Então Deve Lançar Exceção")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DadoUrlInvalida_QuandoCriado_EntaoDeveLancarExcecao(string urlInvalida)
    {
        // Act
        Action action = () => new ImagemProduto(urlInvalida);

        // Assert
        action
            .Should()
            .Throw<CustomExceptionBase>()
            .WithMessage(string.Format(Mensagens.StrNulaOuVazia, nameof(ImagemProduto.Url)));
    }
}
