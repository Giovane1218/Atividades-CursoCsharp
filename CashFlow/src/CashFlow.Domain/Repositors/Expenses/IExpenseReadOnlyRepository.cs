using CashFlow.Domain.Entidades;

namespace CashFlow.Domain.Repositors.Expenses;

public interface IExpenseReadOnlyRepository
{
    Task<List<Expense>> GetAll();
    Task<Expense?> GetById(long id);

    Task<List<Expense>> FilterByMonth(DateOnly month);
}
