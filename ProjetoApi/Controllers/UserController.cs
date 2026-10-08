using Microsoft.AspNetCore.Mvc;
using ProjetoApi.Communications.requests;

namespace ProjetoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetById([FromRoute] int id)
    {
        var response = new User
        {
            Id = id,
            Name = "giovane",
            Age = 24
        };
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] RegisterUser request)
    {
        var user = new User
        {
            Id = request.UserId != 0 ? request.UserId : 1,
            Name = request.Name,
            Age = 0
        };

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromRoute] int id, [FromBody] RegisterUser request)
    {
        // Simulate update of the user with id
        return NoContent();
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        return NoContent();
    }
    [HttpGet]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Age = 30 },
            new User { Id = 2, Name = "Jane Smith", Age = 25 },
            new User { Id = 3, Name = "Alice Johnson", Age = 28 }
        };
        return Ok(users);
    }


}
