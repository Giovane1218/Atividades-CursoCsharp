using AutoMapper;
using CashFlow.communication.Responses;
using CashFlow.Domain.Repositors.Expenses;

namespace CashFlow.application.UseCases.Expenses.GetAll;

public class GetAllExpenseUseCase : IGetAllExpenseUseCase
{
    private readonly IExpenseReadOnlyRepository _expenseRepository;
    private readonly IMapper _mapper;
    public GetAllExpenseUseCase(IExpenseReadOnlyRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }
    public async Task<ResponseAllExpensesJson> Execute()
    {
        var result = await _expenseRepository.GetAll();

        return new ResponseAllExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    }
}
