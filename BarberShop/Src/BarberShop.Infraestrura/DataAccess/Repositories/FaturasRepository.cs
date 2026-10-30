using BarberShop.Domain.Entidades;
using BarberShop.Domain.Repositors.Faturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BarberShop.Infraestrutura.DataAccess.Repositories;

internal class FaturasRepository : IFaturaReadOnlyRepository, IFaturaWriteOnlyRepository, IFaturaUpdateOnlyRepository
{
    private readonly BarberShopDBContext _dbContext;

    public FaturasRepository(BarberShopDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Fatura fatura)
    {
        await _dbContext.Faturamentos.AddAsync(fatura);
    }

    public async Task<bool> Delete(long id, long userId)
    {
        var result = await _dbContext.Faturamentos
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        if (result is null)
        {
            return false;
        }

        _dbContext.Faturamentos.Remove(result);

        return true;
    }
    public async Task<List<Fatura>> GetAll(long userId)
    {
        return await _dbContext.Faturamentos
            .AsNoTracking()
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    async Task<Fatura?> IFaturaReadOnlyRepository.GetById(long id, long userId)
    {
        return await GetFullExpense()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
    }
    async Task<Fatura?> IFaturaUpdateOnlyRepository.GetById(long id, long userId)
    {
        return await GetFullExpense()
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
    }

    public void Update(Fatura fatura)
    {
        _dbContext.Faturamentos.Update(fatura);
    }


    private IIncludableQueryable<Fatura, ICollection<Tag>> GetFullExpense()
    {
        return _dbContext.Faturamentos
            .Include(fatura => fatura.Tags);
    }

    public async Task<List<Fatura>> FilterByMonth(DateOnly month)
    {
        var startDate = new DateTime(year: month.Year, month: month.Month, day: 1).Date;
        var daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
        var endDate = new DateTime(year: month.Year, month: month.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext
            .Faturamentos
            .AsNoTracking()
            .Where(e => e.Data >= startDate && e.Data <= endDate)
            .OrderBy(fatura => fatura.Data)
            .ToListAsync();
    }
}
