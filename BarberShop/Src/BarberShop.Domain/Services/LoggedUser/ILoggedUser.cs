using BarberShop.Domain.Entidades;

namespace BarberShop.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<Usuario> Get();
}
