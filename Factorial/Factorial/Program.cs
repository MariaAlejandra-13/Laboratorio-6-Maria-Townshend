using System;

namespace Factorial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Cálculo del Factorial del 0 al 10
            for (long contador = 0;
                 contador <= 10;
                 contador++)
            {
                Console.WriteLine(
                    "{0}! = {1}",
                    contador,
                    Factorial(contador)
                );
            }

            Console.ReadKey();
        }


        // Declaración recursiva del método Factorial
        public static long Factorial(long numero)
        {
            // Caso base
            if (numero <= 1)
            {
                return 1;
            }

            // Paso de recursividad
            else
            {
                return numero *
                       Factorial(numero - 1);
            }
        }
    }
}
