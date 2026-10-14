using BarberShop.Domain.Entidades;
using BarberShop.Domain.Repositors.Faturas;
using Microsoft.EntityFrameworkCore;

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
        await _dbContext.faturamentos.AddAsync(fatura);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.faturamentos.FirstOrDefaultAsync(e => e.Id == id);
        if (result is null)
        {
            return false;
        }

        _dbContext.faturamentos.Remove(result);

        return true;
    }
    public async Task<List<Fatura>> GetAll()
    {
        return await _dbContext.faturamentos.AsNoTracking().ToListAsync();
    }

    async Task<Fatura?> IFaturaReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.faturamentos.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }
    async Task<Fatura?> IFaturaUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.faturamentos.FirstOrDefaultAsync(e => e.Id == id);
    }

    public void Update(Fatura fatura)
    {
        _dbContext.faturamentos.Update(fatura);
    }

    public async Task<List<Fatura>> FilterByMonth(DateOnly month)
    {
        var startDate = new DateTime(year: month.Year, month: month.Month, day: 1).Date;
        var daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
        var endDate = new DateTime(year: month.Year, month: month.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext
            .faturamentos
            .AsNoTracking()
            .Where(e => e.Data >= startDate && e.Data <= endDate)
            .OrderBy(fatura => fatura.Data)
            .ToListAsync();
    }
}
