using BarberShop.Communication.Request.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarberShop.Application.UseCases.Users.Login;

public interface IDoLoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
