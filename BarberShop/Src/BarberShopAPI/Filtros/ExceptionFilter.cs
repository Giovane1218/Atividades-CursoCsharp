using BarberShop.Communication.Responses;
using BarberShop.Exception;
using BarberShop.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace BarberShop.API.Filtros;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BarberShopException)
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
        var exception = (BarberShopException)context.Exception;
        var errorResponse = new ResponseErrorJson(exception.GetErrors());

        context.HttpContext.Response.StatusCode = exception.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void UnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMsg.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
