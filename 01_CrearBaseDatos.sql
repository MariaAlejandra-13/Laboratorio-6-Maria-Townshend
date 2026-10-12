CREATE DATABASE ProductosDB;
GO

USE ProductosDB;
GO

CREATE TABLE Productos
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Cantidad INT NOT NULL,
    Imagen VARBINARY(MAX) NULL
);
GO