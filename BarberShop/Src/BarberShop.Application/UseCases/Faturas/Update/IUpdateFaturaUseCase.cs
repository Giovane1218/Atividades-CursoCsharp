using BarberShop.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarberShop.Application.UseCases.Faturas.Update;

public interface IUpdateFaturaUseCase
{
    Task Execute(long id, RequestFaturasJson request);
}
