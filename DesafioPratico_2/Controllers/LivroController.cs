using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DesafioPratico_2.Model;
using DesafioPratico_2.Services;

namespace DesafioPratico_2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LivroController : ControllerBase
{
    private static List<LivroModel> livros = new List<LivroModel>
    {
        new LivroModel
        {
            Id = Guid.NewGuid(),
            Name = "O livro",
            Author = "Giovane",
            Genres = "Mistério",
            Price = 39,
            stock = 5
        },
        new LivroModel
        {
            Id = Guid.NewGuid(),
            Name = "As aventuras de Gi",
            Author = "pedrovaldo",
            Genres = "Aventura",
            Price = 49,
            stock = 14
        }
    };

    private static readonly ValidarLivro _validationService = 
        new ValidarLivro(livros);

    [HttpPost]
    [ProducesResponseType(typeof(LivroModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public IActionResult CreateLivro([FromBody] LivroModel livro)
    { 
        if (livro == null)
            return BadRequest("Livro não pode ser nulo.");

        var (isValid, errorMessage) = _validationService.ValidarCriacao(livro);
        if (!isValid)
            return BadRequest(errorMessage);

        if (livro.Id == Guid.Empty)
            livro.Id = Guid.NewGuid();

        livros.Add(livro);
        return CreatedAtAction(nameof(GetLivroById), new { id = livro.Id }, livro); 
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LivroModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult GetLivroById([FromRoute] Guid id)
    {
        var livro = livros.FirstOrDefault(l => l.Id == id);
        if (livro == null)
            return NotFound("Livro não encontrado.");
        
        return Ok(livro);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<LivroModel>), StatusCodes.Status200OK)]
    public IActionResult GetAllLivros()
    {
        return Ok(livros);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(LivroModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult UpdateLivro([FromRoute] Guid id, [FromBody] LivroModel livroAtualizado)
    {
        if (livroAtualizado == null)
            return BadRequest("Livro não pode ser nulo.");

        var (isValid, errorMessage) = _validationService.ValidarAtualizacao(id, livroAtualizado);
        if (!isValid)
            return BadRequest(errorMessage);

        var livro = livros.FirstOrDefault(l => l.Id == id);

        livro.Name = livroAtualizado.Name;
        livro.Author = livroAtualizado.Author;
        livro.Genres = livroAtualizado.Genres;
        livro.Price = livroAtualizado.Price;
        livro.stock = livroAtualizado.stock;

        return Ok(livro);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult DeleteLivro([FromRoute] Guid id)
    {
        var livro = livros.FirstOrDefault(l => l.Id == id);
        if (livro == null)
            return NotFound("Livro não encontrado.");

        livros.Remove(livro);
        return NoContent();
    }
}
