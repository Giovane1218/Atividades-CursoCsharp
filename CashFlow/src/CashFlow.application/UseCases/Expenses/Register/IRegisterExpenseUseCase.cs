using CashFlow.communication.Requests;
using CashFlow.communication.Responses;

namespace CashFlow.application.UseCases.Expenses.Register;

public interface IRegisterExpenseUseCase
{
    Task<ResponseRegisterExpenseJson> Execute(RequestExpensesJson request);
}
