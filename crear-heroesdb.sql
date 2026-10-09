IF DB_ID(N'HeroesDb') IS NULL
    CREATE DATABASE HeroesDb;
GO
USE HeroesDb;
GO
IF OBJECT_ID(N'dbo.Heroes', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Heroes
    (
        Id int IDENTITY(1,1) NOT NULL,
        Nombre nvarchar(100) NOT NULL,
        Ciudad nvarchar(100) NOT NULL,
        IdentidadSecreta nvarchar(100) NULL,
        CONSTRAINT PK__Heroes__3214EC075B58393A PRIMARY KEY CLUSTERED (Id)
    );
END
GO
IF OBJECT_ID(N'dbo.SuperPoderes', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.SuperPoderes
    (
        Id int IDENTITY(1,1) NOT NULL,
        Nombre nvarchar(100) NOT NULL,
        Descripcion nvarchar(250) NULL,
        HeroeId int NOT NULL,
        CONSTRAINT PK__SuperPod__3214EC077D987875 PRIMARY KEY CLUSTERED (Id),
        CONSTRAINT FK_SuperPoderes_Heroes FOREIGN KEY (HeroeId) REFERENCES dbo.Heroes (Id)
    );
END
GO
IF NOT EXISTS (SELECT 1 FROM dbo.Heroes)
BEGIN
    INSERT INTO dbo.Heroes (Nombre, Ciudad, IdentidadSecreta)
    VALUES (N'Centinela', N'Montevideo', N'Ana Ruiz');
END
GO
SELECT Id, Nombre, Ciudad FROM dbo.Heroes;
