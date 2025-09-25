using System.Runtime.Serialization;

namespace TesteMeli.Sahred.Exceptions;

/// <summary>
/// Exceção para falha de validação de negócio (400)
/// </summary>
[Serializable]
public class CustomExceptionBase : Exception
{
    public CustomExceptionBase() { }

    public CustomExceptionBase(string message) : base(message) { }

    public CustomExceptionBase(string message, Exception innerException) : base(message, innerException) { }

    protected CustomExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context) { }
}