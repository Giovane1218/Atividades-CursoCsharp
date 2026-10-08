using AutoMapper;
using CashFlow.communication.Requests;
using CashFlow.communication.Responses;
using CashFlow.Domain.Entidades;

namespace CashFlow.application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    public void RequestToEntity()
    {
        CreateMap<RequestExpensesJson, Expense>();
    }

    public void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisterExpenseJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
        CreateMap<Expense, ResponseExpenseJson>();
    }
}
