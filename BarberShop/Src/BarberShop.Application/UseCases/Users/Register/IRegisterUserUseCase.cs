using BarberShop.Communication.Request.User;

namespace BarberShop.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestUserJson request);
}
