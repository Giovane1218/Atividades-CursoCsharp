namespace Exercicio1;

static class Exercicio2
{
    static void Main()
    {
        Console.WriteLine("Qual o seu nome?");
        string nome = Console.ReadLine();
        Console.WriteLine("Qual o seu sobrenome?");
        string sobrenome = Console.ReadLine();

        string nCompleto = nome + " " + sobrenome;
        Console.WriteLine($"Nome completo: {nCompleto}");
   }

}
