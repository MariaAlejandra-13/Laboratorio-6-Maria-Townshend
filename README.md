# Universidad Tecnológica de Panamá

# Facultad de Ingeniería de Sistemas Computacionales

## Laboratorio de Temas Varios

### Repaso del CRUD, Inyección SQL, Consultas Parametrizadas, Métodos Sobrecargados, Recursividad y Frecuencias

### Herramientas de Programación Aplicada III (.Net)

**Grupo:** 1IL133  
**Instructor:** Ing. Irina Fong  
**Semestre:** II Semestre 2026  
**Fecha:** 12 de octubre de 2026  

---

# Descripción

En este laboratorio se desarrollaron diferentes ejercicios relacionados con acceso a bases de datos, seguridad, estructuras de datos y conceptos de Programación Orientada a Objetos utilizando C# y Microsoft SQL Server.

Las actividades realizadas incluyen:

- Consultas SQL e Inyección SQL.
- Consultas parametrizadas.
- Manipulación de cadenas para construir sentencias `INSERT` y `UPDATE`.
- Uso de `Dictionary<string, object>`.
- Métodos sobrecargados.
- Recursividad mediante el cálculo del factorial.
- Frecuencias y conteo de valores.
- Conexión entre aplicaciones C# y Microsoft SQL Server.

---

# Tecnologías Utilizadas

- **Lenguaje:** C#
- **SDK:** .NET
- **IDE:** Visual Studio Community
- **Base de datos:** Microsoft SQL Server
- **Administrador de base de datos:** SQL Server Management Studio
- **Proveedor SQL para C#:** Microsoft.Data.SqlClient
- **Control de versiones:** Git
- **Repositorio:** GitHub
- **Sistema operativo:** Windows

---

# Estructura del Repositorio

```text
LaboratorioTemasVarios
│
├── README.md
├── Laboratorio.sln / Laboratorio.slnx
│
├── Escenario1_Cadenas
├── Escenario2_Sobrecarga
├── Escenario2_Factorial
├── Escenario3_Frecuencias
│
├── SQL
│   ├── 01_CrearBaseDatos.sql
│   └── 02_ConsultasSQL.sql
│
└── Evidencias
    ├── problema1.png
    ├── problema2.png
    ├── problema3.png
    ├── problema4.png
    └── problema5.png
```

---

# Guía de Instalación y Uso

## 1. Clonar el repositorio

Abrir una terminal y ejecutar:

```bash
git clone URL_DEL_REPOSITORIO
```

Luego ingresar a la carpeta:

```bash
cd NOMBRE_DEL_REPOSITORIO
```

---

## 2. Restaurar dependencias

Ejecutar:

```bash
dotnet restore
```

Esto descargará las dependencias necesarias para los proyectos de C#.

---

## 3. Abrir la solución

Abrir Visual Studio Community y seleccionar:

```text
Open a project or solution
```

Luego seleccionar:

```text
Laboratorio.sln
```

o:

```text
Laboratorio.slnx
```

dependiendo del formato utilizado en el proyecto.

---

## 4. Crear la base de datos

Abrir Microsoft SQL Server Management Studio.

Ejecutar el archivo:

```text
SQL/01_CrearBaseDatos.sql
```

Este script crea:

```text
ProductosDB
```

y la tabla:

```text
Productos
```

---

## 5. Ejecutar las consultas SQL

Abrir el archivo:

```text
SQL/02_ConsultasSQL.sql
```

desde SQL Server Management Studio.

Este archivo contiene los ejemplos de consultas SQL utilizados durante el laboratorio.

---

# Problema #1 - Consultas SQL

## Descripción

En este problema se estudiaron diferentes consultas relacionadas con seguridad en bases de datos.

Se trabajó con la tabla:

```text
Productos
```

de la base de datos:

```text
ProductosDB
```

---

## Consulta #1 - Ejemplo de Inyección SQL

```sql
SELECT *
FROM Productos
WHERE Nombre = '' OR '1'='1'
  AND Id = 0 OR '1'='1';
```

La condición:

```sql
'1'='1'
```

siempre es verdadera.

Este ejemplo permite observar cómo una consulta construida de forma insegura puede modificar la lógica esperada del `WHERE`.

---

