using BarberShop.Communication.Response.Users;
using BarberShop.Domain.Repositors.User;
using BarberShop.Domain.Services.LoggedUser;

namespace BarberShop.Application.UseCases.Users.GetById;

public interface IGetUserUseCase
{    
    Task<ResponseUserJson> Execute();
}
