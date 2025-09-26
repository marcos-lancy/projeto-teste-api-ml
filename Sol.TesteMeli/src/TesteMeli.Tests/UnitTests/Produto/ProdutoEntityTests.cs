using FluentAssertions;
using TesteMeli.Business.Entity;
using TesteMeli.Shared.Exceptions;
using TesteMeli.Shared.Language;
using TesteMeli.Tests.Fakes;

namespace TesteMeli.Tests.UnitTests.Produto;

public class ProdutoEntityTests
{
    [Fact(DisplayName = "Dado Parâmetros Válidos, Quando Criar Produto, Então Deve Ser Criado Com Sucesso")]
    public void DadoParametrosValidos_QuandoCriarProduto_EntaoDeveSerCriadoComSucesso()
    {
        // Arrange & Act
        var produto = ProdutoFake.Gerar();

        // Assert
        produto.Should().NotBeNull();
        produto.Nome.Should().NotBeNullOrWhiteSpace();
        produto.Descricao.Should().NotBeNullOrWhiteSpace();
        produto.Preco.Valor.Should().BeGreaterThan(0);
        produto.Classificacao.Valor.Should().BeInRange(1.0, 5.0);
        produto.Especificacoes.Should().HaveCountGreaterThanOrEqualTo(3);
        produto.Id.Should().NotBe(Guid.Empty);
        produto.Imagem.Url.Should().NotBeNullOrWhiteSpace();
    }

    [Fact(DisplayName = "Dado Quantidade de Produtos, Quando Gerar Lista, Então Deve Retornar Lista Com Produtos Diversos")]
    public void DadoQuantidadeDeProdutos_QuandoGerarLista_EntaoDeveRetornarListaComProdutosDiversos()
    {
        // Arrange & Act
        var produtos = ProdutoFake.GerarLista(5);

        // Assert
        produtos.Should().HaveCount(5);
        produtos.Should().AllSatisfy(p =>
        {
            p.Should().NotBeNull();
            p.Nome.Should().NotBeNullOrWhiteSpace();
            p.Preco.Valor.Should().BeGreaterThan(0);
        });

        var nomesUnicos = produtos.Select(p => p.Nome).Distinct();
        nomesUnicos.Should().HaveCountGreaterThan(1);
    }

    [Fact(DisplayName = "Dado Produto Eletrônico, Quando Verificar Especificações, Então Deve Conter Especificações Técnicas")]
    public void DadoProdutoEletronico_QuandoVerificarEspecificacoes_EntaoDeveConterEspecificacoesTecnicas()
    {
        // Arrange & Act
        var produto = ProdutoFake.GerarEletronico();

        // Assert
        produto.Especificacoes.Should().ContainKeys("Tela", "Memória", "Processador");
        produto.Preco.Valor.Should().BeGreaterThanOrEqualTo(500);
        produto.Nome.Should().ContainAny("Pro", "Plus", "Max", "Ultra");
        produto.Especificacoes["Tela"].Should().Contain("polegadas");
        produto.Especificacoes["Memória"].Should().ContainAny("GB", "MB");
    }

    [Fact(DisplayName = "Dado Produto Com Classificação, Quando Adicionar Avaliação Válida, Então Deve Recalcular Média Corretamente")]
    public void DadoProdutoComClassificacao_QuandoAdicionarAvaliacaoValida_EntaoDeveRecalcularMediaCorretamente()
    {
        // Arrange & Act
        var produto = ProdutoFake.GerarLivro();

        // Assert
        produto.Especificacoes.Should().ContainKeys("Autor", "ISBN", "Páginas");
        produto.Preco.Valor.Should().BeLessThanOrEqualTo(100);
        produto.Descricao.Should().NotBeNullOrWhiteSpace();
        produto.Especificacoes["Páginas"].Should().NotBeNullOrWhiteSpace();
    }

    [Fact(DisplayName = "Dado Nota Inválida, Quando Adicionar Avaliação, Então Deve Lançar Exceção")]
    public void DadoNotaInvalida_QuandoAdicionarAvaliacao_EntaoDeveLancarExcecao()
    {
        // Arrange
        var produto = ProdutoFake.Gerar();
        var notaInvalida = 6.0;

        // Act
        var action = () => produto.AdicionarAvaliacao(notaInvalida);

        // Assert
        action.Should().Throw<CustomExceptionBase>().WithMessage(Mensagens.NotaInvalida);
    }

    [Fact(DisplayName = "Dado Nome Vazio, Quando Criar Produto, Então Deve Lançar Exceção")]
    public void DadoNomeVazio_QuandoCriarProduto_EntaoDeveLancarExcecao()
    {
        // Arrange
        var nome = "";
        var descricao = "Descrição válida";
        var preco = PrecoFake.Gerar();
        var imagem = ImagemProdutoFake.Gerar();
        var classificacao = ClassificacaoFake.Gerar();
        var especificacoes = new Dictionary<string, string>();

        // Act
        var action = () => new ProdutoEntity(nome, descricao, preco, imagem, classificacao, especificacoes);

        // Assert
        action.Should().Throw<CustomExceptionBase>().WithMessage(string.Format(Mensagens.StrNulaOuVazia, nameof(nome)));
    }

    [Fact(DisplayName = "Dado Descricao Vazia, Quando Criar Produto, Então Deve Lançar Exceção")]
    public void DadoDescricaoVazia_QuandoCriarProduto_EntaoDeveLancarExcecao()
    {
        // Arrange
        var nome = "Nome";
        var descricao = "";
        var preco = PrecoFake.Gerar();
        var imagem = ImagemProdutoFake.Gerar();
        var classificacao = ClassificacaoFake.Gerar();
        var especificacoes = new Dictionary<string, string>();

        // Act
        var action = () => new ProdutoEntity(nome, descricao, preco, imagem, classificacao, especificacoes);

        // Assert
        action.Should().Throw<CustomExceptionBase>().WithMessage(string.Format(Mensagens.StrNulaOuVazia, nameof(descricao)));
    }

    [Fact(DisplayName = "Dado Preco Vazio, Quando Criar Produto, Então Deve Lançar Exceção")]
    public void DadoPrecoVazio_QuandoCriarProduto_EntaoDeveLancarExcecao()
    {
        // Arrange
        var nome = "Nome";
        var descricao = "Descrição válida";
        //var preco = default;
        var imagem = ImagemProdutoFake.Gerar();
        var classificacao = ClassificacaoFake.Gerar();
        var especificacoes = new Dictionary<string, string>();

        // Act
        var action = () => new ProdutoEntity(nome, descricao, null, imagem, classificacao, especificacoes);

        // Assert
        action.Should().Throw<CustomExceptionBase>().WithMessage(string.Format(Mensagens.ObjNulo, "preco"));
    }

    [Fact(DisplayName = "Adicionar Avaliação, Em Produto, Deve Recalcular Corretamente")]
    public void AdicionarAvaliacao_EmProduto_DeveRecalcularCorretamente()
    {
        // Arrange
        var produto = ProdutoFake.Gerar();
        var classificacaoInicial = produto.Classificacao.Valor;
        var novaNota = 5.0;

        // Act
        produto.AdicionarAvaliacao(novaNota);

        // Assert
        produto.Classificacao.Valor.Should().BeGreaterThanOrEqualTo(classificacaoInicial);
    }
}