## Consulta #2 - Ejemplo de Retardo

En SQL Server se utiliza:

```sql
WAITFOR DELAY
```

para generar un retraso controlado.

```sql
SELECT *
FROM Productos
WHERE Id = 1;

WAITFOR DELAY '00:00:15';
```

El comando genera una espera de 15 segundos.

---

## Consulta #3 - Consulta Parametrizada

```sql
DECLARE @Nombre VARCHAR(100) = 'Laptop HP Envy';
DECLARE @Id INT = 1;

SELECT *
FROM Productos
WHERE Nombre = @Nombre
  AND Id = @Id;
```

Los parámetros:

```text
@Nombre
@Id
```

permiten mantener los valores separados de la estructura de la consulta.

---

## Aplicación en C#

También se utilizó una consulta parametrizada desde C# utilizando:

```csharp
SqlCommand
```

junto con:

```csharp
cmd.Parameters.AddWithValue()
```

Ejemplo:

```csharp
string query =
    @"SELECT *
      FROM Productos
      WHERE Nombre = @Nombre
      AND Id = @Id";

using (SqlCommand cmd =
       new SqlCommand(query, conexion))
{
    cmd.Parameters.AddWithValue(
        "@Nombre",
        txtNombre.Text.Trim()
    );

    cmd.Parameters.AddWithValue(
        "@Id",
        idSeleccionado
    );

    using (SqlDataReader reader =
           cmd.ExecuteReader())
    {
        while (reader.Read())
        {
            // Procesar resultado
        }
    }
}
```

---

# Resultado - Problema #1

<!--
CAPTURA:
Abre SQL Server Management Studio.

Ejecuta las tres consultas.

Puedes utilizar una captura por consulta o una captura donde se vean las pestañas correspondientes.

Debe observarse:
- La consulta.
- El resultado.
- La base ProductosDB.
- La tabla Productos.
-->

<img width="419" height="283" alt="image" src="https://github.com/user-attachments/assets/2487c005-21f8-403d-9d42-dae36d24219d" />
<img width="312" height="274" alt="image" src="https://github.com/user-attachments/assets/49f42cd1-318d-428f-8e72-f9b5f3ffef5a" />
<img width="344" height="260" alt="image" src="https://github.com/user-attachments/assets/6b5dae1e-b731-4b02-8a9c-702b2ab1f5e4" />


---

# Problema #2 - Cadenas, Dictionary e INSERT / UPDATE

## Descripción

En este ejercicio se utilizó:

```csharp
Dictionary<string, object>
```

para representar dinámicamente los campos y valores correspondientes a un producto.

Ejemplo:

```csharp
Dictionary<string, object> datosProducto =
    new Dictionary<string, object>
    {
        { "Nombre", "Laptop HP Envy" },
        { "Precio", 850.99m },
        { "Cantidad", 15 }
    };
```

La propiedad:

```csharp
.Keys
```

permite obtener las claves almacenadas en el diccionario.

En este caso:

```text
Nombre
Precio
Cantidad
```

---

## Recorrido del Dictionary

El diccionario se recorre mediante:

```csharp
foreach (var clave in datosProducto.Keys)
{
    Console.WriteLine(
        $"Clave: {clave} | Valor: {datosProducto[clave]}"
    );
}
```

Esto permite visualizar tanto las claves como sus respectivos valores.

---

## Generación de la cláusula SET

Para construir dinámicamente una consulta `UPDATE` se utiliza una lista:

```csharp
List<string> setParts =
    new List<string>();
```

Posteriormente:

```csharp
foreach (var key in datosProducto.Keys)
{
    setParts.Add(
        $"{key} = @{key}"
    );
}
```

Finalmente:

```csharp
string setClause =
    string.Join(", ", setParts);
```

El resultado es:

```text
Nombre = @Nombre, Precio = @Precio, Cantidad = @Cantidad
```

---

## Generación de INSERT

Las columnas son generadas mediante:

```csharp
string columns =
    string.Join(", ", datosProducto.Keys);
```

Los parámetros se generan mediante:

```csharp
string placeholders =
    "@" + string.Join(", @", datosProducto.Keys);
```

Finalmente:

