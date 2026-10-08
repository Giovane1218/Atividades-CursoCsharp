using AutoMapper;
using CashFlow.communication.Requests;
using CashFlow.Domain.Entidades;
using CashFlow.Domain.Repositors;
using CashFlow.Domain.Repositors.Expenses;
using CashFlow.Exeption;
using CashFlow.Exeption.ExeptionBase;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IExpenseUpdateOnlyRepository _repository;

    public UpdateExpenseUseCase(IUnitOfWork unitOfWork, IMapper mapper, IExpenseUpdateOnlyRepository expenseUpdateOnlyRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = expenseUpdateOnlyRepository;
    }

    public async Task Execute(long id, RequestExpensesJson request)
    {
        Validate(request);
        var expense = await _repository.GetById(id);

        if (expense is null)
        {
            throw new NotFoundExeption(ResourceErrorMsg.NOT_FOUND_ERROR);
        }

        _mapper.Map(request, expense);

        _repository.Update(expense);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestExpensesJson request)
    {
        var validator = new ExpenseValidator();
        var result = validator.Validate(request);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationExeption(errorMessages);
        }
    }


}
