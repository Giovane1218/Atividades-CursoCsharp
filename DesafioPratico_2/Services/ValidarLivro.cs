using DesafioPratico_2.Model;

namespace DesafioPratico_2.Services;

public class ValidarLivro
{
    private readonly List<string> _genresValidos = new List<string>
    {
        "Ficção", "Romance", "Aventura", "Fantasia", "Mistério",
        "Terror", "Biografia", "História", "Ciência", "Autoajuda"
    };

    private readonly List<LivroModel> _livrosExistentes;

    public ValidarLivro(List<LivroModel> livrosExistentes)
    {
        _livrosExistentes = livrosExistentes;
    }

    public (bool isValid, string errorMessage) ValidarCriacao(LivroModel livro)
    {
        var validacaoNome = ValidarNome(livro.Name);
        if (!validacaoNome.isValid)
            return validacaoNome;

        var validacaoGenero = ValidarGenero(livro.Genres);
        if (!validacaoGenero.isValid)
            return validacaoGenero;

        var validacaoPreco = ValidarPreco(livro.Price);
        if (!validacaoPreco.isValid)
            return validacaoPreco;

        var validacaoEstoque = ValidarEstoque(livro.stock);
        if (!validacaoEstoque.isValid)
            return validacaoEstoque;

        var validacaoDuplicata = ValidarDuplicata(livro.Name);
        if (!validacaoDuplicata.isValid)
            return validacaoDuplicata;

        return (true, string.Empty);
    }

    public (bool isValid, string errorMessage) ValidarAtualizacao(Guid id, LivroModel livro)
    {
        var livroExistente = _livrosExistentes.FirstOrDefault(l => l.Id == id);
        if (livroExistente == null)
            return (false, "Livro não encontrado.");

        var validacaoNome = ValidarNome(livro.Name);
        if (!validacaoNome.isValid)
            return validacaoNome;

        // Verifica duplicata apenas se o nome foi alterado
        if (livroExistente.Name != livro.Name)
        {
            var validacaoDuplicata = ValidarDuplicata(livro.Name);
            if (!validacaoDuplicata.isValid)
                return validacaoDuplicata;
        }

        var validacaoGenero = ValidarGenero(livro.Genres);
        if (!validacaoGenero.isValid)
            return validacaoGenero;

        var validacaoPreco = ValidarPreco(livro.Price);
        if (!validacaoPreco.isValid)
            return validacaoPreco;

        var validacaoEstoque = ValidarEstoque(livro.stock);
        if (!validacaoEstoque.isValid)
            return validacaoEstoque;

        return (true, string.Empty);
    }

    private (bool isValid, string errorMessage) ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return (false, "O nome do livro é obrigatório.");
        return (true, string.Empty);
    }

    private (bool isValid, string errorMessage) ValidarGenero(string genero)
    {
        if (string.IsNullOrWhiteSpace(genero))
            return (false, "O gênero é obrigatório.");
        if (!_genresValidos.Contains(genero))
            return (false, $"Gênero inválido. Válidos: {string.Join(", ", _genresValidos)}");
        return (true, string.Empty);
    }

    private (bool isValid, string errorMessage) ValidarPreco(float preco)
    {
        if (preco <= 0)
            return (false, "O preço do livro deve ser maior que zero.");
        return (true, string.Empty);
    }

    private (bool isValid, string errorMessage) ValidarEstoque(int estoque)
    {
        if (estoque < 0)
            return (false, "O estoque do livro não pode ser negativo.");
        return (true, string.Empty);
    }

    private (bool isValid, string errorMessage) ValidarDuplicata(string nome)
    {
        if (_livrosExistentes.Any(l => l.Name == nome))
            return (false, "Já existe um livro com este nome.");
        return (true, string.Empty);
    }

    public List<string> GetGenerosValidos() => _genresValidos;
}