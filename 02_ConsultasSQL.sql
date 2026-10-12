USE ProductosDB;
GO


-- CONSULTA 1 EJEMPLO DE INYECCIÓN SQL

SELECT *
FROM Productos
WHERE Nombre = '' OR '1'='1'
  AND Id = 0 OR '1'='1';

GO


-- CONSULTA 2 EJEMPLO DE RETARDO EN SQL SERVER

SELECT *
FROM Productos
WHERE Id = 1;

WAITFOR DELAY '00:00:15';

GO


-- CONSULTA 3 CONSULTA PARAMETRIZADA

DECLARE @Nombre VARCHAR(100) = 'Laptop HP Envy';
DECLARE @Id INT = 1;

SELECT *
FROM Productos
WHERE Nombre = @Nombre
  AND Id = @Id;

GO