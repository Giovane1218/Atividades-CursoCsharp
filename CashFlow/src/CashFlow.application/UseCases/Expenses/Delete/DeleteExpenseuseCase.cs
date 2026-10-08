using CashFlow.Domain.Repositors;
using CashFlow.Domain.Repositors.Expenses;
using CashFlow.Exeption;
using CashFlow.Exeption.ExeptionBase;

namespace CashFlow.application.UseCases.Expenses.Delete;

public class DeleteExpenseuseCase : IDeleteExpenseUseCase
{
    private readonly IExpenseWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpenseuseCase(IExpenseWriteOnlyRepository repository, IUnitOfWork unitOfWork)
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
