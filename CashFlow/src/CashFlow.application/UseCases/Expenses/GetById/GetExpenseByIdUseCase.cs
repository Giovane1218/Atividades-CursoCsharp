using AutoMapper;
using CashFlow.communication.Responses;
using CashFlow.Domain.Repositors.Expenses;
using CashFlow.Exeption;
using CashFlow.Exeption.ExeptionBase;

namespace CashFlow.application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{
    private readonly IExpenseReadOnlyRepository _expenseRepository;
    private readonly IMapper _mapper;
    public GetExpenseByIdUseCase(IExpenseReadOnlyRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await _expenseRepository.GetById(id);

        if (result is null)
        {
            throw new NotFoundExeption(ResourceErrorMsg.NOT_FOUND_ERROR);
        }
        return _mapper.Map<ResponseExpenseJson>(result);
    }
}
