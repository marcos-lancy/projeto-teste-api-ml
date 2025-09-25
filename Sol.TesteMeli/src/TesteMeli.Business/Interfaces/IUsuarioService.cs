namespace TesteMeli.Business.Interfaces;

public interface IUsuarioService
{
    Task<string> EfetuarLoginAsync(string email, string senha);
}
