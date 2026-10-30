using BarberShop.Application.UseCases.Users.ChangePassW;
using BarberShop.Application.UseCases.Users.Delete;
using BarberShop.Application.UseCases.Users.GetById;
using BarberShop.Application.UseCases.Users.Register;
using BarberShop.Application.UseCases.Users.Update;
using BarberShop.Communication.Request.User;
using BarberShop.Communication.Response.Users;
using BarberShop.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase usecase,
        [FromBody] RequestUserJson request)
    {
        var response = await usecase.Execute(request);

        return Created(string.Empty, response);
    }

   
    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetById([FromServices] IGetUserUseCase usecase)
    {
        var response = await usecase.Execute();

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateUserUseCase usecase,
        [FromBody] RequestUpdateUserJson request)
    {
        await usecase.Execute(request);

        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteUserUseCase useCase)
    {
        await useCase.Execute();
        return NoContent();
    }

    [HttpPut("password")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePassword(
        [FromServices] IChangePassWordUseCase useCase,
        [FromBody] RequestChangePassWordJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }
}
