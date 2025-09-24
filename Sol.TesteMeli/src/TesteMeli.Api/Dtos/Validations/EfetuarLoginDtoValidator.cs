using FluentValidation;
using TesteMeli.Api.Dtos.Auth;

namespace TesteMeli.Api.Dtos.Validations;

public class EfetuarLoginDtoValidator : AbstractValidator<LoginRequest>
{
    public EfetuarLoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório")
            .EmailAddress().WithMessage("O e-mail informado não é válido");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória");
    }
}