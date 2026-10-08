using CashFlow.communication.Requests;
using CashFlow.communication.Responses;
using CashFlow.Domain.Repositors.Expenses;
using CashFlow.Domain.Entidades;
using CashFlow.Exeption.ExeptionBase;
using CashFlow.Domain.Repositors;
using AutoMapper;

namespace CashFlow.application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    
    private readonly IExpenseWriteOnlyRepository _expenseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterExpenseUseCase(
        IExpenseWriteOnlyRepository expeneseRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _expenseRepository = expeneseRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }

    public async Task<ResponseRegisterExpenseJson> Execute(RequestExpensesJson request)
    {
        Validate(request);

        var entity = _mapper.Map<Expense>(request);

        await _expenseRepository.Add(entity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterExpenseJson>(entity);
    }

    private void Validate(RequestExpensesJson request)
    {
        var validator = new ExpenseValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationExeption(errorMessages);
        }
    }
}
