namespace TesteMeli.Business.Interfaces;

public interface IJwtService
{
    string GerarToken(string email, string role);
}
