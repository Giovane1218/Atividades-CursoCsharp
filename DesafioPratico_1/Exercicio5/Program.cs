using System;

internal class exercicio5
{
    static string AddPlaca()
    {
        string placa;

        do
        {
            Console.WriteLine("Digite a placa do veículo sem o traço (-):");
            placa = Console.ReadLine().Replace(" ", "");

            if (placa.Length != 7)
            {
                Console.WriteLine("A placa deve conter 7 caracteres!");
            }

        } while (placa.Length != 7);

        return placa;
    }

    static void Main()
    {
        string placa = AddPlaca();

        char[] placa7 = placa.ToCharArray();

        bool valida = true;

        for (int i = 0; i < placa7.Length; i++)
        {
            if (i < 3)
            {
                if (!char.IsLetter(placa7[i]))
                {
                    valida = false;
                    break;
                }
            }
            else
            {
                if (!char.IsDigit(placa7[i]))
                {
                    valida = false;
                    break;
                }
            }
        }

        if (valida)
        {
            Console.WriteLine($"A placa {placa} é válida!");
        }
        else
        {
            Console.WriteLine($"A placa {placa} é inválida!");
        }
    }
}