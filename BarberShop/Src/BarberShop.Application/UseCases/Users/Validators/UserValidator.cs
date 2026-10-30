using BarberShop.Communication.Request.User;

namespace BarberShop.Application.UseCases.Users.Validators;

public class UserValidator : AbstractValidator<RequestUserJson>
{
    public UserValidator()
    {
        RuleFor(user => user.Nome).NotEmpty().WithMessage(ResourceErrorMsg.EMPTY_NAME_ERROR);
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMsg.EMPTY_EMAIL_ERROR)
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMsg.EMAIL_INVALID_ERROR);

        RuleFor(user => user.Senha).SetValidator(new SenhaValidator<RequestUserJson>());
    }
}
