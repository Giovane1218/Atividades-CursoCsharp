using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace BarberShop.Application.UseCases.Users.Validators;

public class SenhaValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MSG_KEY = "ErrorMessage";

    public override string Name => "SenhaValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MSG_KEY}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
        {
            context.MessageFormatter.AppendArgument(ERROR_MSG_KEY, ResourceErrorMsg.EMPTY_PASSW_ERROR);
            return false;
        }
        if (senha.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ERROR_MSG_KEY, ResourceErrorMsg.PASSW_LENGH_ERROR);
            return false;
        }

        if (Regex.IsMatch(senha, @"[A-Z]+") == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MSG_KEY, ResourceErrorMsg.PASSW_UPCASE_ERROR);
            return false;
        }

        if (Regex.IsMatch(senha, @"[a-z]+") == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MSG_KEY, ResourceErrorMsg.PASSW_LOWCASE_ERROR);
            return false;
        }

        if (Regex.IsMatch(senha, @"[0-9]+") == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MSG_KEY, ResourceErrorMsg.PASSW_NUM_ERROR);
            return false;
        }

        if (Regex.IsMatch(senha, @"[\!\?\*\#\.]+") == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MSG_KEY, ResourceErrorMsg.PASSW_CHAR_ERROR);
            return false;
        }

        return true;
    }
}
