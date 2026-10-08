using CashFlow.communication.Responses;
using CashFlow.Exeption;
using CashFlow.Exeption.ExeptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace CashFlow.API.Filtros;

public class ExeptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is CashFlowExeption)
        {
            HandleprojectException(context);
        }
        else 
        {
            UnknownError(context);
        }
    }

    private void HandleprojectException(ExceptionContext context)
    {
        var cashFlowExeption = (CashFlowExeption)context.Exception;
        var errorResponse = new ResponseErrorJson(cashFlowExeption.GetErrors());

        context.HttpContext.Response.StatusCode = cashFlowExeption.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void UnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMsg.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
