using BarberShop.Domain.Entidades;

namespace BarberShop.Domain.Repositors.Faturas;

public interface IFaturaWriteOnlyRepository
{
    Task Add(Fatura fatura);
    /// <summary>
    /// This function returns true if the fatura was deleted successfully, and false if the fatura was not found.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    Task<bool> Delete(long id);
}
