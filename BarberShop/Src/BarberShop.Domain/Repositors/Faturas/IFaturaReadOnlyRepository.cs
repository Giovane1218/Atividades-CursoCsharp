using BarberShop.Domain.Entidades;

namespace BarberShop.Domain.Repositors.Faturas;

public interface IFaturaReadOnlyRepository
{
    Task<List<Fatura>> GetAll();
    Task<Fatura?> GetById(long id);

    Task<List<Fatura>> FilterByMonth(DateOnly month);
}
