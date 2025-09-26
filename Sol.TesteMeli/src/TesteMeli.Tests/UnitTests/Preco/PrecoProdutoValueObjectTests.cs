using FluentAssertions;
using TesteMeli.Shared.Exceptions;
using TesteMeli.Shared.Language;
using TesteMeli.Tests.Fakes;

namespace TesteMeli.Tests.UnitTests.Preco;

public class PrecoValueObjectTests
{
    [Fact(DisplayName = "Dado Preço em BRL, Quando Formatado, Então Deve Usar Formato Brasileiro")]
    public void DadoPrecoEmBRL_QuandoFormatado_EntaoDeveUsarFormatoBrasileiro()
    {
        // Arrange
        var preco = PrecoFake.GerarPrecoComMoedaEspecifica("BRL");

        // Act
        var formatoReal = preco.ToString();

        // Assert
        formatoReal.Should().MatchRegex(@"^R\$\s\d{1,3}(\.\d{3})*,\d{2}(\s\(BRL\))?$");
        formatoReal.Should().Contain("R$");
        formatoReal.Should().Contain(",");
    }

    [Fact(DisplayName = "Dado Preço Alto em BRL, Quando Formatado, Então Deve Usar Separadores de Milhar Corretos")]
    public void DadoPrecoAltoEmBRL_QuandoFormatado_EntaoDeveUsarSeparadoresDeMilharCorretos()
    {
        // Arrange
        var preco = PrecoFake.GerarPrecoAlto();

        // Act
        var formatoReal = preco.ToString();

        // Assert
        formatoReal.Should().Contain(".");
        formatoReal.Should().Contain(",");
        formatoReal.Should().StartWith("R$");

        if (preco.Valor >= 1000)
        {
            formatoReal.Should().MatchRegex(@"\d\.\d{3}");
        }
    }

    [Fact(DisplayName = "Dado Preço Inválido, Quando Criado, Então Deve Lançar Exceção")]
    public void DadoPrecoInvalido_QuandoCriado_EntaoDeveLancarExcecao()
    {
        // Arrange & Act
        Action action = () => PrecoFake.GerarValorZero();

        // Assert
        action
            .Should()
            .Throw<CustomExceptionBase>()
            .WithMessage(string.Format(Mensagens.ValorZeroOuNegativo, "Valor"));
    }
}
