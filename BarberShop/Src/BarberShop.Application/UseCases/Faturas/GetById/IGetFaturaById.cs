namespace BarberShop.Application.UseCases.Faturas.GetById;

public interface IGetFaturaById
{
    Task<ResponseFaturaJson> Execute(long id);
}
