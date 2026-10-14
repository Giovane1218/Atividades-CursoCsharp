using BarberShop.Application.UseCases.Faturas.Delete;
using BarberShop.Application.UseCases.Faturas.GetAll;
using BarberShop.Application.UseCases.Faturas.GetById;
using BarberShop.Application.UseCases.Faturas.Register;
using BarberShop.Application.UseCases.Faturas.Update;
using BarberShop.Communication.Requests;
using BarberShop.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FaturasController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterFaturaJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterFaturaUseCase useCase,
        [FromBody] RequestFaturasJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllFaturasJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllFaturasUseCase useCase)
    {
        var response = await useCase.Execute();
        return response.Faturas.Count > 0 ? Ok(response) : NoContent();
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ResponseFaturaJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetFaturaById useCase, [FromRoute] long id)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteFaturaUseCase useCase, [FromRoute] long id)
    {
        await useCase.Execute(id);
        return NoContent();
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateFaturaUseCase useCase,
        [FromRoute] long id,
        [FromBody] RequestFaturasJson request)
    {
        await useCase.Execute(id, request);
        return NoContent();
    }
}
