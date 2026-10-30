using BarberShop.Communication.Request.User;

namespace BarberShop.Application.UseCases.Users.Update;

public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}
