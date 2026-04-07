-- Crear base de datos (si esta aún no existe)
CREATE DATABASE NotasDB;
GO

USE NotasDB;
GO

-- Crear tabla Cursos
IF OBJECT_ID('Cursos', 'U') IS NOT NULL
    DROP TABLE Cursos;
GO

CREATE TABLE Cursos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX) NULL
);
GO

-- Crear tabla Notas
IF OBJECT_ID('Notas', 'U') IS NOT NULL
    DROP TABLE Notas;
GO

CREATE TABLE Notas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(100) NOT NULL,
    Contenido NVARCHAR(MAX) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    CursoId INT NOT NULL,
    CONSTRAINT FK_Notas_Cursos FOREIGN KEY (CursoId) REFERENCES Cursos(Id)
);
GO

-- Procedimiento: Listar notas
CREATE OR ALTER PROCEDURE sp_GetNotas
AS
BEGIN
    SELECT 
        n.Id,
        n.Titulo,
        n.Contenido,
        n.FechaCreacion,
        n.CursoId,
        c.Id AS CursoIdReal,
        c.Nombre AS Curso
    FROM Notas n
    INNER JOIN Cursos c ON n.CursoId = c.Id;
END
GO

-- Procedimiento: Insertar nota
CREATE OR ALTER PROCEDURE sp_InsertNota
    @Titulo NVARCHAR(100),
    @Contenido NVARCHAR(MAX),
    @FechaCreacion DATETIME,
    @CursoId INT
AS
BEGIN
    INSERT INTO Notas (Titulo, Contenido, FechaCreacion, CursoId)
    VALUES (@Titulo, @Contenido, GETDATE(), @CursoId);
END
GO

-- Procedimiento: Actualizar nota
CREATE OR ALTER PROCEDURE sp_UpdateNota
    @Id INT,
    @Titulo NVARCHAR(100),
    @Contenido NVARCHAR(MAX),
    @CursoId INT
AS
BEGIN
    UPDATE Notas
    SET Titulo = @Titulo,
        Contenido = @Contenido,
        CursoId = @CursoId
    WHERE Id = @Id;
END
GO

-- Procedimiento: Eliminar nota
CREATE OR ALTER PROCEDURE sp_DeleteNota
    @Id INT
AS
BEGIN
    DELETE FROM Notas WHERE Id = @Id;
END
GO

-- Procedimiento: Listar cursos con cantidad de notas
CREATE OR ALTER PROCEDURE sp_GetCursos
AS
BEGIN
    SELECT 
        c.Id, 
        c.Nombre, 
        c.Descripcion,
        COUNT(n.Id) AS CantidadNotas
    FROM Cursos c
    LEFT JOIN Notas n ON c.Id = n.CursoId
    GROUP BY c.Id, c.Nombre, c.Descripcion;
END
GO

-- Procedimiento: Insertar curso
CREATE OR ALTER PROCEDURE sp_InsertCurso
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Cursos (Nombre, Descripcion)
    VALUES (@Nombre, @Descripcion);
END
GO

-- Procedimiento: Actualizar curso
CREATE OR ALTER PROCEDURE sp_UpdateCurso
    @Id INT,
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(MAX)
AS
BEGIN
    UPDATE Cursos
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE Id = @Id;
END
GO

-- Procedimiento: Eliminar curso
CREATE OR ALTER PROCEDURE sp_DeleteCurso
    @Id INT
AS
BEGIN
    DELETE FROM Cursos WHERE Id = @Id;
END
GO

-- Procedimiento: Obtener curso por Id (útil para Editar)
CREATE OR ALTER PROCEDURE sp_GetCursoById
    @Id INT
AS
BEGIN
    SELECT 
        c.Id, 
        c.Nombre, 
        c.Descripcion,
        COUNT(n.Id) AS CantidadNotas
    FROM Cursos c
    LEFT JOIN Notas n ON c.Id = n.CursoId
    WHERE c.Id = @Id
    GROUP BY c.Id, c.Nombre, c.Descripcion;
END
GO
