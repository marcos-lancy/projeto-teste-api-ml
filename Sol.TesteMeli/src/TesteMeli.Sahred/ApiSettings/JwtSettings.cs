namespace TesteMeli.Sahred.ApiSettings;

/// <summary>
/// Classe de configuração para JWT.
/// Valores provenientes do appsettings.json.
/// </summary>
public class JwtSettings
{
    public string SecretKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpirationMinutes { get; set; }
}