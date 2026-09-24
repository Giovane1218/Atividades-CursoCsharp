internal class Exercicio3
{
    static void Main()
    {
        Console.WriteLine("Digite um número: ");
        double valor1 = double.Parse(Console.ReadLine());
        Console.WriteLine("Digite outro número: ");
        double valor2 = double.Parse(Console.ReadLine());


        Console.WriteLine($"A soma dos valores é: {valor1 + valor2}");
        Console.WriteLine($"A subtração do valores é : {valor1 - valor2}");
        Console.WriteLine($"A multiplicaçao dos valores é: {valor1 * valor2}");
        if ( valor2 == 0)
            Console.WriteLine($"Não pode dividir por zero");
        else
            Console.WriteLine($"A divisão do valores é: {valor1 / valor2}");


        Console.WriteLine($"A média entre os valores é: {(valor1 + valor2) / 2}");
    }
}