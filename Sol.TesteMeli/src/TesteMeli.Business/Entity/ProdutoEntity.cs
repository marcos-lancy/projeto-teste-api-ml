using TesteMeli.Business.Interfaces;
using TesteMeli.Business.ValueObjects;
using TesteMeli.Sahred.Language;

namespace TesteMeli.Business.Entity;

/// <summary>
/// Classe que representa um produto no sistema.
/// </summary>
public class ProdutoEntity : EntityBase<Guid>, IEntityBase<Guid>, IAggregateRoot
{
    /// <summary>
    /// Nome do produto
    /// </summary>
    public string Nome { get; private set; }
    /// <summary>
    /// Descrição detalhada do produto
    /// </summary>
    public string Descricao { get; private set; }
    public Preco Preco { get; private set; }
    public ImagemProduto Imagem { get; private set; }
    public Classificacao Classificacao { get; private set; }

    /// <summary>
    /// Escolha pelo Dictionary foi para simplificar o uso com json.
    /// </summary>
    public Dictionary<string, string> Especificacoes { get; private set; }

    public ProdutoEntity(string nome, string descricao, Preco preco,
        ImagemProduto imagem, Classificacao classificacao,
        Dictionary<string, string> especificacoes): base(Guid.NewGuid())
    {
        
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Imagem = imagem;
        Classificacao = classificacao;
        Especificacoes = especificacoes ?? new Dictionary<string, string>();
        
        Validation();
    }

    public ProdutoEntity(Guid id, string nome, string descricao, Preco preco,
    ImagemProduto imagem, Classificacao classificacao,
    Dictionary<string, string> especificacoes) : base(id)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Imagem = imagem;
        Classificacao = classificacao;
        Especificacoes = especificacoes ?? new Dictionary<string, string>();

        Validation();
    }

    public void AdicionarImagem(string url)
    {
        Imagem = Imagem.AdicionarImagem(url);
    }

    public void AdicionarAvaliacao(double nota)
    {
        Classificacao = Classificacao.AdicionarAvaliacao(nota);
    }

    public override string ToString()
    {
        return $"{Nome} - {Preco.Valor:C} ({Classificacao.Valor})";
    }

    public void Validation()
    {
        Validacoes.ValidarSeVazio(Nome, string.Format(Mensagens.StrNulaOuVazia, nameof(Nome)));
        Validacoes.ValidarSeVazio(Descricao, string.Format(Mensagens.StrNulaOuVazia, nameof(Descricao)));
        Validacoes.ValidarSeNulo(Preco, string.Format(Mensagens.ObjNulo, nameof(Preco)));
    }
}
