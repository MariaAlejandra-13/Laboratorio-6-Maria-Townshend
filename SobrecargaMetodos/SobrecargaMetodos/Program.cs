using System;

namespace SobrecargaMetodos
{
    public class SobreCarga
    {
        // Prueba los métodos Cuadrado sobrecargados
        public void ProbarMetodosSobreCargados()
        {
            Console.WriteLine(
                "El cuadrado del integer 7 es {0}",
                Cuadrado(7)
            );

            Console.WriteLine(
                "El cuadrado del double 7.5 es {0}",
                Cuadrado(7.5)
            );
        }


        public int Cuadrado(int valorInt)
        {
            Console.WriteLine(
                "Se llamó a Cuadrado con argumento int: {0}",
                valorInt
            );

            return valorInt * valorInt;
        }


        public double Cuadrado(double valorDouble)
        {
            Console.WriteLine(
                "Se llamó a Cuadrado con argumento double: {0}",
                valorDouble
            );

            return valorDouble * valorDouble;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // Instancia la clase y ejecuta el método
            SobreCarga varSobreCarga = new SobreCarga();

            varSobreCarga.ProbarMetodosSobreCargados();

            varSobreCarga.Cuadrado(8);

            Console.WriteLine(
                "El cuadrado de {0}",
                varSobreCarga.Cuadrado(9)
            );

            Console.ReadKey();
        }
    }
}