```csharp
string sqlInsert =
    $"INSERT INTO Productos ({columns}) " +
    $"VALUES ({placeholders});";
```

Resultado:

```sql
INSERT INTO Productos
(Nombre, Precio, Cantidad)
VALUES
(@Nombre, @Precio, @Cantidad);
```

---

## Generación de UPDATE

```csharp
string sqlUpdate =
    $"UPDATE Productos " +
    $"SET {setClause} " +
    $"WHERE Id = @Id;";
```

Resultado:

```sql
UPDATE Productos
SET Nombre = @Nombre,
    Precio = @Precio,
    Cantidad = @Cantidad
WHERE Id = @Id;
```

---

# Resultado - Problema #2

<!--
CAPTURA:
Ejecuta el programa de consola.

La consola debe mostrar:

1. Clave: Nombre | Valor: Laptop HP Envy
2. Clave: Precio | Valor: 850.99
3. Clave: Cantidad | Valor: 15

Debajo debe aparecer:

Cláusula SET generada:
Nombre = @Nombre, Precio = @Precio, Cantidad = @Cantidad

Consulta INSERT:
INSERT INTO Productos ...

Consulta UPDATE:
UPDATE Productos SET ...

Toma la captura con TODA esa información visible.
-->

<img width="684" height="311" alt="image" src="https://github.com/user-attachments/assets/d92e5c68-66fd-480d-ae9a-b0009beaa81f" />


---

# Problema #3 - Métodos Sobrecargados

## Descripción

La sobrecarga de métodos permite crear varios métodos con el mismo nombre, siempre que tengan diferentes firmas.

En este ejercicio se implementó el método:

```csharp
Cuadrado()
```

en dos versiones.

Una recibe:

```csharp
int
```

y otra recibe:

```csharp
double
```

---

## Método con int

```csharp
public int Cuadrado(int valorInt)
{
    Console.WriteLine(
        "Se llamó a Cuadrado con argumento int: {0}",
        valorInt
    );

    return valorInt * valorInt;
}
```

---

## Método con double

```csharp
public double Cuadrado(double valorDouble)
{
    Console.WriteLine(
        "Se llamó a Cuadrado con argumento double: {0}",
        valorDouble
    );

    return valorDouble * valorDouble;
}
```

C# selecciona automáticamente la versión adecuada de acuerdo con el tipo del argumento enviado.

---

# Resultado - Problema #3

<!--
CAPTURA:
Ejecuta el programa de Métodos Sobrecargados.

Debe verse en consola:
- La llamada con int.
- Resultado del cuadrado de 7.
- La llamada con double.
- Resultado del cuadrado de 7.5.
- Las demás pruebas utilizadas en el programa.

Toma una captura de toda la consola.
-->

<img width="338" height="146" alt="image" src="https://github.com/user-attachments/assets/cc6e25d2-5bb7-4801-8615-73961e8f20ec" />


---

# Problema #4 - Recursividad y Factorial

## Descripción

Se implementó un método recursivo para calcular el factorial de un número.

El factorial se define como:

```text
n! = n × (n - 1) × (n - 2) × ... × 1
```

Por ejemplo:

```text
5! = 5 × 4 × 3 × 2 × 1 = 120
```

---

## Método Factorial

```csharp
public static long Factorial(long numero)
{
    if (numero <= 1)
    {
        return 1;
    }
    else
    {
        return numero *
               Factorial(numero - 1);
    }
}
```

---

## Caso Base

La condición:

```csharp
if (numero <= 1)
```

representa el caso base.

Esta condición evita que el método continúe llamándose indefinidamente.

---

## Paso Recursivo

```csharp
return numero *
       Factorial(numero - 1);
```

En cada llamada el valor disminuye hasta llegar al caso base.

---

# Resultado - Problema #4

<!--
CAPTURA:
Ejecuta el programa Factorial.

Debe verse la salida desde:

0! = 1

hasta:

10! = 3628800

Toma una captura donde se vea toda la secuencia.
-->

<img width="245" height="199" alt="image" src="https://github.com/user-attachments/assets/a5f16466-8425-4620-b0c3-7c5d3e4e4408" />

---

# Problema #5 - Frecuencias

## Descripción

En este ejercicio se utilizó la clase:

```csharp
Random
```

