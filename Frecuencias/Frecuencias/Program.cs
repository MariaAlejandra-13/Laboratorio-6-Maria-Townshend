using System;

namespace Frecuencias
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random numerosAleatorios = new Random();

            int frecuencia1 = 0;
            int frecuencia2 = 0;
            int frecuencia3 = 0;
            int frecuencia4 = 0;
            int frecuencia5 = 0;
            int frecuencia6 = 0;

            int cara; // Almacena el último valor que se tiró

            for (int tiro = 1; tiro <= 6000; tiro++)
            {
                // Números del 1 al 6
                cara = numerosAleatorios.Next(1, 7);

                // Determina el valor del tiro del 1 al 6
                // e incrementa el contador apropiado

                switch (cara)
                {
                    case 1:
                        frecuencia1++;
                        break;

                    case 2:
                        frecuencia2++;
                        break;

                    case 3:
                        frecuencia3++;
                        break;

                    case 4:
                        frecuencia4++;
                        break;

                    case 5:
                        frecuencia5++;
                        break;

                    case 6:
                        frecuencia6++;
                        break;

                    default:
                        Console.WriteLine("Hubo un error de entrada");
                        break;
                }
            }

            Console.WriteLine("Cara\tFrecuencia");

            Console.WriteLine(
                "1\t{0}\n2\t{1}\n3\t{2}\n4\t{3}\n5\t{4}\n6\t{5}",
                frecuencia1,
                frecuencia2,
                frecuencia3,
                frecuencia4,
                frecuencia5,
                frecuencia6
            );

            Console.ReadKey();
        }
    }
}
