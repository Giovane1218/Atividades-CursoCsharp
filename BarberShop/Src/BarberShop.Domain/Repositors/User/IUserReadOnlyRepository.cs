using BarberShop.Domain.Entidades;

namespace BarberShop.Domain.Repositors.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ExisteUserComEmail(string email);
    Task<Usuario?> GetUserById(long Id);

    Task<Usuario?> GetUserLogin(string email);
}
