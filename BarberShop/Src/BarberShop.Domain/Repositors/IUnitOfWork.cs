namespace BarberShop.Domain.Repositors;
public interface IUnitOfWork
{
    Task Commit();
}
