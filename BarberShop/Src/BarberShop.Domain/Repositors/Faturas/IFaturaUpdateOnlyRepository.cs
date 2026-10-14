using BarberShop.Domain.Entidades;

namespace BarberShop.Domain.Repositors.Faturas;

public interface IFaturaUpdateOnlyRepository
{
    Task<Fatura?> GetById(long id);
    void Update(Fatura fatura);
}
