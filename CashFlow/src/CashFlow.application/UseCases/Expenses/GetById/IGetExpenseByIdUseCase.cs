using CashFlow.communication.Responses;

namespace CashFlow.application.UseCases.Expenses.GetById;

public interface IGetExpenseByIdUseCase
{
    Task<ResponseExpenseJson> Execute(long id);
}
