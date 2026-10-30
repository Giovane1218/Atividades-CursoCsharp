using BarberShop.Domain.Entidades;

namespace BarberShop.Domain.Repositors.Faturas;

public interface IFaturaReadOnlyRepository
{
    Task<List<Fatura>> GetAll(long Id);
    Task<Fatura?> GetById(long Id, long userid);

    Task<List<Fatura>> FilterByMonth(DateOnly month);
}
