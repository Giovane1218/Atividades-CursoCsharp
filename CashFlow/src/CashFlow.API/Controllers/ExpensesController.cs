using CashFlow.application.UseCases.Expenses.GetAll;
using CashFlow.application.UseCases.Expenses.Register;
using CashFlow.application.UseCases.Expenses.GetById;
using CashFlow.communication.Requests;
using CashFlow.communication.Responses;
using Microsoft.AspNetCore.Mvc;
using CashFlow.application.UseCases.Expenses.Delete;
using CashFlow.application.UseCases.Expenses.Update;

namespace CashFlow.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterExpenseUseCase registerExpenseUseCase,
        [FromBody] RequestExpensesJson request)
    {
      
        var response = await registerExpenseUseCase.Execute(request);

        return Created(string.Empty, response);
    }


    [HttpGet]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpenseUseCase useCase)
    {
        var response = await useCase.Execute();

        if  (response.Expenses.Count != 0)
        {
            return Ok(response);
        }
        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetExpenseByIdUseCase useCase, [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteExpenseUseCase useCase, [FromRoute] long id)
    {
        await useCase.Execute(id);

        return NoContent();
    }


    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateExpenseUseCase useCase,
        [FromRoute] long id,
        [FromBody] RequestExpensesJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

}   