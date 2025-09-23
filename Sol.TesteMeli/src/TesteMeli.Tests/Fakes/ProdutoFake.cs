using Bogus;
using TesteMeli.Business.Entity;
using TesteMeli.Business.ValueObjects;

namespace TesteMeli.Tests.Fakes;

public static class ProdutoFake
{
    private static readonly Faker<ProdutoEntity> _produtoFaker = new Faker<ProdutoEntity>("pt_BR")
            .CustomInstantiator(f => new ProdutoEntity(
                nome: f.Commerce.ProductName(),
                descricao: f.Commerce.ProductDescription(),
                preco: PrecoFake.Gerar(),
                imagem: ImagemProdutoFake.Gerar(),
                classificacao: ClassificacaoFake.Gerar(),
                especificacoes: GerarEspecificacoasFake(f)
            ));

    private static Dictionary<string, string> GerarEspecificacoasFake(Faker f)
    {
        return new Dictionary<string, string>
            {
                {"Marca", f.Company.CompanyName()},
                {"Cor", f.Commerce.Color()},
                {"Material", f.Commerce.ProductMaterial()},
                {"Dimensões", $"{f.Random.Int(10, 100)}x{f.Random.Int(10, 100)}x{f.Random.Int(1, 20)}cm"},
                {"Peso", $"{f.Random.Double(0.1, 5.0)}kg"}
            };
    }

    public static ProdutoEntity Gerar()
    {
        return _produtoFaker.Generate();
    }

    public static ProdutoEntity GerarEletronico()
    {
        var faker = BogusConfig.Faker;

        var especificacoes = new Dictionary<string, string>
            {
                {"Marca", faker.Company.CompanyName()},
                {"Modelo", $"MOD-{faker.Random.AlphaNumeric(6)}"},
                {"Tela", $"{faker.Random.Double(5, 15):0.1} polegadas"},
                {"Memória", $"{faker.PickRandom(64, 128, 256)}GB"},
                {"Processador", faker.PickRandom("Intel i5", "Intel i7", "AMD Ryzen 5", "AMD Ryzen 7")},
                {"Sistema Operacional", faker.PickRandom("Windows 11", "macOS", "Linux")}
            };

        return new ProdutoEntity(
            nome: faker.Commerce.ProductName() + " " + faker.PickRandom("Pro", "Plus", "Max", "Ultra"),
            descricao: faker.Commerce.ProductDescription(),
            preco: new Preco(faker.Finance.Amount(500, 5000, 2), "BRL"),
            imagem: new ImagemProduto(faker.Image.PicsumUrl()),
            classificacao: new Classificacao(
                Math.Round(faker.Random.Double(3.5, 5.0), 1)
            ),
            especificacoes: especificacoes
        );
    }

    public static ProdutoEntity GerarLivro()
    {
        var faker = BogusConfig.Faker;

        var especificacoes = new Dictionary<string, string>
            {
                {"Autor", faker.Name.FullName()},
                {"ISBN", faker.Random.Replace("978-###-#####-###-#")},
                {"Editora", faker.Company.CompanyName()},
                {"Páginas", faker.Random.Int(100, 500).ToString()},
                {"Idioma", faker.PickRandom("Português", "Inglês", "Espanhol")},
                {"Encadernação", faker.PickRandom("Capa dura", "Capa comum", "Espiral")}
            };

        return new ProdutoEntity(
            nome: faker.Lorem.Sentence(3),
            descricao: faker.Lorem.Paragraphs(2),
            preco: new Preco(faker.Finance.Amount(20, 100, 2), "BRL"),
            imagem: new ImagemProduto(faker.Image.PicsumUrl()),
            classificacao: new Classificacao(
                Math.Round(faker.Random.Double(4.0, 5.0), 1)
            ),
            especificacoes: especificacoes
        );
    }

    public static ProdutoEntity GerarRoupa()
    {
        var faker = BogusConfig.Faker;

        var especificacoes = new Dictionary<string, string>
            {
                {"Marca", faker.Company.CompanyName()},
                {"Tamanho", faker.PickRandom("P", "M", "G", "GG", "XG")},
                {"Cor", faker.Commerce.Color()},
                {"Material", faker.PickRandom("Algodão", "Poliéster", "Linho", "Seda")},
                {"Composição", "100% " + faker.PickRandom("Algodão", "Poliéster")},
                {"Cuidados", "Lavar a mão, não usar alvejante"}
            };

        return new ProdutoEntity(
            nome: faker.Commerce.ProductAdjective() + " " + faker.Commerce.ProductName(),
            descricao: faker.Commerce.ProductDescription(),
            preco: new Preco(faker.Finance.Amount(30, 200, 2), "BRL"),
            imagem: new ImagemProduto(faker.Image.PicsumUrl()),
            classificacao: new Classificacao(
                Math.Round(faker.Random.Double(3.0, 5.0), 1)
            ),
            especificacoes: especificacoes
        );
    }

    public static List<ProdutoEntity> GerarLista(int quantidade = 5)
    {
        return _produtoFaker.Generate(quantidade);
    }

    public static List<ProdutoEntity> GerarListaParaComparacao(int quantidade = 3)
    {
        var produtos = new List<ProdutoEntity>();

        // Gerar produtos diversificados para teste de comparação
        produtos.Add(GerarEletronico());
        produtos.Add(GerarLivro());
        produtos.Add(GerarRoupa());

        // Adicionar mais se necessário
        for (int i = 3; i < quantidade; i++)
        {
            produtos.Add(Gerar());
        }

        return produtos;
    }
}
