using CashFlow.Domain.Repositors;

namespace CashFlow.Infraestrutura.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly CashFlowDBContext _dbContext;
    public UnitOfWork(CashFlowDBContext dBContext)
    {
        _dbContext = dBContext;
    }
    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
