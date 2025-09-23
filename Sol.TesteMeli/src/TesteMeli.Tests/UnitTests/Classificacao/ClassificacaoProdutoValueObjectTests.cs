using FluentAssertions;
using TesteMeli.Tests.Fakes;

namespace TesteMeli.Tests.UnitTests.Classificacao;

public class ClassificacaoProdutoValueObjectTests
{
    [Fact(DisplayName = "Dado Nota Válida, Quando Criada, Então Deve Conter Valor")]
    public void DadoNotaValida_QuandoCriada_EntaoDeveConterValor()
    {
        // Arrange
        var classificacao = ClassificacaoFake.Gerar();

        // Act
        var valor = classificacao.Valor;

        // Assert
        valor.Should().BeInRange(0, 5);
    }

    [Fact(DisplayName = "Dado Sem Avaliações, Quando Criado, Então Deve Ter Valor Zero")]
    public void DadoSemAvaliacoes_QuandoCriado_EntaoDeveTerValorZero()
    {
        // Arrange
        var classificacao = ClassificacaoFake.GerarSemAvaliacoes();

        // Act
        var valor = classificacao.Valor;

        // Assert
        valor.Should().Be(0);
    }
}
