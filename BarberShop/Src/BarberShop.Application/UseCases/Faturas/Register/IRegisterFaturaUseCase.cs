namespace BarberShop.Application.UseCases.Faturas.Register;

public interface IRegisterFaturaUseCase
{
    Task<ResponseRegisterFaturaJson> Execute(RequestFaturasJson request);
}
