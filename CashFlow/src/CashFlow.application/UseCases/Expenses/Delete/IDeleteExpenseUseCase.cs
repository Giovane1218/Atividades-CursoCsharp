namespace CashFlow.application.UseCases.Expenses.Delete;

public interface IDeleteExpenseUseCase
{
    Task Execute(long id);
}
