using BarberShop.Domain.Entidades;

namespace BarberShop.Domain.Repositors.User;

public interface IUserWriteOnlyRepository
{
    Task Add(Usuario user);


    /// <summary>
    /// This function returns true if the fatura was deleted successfully, and false if the fatura was not found.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Delete(Usuario user);
}
