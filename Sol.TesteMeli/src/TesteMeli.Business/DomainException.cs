namespace TesteMeli.Business;

/// <summary>
/// Representa erros que ocorrem dentro da camada de domínio de uma aplicação.
/// </summary>
/// <remarks>
/// Esta exceção deve ser utilizada para erros específicos do domínio que não se enquadram em categorias de exceções mais gerais.  
/// Ela fornece construtores para especificar uma mensagem de erro e uma exceção interna opcional, permitindo capturar a causa subjacente.
/// </remarks>
public class DomainException : Exception
{
    public DomainException()
    { }

    public DomainException(string message) : base(message)
    { }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    { }
}