using BarberShop.Domain.Repositors;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infraestrutura.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly BarberShopDBContext _dbContext;
    public UnitOfWork(BarberShopDBContext dBContext)
    {
        _dbContext = dBContext;
    }
    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
