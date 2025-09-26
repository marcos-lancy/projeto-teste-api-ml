using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace TesteMeli.Data.Repositories;

/// <summary>
/// Classe base para repositórios que armazenam dados em arquivos JSON.
/// Classe genérica que pode ser utilizada para qualquer tipo de entidade. Em nosso caso é usada para ProdutoDto.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class RepositoryBase<T> where T : class
{
    private readonly ILogger<RepositoryBase<T>> _logger;

    private readonly string _caminhoArquivo;
    private readonly JsonSerializerOptions _jsonOptions;

    protected List<T> Dados { get; private set; }

    protected RepositoryBase(string caminhoArquivo, ILogger<RepositoryBase<T>> logger)
    {
        _caminhoArquivo = caminhoArquivo;
        _logger = logger;

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        Directory.CreateDirectory(Path.GetDirectoryName(_caminhoArquivo)!);
        CarregarDados();
    }

    #region Streaming

    /// <summary>
    /// Lê um objeto do JSON diretamente via streaming, sem carregar tudo em memória.
    /// Ideal para buscas por Id em arquivos grandes.
    /// </summary>
    protected async Task<T?> ObterObjetoStreamingAsync(Func<JsonElement, bool> predicate)
    {
        try
        {
            await using var fs = File.OpenRead(_caminhoArquivo);
            using var reader = new StreamReader(fs);

            var buffer = await File.ReadAllBytesAsync(_caminhoArquivo);
            var jsonReader = new Utf8JsonReader(buffer);

            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonTokenType.StartObject)
                {
                    using var doc = JsonDocument.ParseValue(ref jsonReader);
                    var element = doc.RootElement;

                    if (predicate(element))
                    {
                        return JsonSerializer.Deserialize<T>(element.GetRawText(), _jsonOptions);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[RepositoryBase] ERRO ao ler streaming de {_caminhoArquivo}");
        }

        return null;
    }

    #endregion

    private void CarregarDados()
    {
        try
        {
            if (File.Exists(_caminhoArquivo))
            {
                var json = File.ReadAllText(_caminhoArquivo);
                Dados = JsonSerializer.Deserialize<List<T>>(json, _jsonOptions) ?? new List<T>();
                _logger.LogWarning($"Dados carregados: {Dados.Count} itens");
            }
            else
            {
                Dados = new List<T>();
                _logger.LogWarning($"Criando novo arquivo em: {_caminhoArquivo}");
                SalvarDados();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERRO ao carregar dados: {ex.Message}");
            Dados = new List<T>();
        }
    }

    protected void SalvarDados()
    {
        try
        {
            var json = JsonSerializer.Serialize(Dados, _jsonOptions);
            File.WriteAllText(_caminhoArquivo, json);
            _logger.LogWarning($"Dados salvos: {Dados.Count} itens");
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERRO ao salvar: {ex.Message}");
        }
    }
}
