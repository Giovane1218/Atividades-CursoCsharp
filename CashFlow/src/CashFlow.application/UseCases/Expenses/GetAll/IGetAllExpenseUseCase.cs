using CashFlow.communication.Responses;

namespace CashFlow.application.UseCases.Expenses.GetAll;

public interface IGetAllExpenseUseCase
{
    public Task<ResponseAllExpensesJson> Execute();
}
