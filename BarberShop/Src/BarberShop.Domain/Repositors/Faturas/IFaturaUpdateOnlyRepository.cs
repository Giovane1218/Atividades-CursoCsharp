using BarberShop.Domain.Entidades;

namespace BarberShop.Domain.Repositors.Faturas;

public interface IFaturaUpdateOnlyRepository
{
    Task<Fatura?> GetById(long id, long userId);
    void Update(Fatura fatura);
}
