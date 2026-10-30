using BarberShop.Communication.Request.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarberShop.Application.UseCases.Users.ChangePassW;

public interface IChangePassWordUseCase
{
    Task Execute(RequestChangePassWordJson request);
}
