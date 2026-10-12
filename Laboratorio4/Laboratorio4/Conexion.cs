using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Laboratorio4
{
    public class Conexion
    {
        // Cadena de conexión utilizando SQL Server

        private static string cadenaConexion =
            @"Server=DESKTOP-V6IHOS5\SQLEXPRESS;Database=ProductosDB;Trusted_Connection=True;TrustServerCertificate=True;";

        // Obtener la conexión

        public static SqlConnection ObtenerConexion()
        {
            try
            {
                SqlConnection conexion = new SqlConnection(cadenaConexion);

                conexion.Open();

                return conexion;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error al conectar con SQL Server:\n" +
                    ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return null;
            }
        }

        // Obtener productos

        public static List<Producto> GetProductos(string filtro = "")
        {
            List<Producto> listaProductos = new List<Producto>();


            string query =
                @"SELECT Id, Nombre, Precio, Cantidad, Imagen
                  FROM Productos";


            // Si existe filtro, agregamos las condiciones de búsqueda.
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                query +=
                    @" WHERE CAST(Id AS VARCHAR(20)) LIKE @filtro
                       OR Nombre LIKE @filtro
                       OR CAST(Precio AS VARCHAR(50)) LIKE @filtro
                       OR CAST(Cantidad AS VARCHAR(20)) LIKE @filtro";
            }


            try
            {
                using (SqlConnection conexion = ObtenerConexion())
                {
                    if (conexion == null)
                        return listaProductos;


                    using (SqlCommand cmd =
                           new SqlCommand(
                               query,
                               conexion))
                    {
                        // Agregar parámetro de búsqueda solamente cuando existe un filtro.
                        if (!string.IsNullOrWhiteSpace(filtro))
                        {
                            cmd.Parameters.AddWithValue(
                                "@filtro",
                                "%" + filtro + "%");
                        }


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Producto prod = new Producto();

                                prod.Id =
                                    Convert.ToInt32(
                                        reader["Id"]);

                                prod.Nombre =
                                    reader["Nombre"]
                                    .ToString();

                                prod.Precio =
                                    Convert.ToDecimal(
                                        reader["Precio"]);

                                prod.Cantidad =
                                    Convert.ToInt32(
                                        reader["Cantidad"]);


                                // La imagen puede venir NULL desde la base de datos.
                                if (reader["Imagen"] != DBNull.Value)
                                {
                                    prod.Imagen = (byte[]) reader["Imagen"];
                                }
                                else
                                {
                                    prod.Imagen = null;
                                }


                                listaProductos.Add(prod);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error al cargar los productos:\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


            return listaProductos;
        }


        // INSERT SEGURO
 
        public static bool InsertSeguro(string tbName, Dictionary<string, object> data)
        {
            // Obtener los nombres de columnas desde las claves del Dictionary.
            var columns = string.Join(", ", data.Keys);

            // Crear automáticamente @Nombre, @Precio, @Cantidad, @Imagen
            var placeholders = "@" + string.Join(", @", data.Keys);

            string sql =
                $"INSERT INTO {tbName} " +
                $"({columns}) " +
                $"VALUES ({placeholders})";

            try
            {
                using (SqlConnection conexion = ObtenerConexion())
                {
                    if (conexion == null)
                        return false;


                    using (SqlCommand stmt = new SqlCommand(sql, conexion))
                    {
                        // Recorrer los datos del Dictionary y crear los parámetros SQL.
                        foreach (var kvp in data)
                        {
                            stmt.Parameters.AddWithValue(
                                "@" + kvp.Key,
                                kvp.Value ??
                                DBNull.Value);
                        }

                        int filasAfectadas =
                            stmt.ExecuteNonQuery();

                        return filasAfectadas > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error en INSERT:\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        // MODIFICAR PRODUCTO 
        public static bool ModificarProducto(Producto producto)
        {
            string sql =
                @"UPDATE Productos
                  SET Nombre = @Nombre,
                      Precio = @Precio,
                      Cantidad = @Cantidad,
                      Imagen = @Imagen
                  WHERE Id = @Id";

            try
            {
                using (SqlConnection conexion = ObtenerConexion())
                {
                    if (conexion == null)
                        return false;

                    using (SqlCommand cmd = new SqlCommand(sql,conexion))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Nombre",
                            producto.Nombre);

                        cmd.Parameters.AddWithValue(
                            "@Precio",
                            producto.Precio);

                        cmd.Parameters.AddWithValue(
                            "@Cantidad",
                            producto.Cantidad);

                        cmd.Parameters.AddWithValue(
                            "@Imagen",
                            (object)producto.Imagen ??
                            DBNull.Value);

                        cmd.Parameters.AddWithValue(
                            "@Id",
                            producto.Id);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        return filasAfectadas > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error al modificar el producto:\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }


        // ELIMINAR PRODUCTO

        public static bool EliminarProducto(int id)
        {
            string sql =
                @"DELETE FROM Productos
                  WHERE Id = @Id";


            try
            {
                using (SqlConnection conexion = ObtenerConexion())
                {
                    if (conexion == null)
                        return false;

                    using (SqlCommand cmd =
                           new SqlCommand(
                               sql,
                               conexion))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Id",
                            id);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        return filasAfectadas > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error al eliminar el producto:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }
    }
}