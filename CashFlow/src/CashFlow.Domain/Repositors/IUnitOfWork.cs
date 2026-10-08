namespace CashFlow.Domain.Repositors;
public interface IUnitOfWork
{
    Task Commit();
}
