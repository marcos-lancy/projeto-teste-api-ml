﻿namespace TesteMeli.Shared.Exceptions;

public class ErrorResponse(
int statusCode,
string message,
IDictionary<string, string[]> details)
{
    public int StatusCode { get; init; } = statusCode;
    public string Message { get; init; } = message;
    public IDictionary<string, string[]>? Details { get; init; } = details;
}
