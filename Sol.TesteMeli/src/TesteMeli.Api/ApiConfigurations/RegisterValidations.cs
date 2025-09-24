using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using TesteMeli.Api.Dtos.Validations;

namespace TesteMeli.Api.ApiConfigurations;

public static class RegisterValidations
{
    public static IServiceCollection AddAbstractValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(EfetuarLoginDtoValidator).Assembly);

        services.AddFluentValidationAutoValidation(options =>
        {
            options.OverrideDefaultResultFactoryWith<CustomValidatorResult>();
        });

        return services;
    }
}
