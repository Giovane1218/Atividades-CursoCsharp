using CashFlow.communication.Requests;
using CashFlow.Exeption;
using FluentValidation;

namespace CashFlow.application.UseCases.Expenses;

public class ExpenseValidator : AbstractValidator<RequestExpensesJson>
{
    public ExpenseValidator()
    {
        RuleFor(expense => expense.Titulo).NotEmpty().WithMessage(ResourceErrorMsg.EMPTY_TITLE_ERROR);
        RuleFor(expense => expense.Valor).GreaterThan(0).WithMessage(ResourceErrorMsg.VALOR_ZERO_ERROR);
        RuleFor(expense => expense.Data).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMsg.FUTURE_DATE_ERROR);
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage(ResourceErrorMsg.PAY_TYPE_ERROR);
    }
}
