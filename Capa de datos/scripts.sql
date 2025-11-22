CREATE DATABASE digitalbank;


CREATE TABLE dbo.Usuario (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre VARCHAR(100) NULL,
    FechaNacimiento DATE NULL,
    Sexo CHAR(1) NULL
);


GO

CREATE PROCEDURE dbo.sp_Usuario_CRUD
    @Accion          VARCHAR(10),
    @Id              SMALLINT = NULL,
    @Nombre          VARCHAR(100) = NULL,
    @FechaNacimiento DATE = NULL,
    @Sexo            CHAR(1) = NULL
AS
BEGIN
    IF (@Accion = 'INSERT')
    BEGIN
        INSERT INTO Usuario (Nombre, FechaNacimiento, Sexo)
        VALUES (@Nombre, @FechaNacimiento, @Sexo);

        SELECT SCOPE_IDENTITY() AS Id;
        RETURN;
    END

    IF (@Accion = 'UPDATE')
    BEGIN
        UPDATE Usuario
        SET Nombre = @Nombre,
            FechaNacimiento = @FechaNacimiento,
            Sexo = @Sexo
        WHERE Id = @Id;

        SELECT @Id AS Id;
        RETURN;
    END

    IF (@Accion = 'DELETE')
    BEGIN
        DELETE FROM Usuario
        WHERE Id = @Id;

        SELECT @Id AS Id;
        RETURN;
    END

    IF (@Accion = 'SELECT')
    BEGIN
        SELECT Id,
               Nombre,
               FechaNacimiento,
               Sexo
        FROM Usuario
        WHERE (@Id IS NULL OR Id = @Id)
        ORDER BY Nombre;
        
        RETURN;
    END
END;