using BarberShop.Domain.Entidades;

namespace BarberShop.Domain.Security.Tokens;

public interface IAccessTokenGenarator
{
    string Generate(Usuario user);
}
