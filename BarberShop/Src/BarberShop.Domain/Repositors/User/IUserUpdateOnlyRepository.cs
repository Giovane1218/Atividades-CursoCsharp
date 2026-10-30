using BarberShop.Domain.Entidades;

namespace BarberShop.Domain.Repositors.User;

public interface IUserUpdateOnlyRepository
{
    Task<Usuario?> GetUserById(long Id);

    void Update(Usuario user);
}
