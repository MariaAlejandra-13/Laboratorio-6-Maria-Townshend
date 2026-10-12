using System;
using System.Collections.Generic;

namespace DiccionarioNuevo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Diccionario que representa los datos de un producto
            Dictionary<string, object> datosProducto =
                new Dictionary<string, object>
                {
                    { "Nombre", "Laptop HP Envy" },
                    { "Precio", 850.99m },
                    { "Cantidad", 15 }
                };

            Console.WriteLine("DATOS DEL DICCIONARIO");

            // Recorrer las claves del diccionario
            foreach (var clave in datosProducto.Keys)
            {
                Console.WriteLine(
                    $"Clave: {clave} | Valor: {datosProducto[clave]}"
                );
            }

            Console.WriteLine("\n");
            Console.WriteLine("GENERACIÓN DE CONSULTAS");
            Console.WriteLine("\n");


            // GENERAR CLAUSULA SET PARA UPDATE
     
            List<string> setParts = new List<string>();

            foreach (var key in datosProducto.Keys)
            {
                setParts.Add($"{key} = @{key}");
            }

            string setClause =
                string.Join(", ", setParts);


            Console.WriteLine("Cláusula SET generada:");

            Console.WriteLine(setClause);


           
            // GENERAR INSERT

            string columns =
                string.Join(", ", datosProducto.Keys);

            string placeholders =
                "@" + string.Join(", @", datosProducto.Keys);

            string sqlInsert =
                $"INSERT INTO Productos ({columns}) " +
                $"VALUES ({placeholders});";


            Console.WriteLine("\nConsulta INSERT:");

            Console.WriteLine(sqlInsert);


            // GENERAR UPDATE

            string sqlUpdate =
                $"UPDATE Productos " +
                $"SET {setClause} " +
                $"WHERE Id = @Id;";


            Console.WriteLine("\nConsulta UPDATE:");

            Console.WriteLine(sqlUpdate);


            Console.WriteLine("\nPresione una tecla para finalizar...");

            Console.ReadKey();
        }
    }
}