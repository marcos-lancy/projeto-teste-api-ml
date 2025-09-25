﻿using System.Runtime.Serialization;

namespace TesteMeli.Sahred.Exceptions;

[Serializable]
public class ConflictException : CustomExceptionBase
{
    public ConflictException() : base("Já há dados cadastrados para esta requisição.") { }

    public ConflictException(string message) : base(message) { }

    public ConflictException(string message, Exception inner) : base(message, inner) { }

    protected ConflictException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}