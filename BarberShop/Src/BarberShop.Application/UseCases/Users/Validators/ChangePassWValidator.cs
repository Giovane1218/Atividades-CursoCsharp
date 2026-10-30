using BarberShop.Communication.Request.User;

namespace BarberShop.Application.UseCases.Users.Validators;

public class ChangePassWValidator : AbstractValidator<RequestChangePassWordJson>
{
    public ChangePassWValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new SenhaValidator<RequestChangePassWordJson>());
    }
}
