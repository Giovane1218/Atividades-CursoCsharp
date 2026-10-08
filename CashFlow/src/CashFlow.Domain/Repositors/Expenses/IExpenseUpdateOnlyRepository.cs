using CashFlow.Domain.Entidades;

namespace CashFlow.Domain.Repositors.Expenses;

public interface IExpenseUpdateOnlyRepository
{
    Task<Expense?> GetById(long id);
    void Update(Expense expense);
}
