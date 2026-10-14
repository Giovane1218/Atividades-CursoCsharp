namespace BarberShop.Application.UseCases.Faturas.Delete;

public class DeleteFaturaUseCase : IDeleteFaturaUseCase
{
    private readonly IFaturaWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFaturaUseCase(IFaturaWriteOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var result = await _repository.Delete(id);

        if (result is false)
        {
            throw new NotFoundExeption(ResourceErrorMsg.NOT_FOUND_ERROR);
        }

        await _unitOfWork.Commit();
    }
}
