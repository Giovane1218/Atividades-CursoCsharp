internal class exerciocio4
{
    static void Main()
    {
        Console.WriteLine("Escreva algo:");
        string frase = Console.ReadLine();
        Console.WriteLine($"você digitou: {frase.Replace(" ","").Length} caracteres");
    }
}