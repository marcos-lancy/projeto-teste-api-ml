using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using System.Net;
using TesteMeli.Shared.Exceptions;

namespace TesteMeli.Api.ApiConfigurations;

public class CustomValidatorResult : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        return new BadRequestObjectResult(
            new ErrorResponse(
                (int)HttpStatusCode.BadRequest,
                "Erros de validação",
                validationProblemDetails?.Errors ?? new Dictionary<string, string[]>()));
    }
}