namespace BarberShop.Application.UseCases.Faturas.GetAll;

public interface IGetAllFaturasUseCase
{
    Task<ResponseAllFaturasJson> Execute();
}