para simular múltiples lanzamientos de un dado.

Cada lanzamiento genera un número entre:

```text
1 y 6
```

Se utilizan seis contadores para almacenar cuántas veces aparece cada cara.

---

## Generación de Valores Aleatorios

```csharp
Random numerosAleatorios =
    new Random();
```

Para obtener valores entre 1 y 6:

```csharp
cara =
    numerosAleatorios.Next(1, 7);
```

---

## Conteo de Frecuencias

Se utiliza:

```csharp
switch
```

para determinar qué contador debe incrementarse.

Ejemplo:

```csharp
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
}
```

La simulación realiza:

```text
6000 lanzamientos
```

---

# Resultado - Problema #5

<!--
CAPTURA:
Ejecuta el programa Frecuencias.

La consola debe mostrar:

Cara    Frecuencia
1       ...
2       ...
3       ...
4       ...
5       ...
6       ...

Los números cambiarán cada vez porque son aleatorios.

Toma captura donde se vean las seis caras y sus frecuencias.
-->

<img width="251" height="155" alt="image" src="https://github.com/user-attachments/assets/96d84578-69b1-45f3-9f4f-9cc2d9fb00f5" />


---

# Seguridad y Consultas Parametrizadas

Uno de los temas principales del laboratorio fue la diferencia entre construir consultas mediante concatenación directa y utilizar consultas parametrizadas.

Una consulta insegura puede permitir que la entrada proporcionada por el usuario cambie la estructura de una sentencia SQL.

Las consultas parametrizadas separan:

```text
Estructura SQL
```

de:

```text
Valores proporcionados por el usuario
```

Por ejemplo:

```csharp
string sql =
    "SELECT * FROM Productos WHERE Nombre = @Nombre";
```

y posteriormente:

```csharp
cmd.Parameters.AddWithValue(
    "@Nombre",
    nombre
);
```

De esta manera el valor recibido es tratado como un dato y no como parte de la estructura SQL.

---

# Scripts SQL

El repositorio incluye una carpeta:

```text
SQL
```

con los archivos:

```text
01_CrearBaseDatos.sql
02_ConsultasSQL.sql
```

El primer archivo permite reconstruir la base de datos utilizada en el laboratorio.

El segundo contiene las consultas SQL utilizadas para realizar las pruebas correspondientes.

---

# Resultados Obtenidos

Al finalizar el laboratorio se logró aplicar diferentes conceptos de programación y acceso a datos.

Se implementaron:

- Consultas SQL.
- Ejemplos de Inyección SQL.
- Consultas parametrizadas.
- Manipulación dinámica de cadenas SQL.
- `Dictionary<string, object>`.
- `List<string>`.
- `.Keys`.
- `foreach`.
- `string.Join()`.
- Métodos sobrecargados.
- Recursividad.
- Factorial.
- Generación de números aleatorios.
- Conteo de frecuencias.
- Conexión con Microsoft SQL Server.
- Uso de parámetros en consultas.

---

# Conclusión

Este laboratorio permitió reforzar diferentes conceptos de C# y acceso a bases de datos dentro de una misma práctica.

La construcción dinámica de consultas permitió comprender el funcionamiento de estructuras como `Dictionary`, `List`, `foreach` y `string.Join()`, mientras que el estudio de Inyección SQL permitió identificar los riesgos asociados a la construcción insegura de consultas y la importancia de utilizar parámetros.

La implementación de métodos sobrecargados permitió observar cómo un mismo nombre de método puede utilizarse con diferentes tipos de parámetros. Por otro lado, el cálculo del factorial permitió aplicar recursividad identificando claramente el caso base y el paso recursivo.

Finalmente, el ejercicio de frecuencias permitió trabajar con números aleatorios, estructuras `switch` y contadores para analizar la repetición de valores dentro de un conjunto de datos.

```


Información del Estudiante

Nombre: Maria Townshend
Grupo: 1IL133  
Curso: Herramientas de Programación Aplicada III (.Net)  
Institución: Universidad Tecnológica de Panamá  
Facultad: Facultad de Ingeniería de Sistemas Computacionales  
Instructor: Ing. Irina Fong  
Semestre: II Semestre 2026  
Fecha de entrega: 12 de octubre de 2026
