using BarberShop.Domain.Services.LoggedUser;

namespace BarberShop.Application.UseCases.Faturas.Delete;

public class DeleteFaturaUseCase : IDeleteFaturaUseCase
{
    private readonly IFaturaWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteFaturaUseCase(IFaturaWriteOnlyRepository repository, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var result = await _repository.Delete(id, loggedUser.Id);
        if (result is false)
        {
            throw new NotFoundExeption(ResourceErrorMsg.NOT_FOUND_ERROR);
        }

        await _unitOfWork.Commit();
    }
}
