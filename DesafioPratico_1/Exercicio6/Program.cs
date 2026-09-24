using System;

internal class Exercicio1
{
    static void Main()
    {
        DateTime dataAtual = DateTime.Now;

        Console.WriteLine("Data atual em diferentes formatos:");

        Console.WriteLine(dataAtual.ToString("dddd, dd 'de' MMMM 'de' yyyy HH:mm:ss"));

        Console.WriteLine(dataAtual.ToString("dd/MM/yyyy"));

        Console.WriteLine(dataAtual.ToString("HH:mm:ss"));

        Console.WriteLine(dataAtual.ToString("dd 'de' MMMM 'de' yyyy"));
    }
}