using System.ComponentModel.DataAnnotations;

namespace DesafioPratico_2.Model;

public class LivroModel
{
    public Guid Id { get; set; }

    [StringLength(120, MinimumLength = 1)]
    [Required(ErrorMessage = "O nome do livro é obrigatório.")]
    public string Name { get; set; }

    [StringLength(120, MinimumLength = 0)]
    public string Author { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório.")]
    public string Genres { get; set; }

    [Range(0.01, float.MaxValue, ErrorMessage = "O preço do livro deve ser maior que zero.")]
    public float Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "O estoque do livro não pode ser negativo.")]
    public int stock { get; set; }
}
