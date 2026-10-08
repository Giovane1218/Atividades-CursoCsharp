using CashFlow.Domain.Entidades;
using CashFlow.Domain.Repositors.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestrutura.DataAccess.Repositories;

internal class ExpensesRepository : IExpenseReadOnlyRepository, IExpenseWriteOnlyRepository, IExpenseUpdateOnlyRepository
{
    private readonly CashFlowDBContext _dbContext;

    public ExpensesRepository(CashFlowDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (result is null)
        {
            return false;
        }

        _dbContext.Expenses.Remove(result);

        return true;
    }
    public async Task<List<Expense>> GetAll()
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();
    }

    async Task<Expense?> IExpenseReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }
    async Task<Expense?> IExpenseUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

    public async Task<List<Expense>> FilterByMonth(DateOnly month)
    {
        var startDate = new DateTime(year: month.Year, month: month.Month, day: 1).Date;
        var daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
        var endDate = new DateTime(year: month.Year, month: month.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext
            .Expenses
            .AsNoTracking()
            .Where(e => e.Data >= startDate && e.Data <= endDate)
            .OrderBy(expense => expense.Data)
            .ToListAsync();
    }
}
