namespace BarberShop.Application.UseCases.Faturas.Delete;

public interface IDeleteFaturaUseCase
{
    Task Execute(long id);
}
