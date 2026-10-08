using CashFlow.Domain.Entidades;

namespace CashFlow.Domain.Repositors.Expenses;

public interface IExpenseWriteOnlyRepository
{
    Task Add(Expense expense);
    /// <summary>
    /// This function returns true if the expense was deleted successfully, and false if the expense was not found.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    Task<bool> Delete(long id);
}
