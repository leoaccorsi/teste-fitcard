CREATE DATABASE TestePratico
GO

USE [TestePratico]
GO

/****** Object: Table [dbo].[Categoria] Script Date: 08/11/2018 21:05:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Categoria] (
    [Id]   BIGINT       IDENTITY (1, 1) NOT NULL,
    [nome] VARCHAR (50) NOT NULL
);


/****** Object: Table [dbo].[Estabelecimento] Script Date: 08/11/2018 21:06:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Estabelecimento] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [razao_social]  VARCHAR (100) NOT NULL,
    [nome_fantasia] VARCHAR (100) NULL,
    [cnpj]          VARCHAR (18)  NOT NULL,
    [email]         VARCHAR (100) NULL,
    [endereco]      VARCHAR (100) NULL,
    [cidade]        VARCHAR (50)  NULL,
    [estado]        VARCHAR (50)  NULL,
    [telefone]      VARCHAR (20)  NULL,
    [data_cadastro] DATETIME      NOT NULL,
    [cod_categoria] BIGINT        NULL,
    [status]        SMALLINT      NOT NULL,
    [agencia]       VARCHAR (5)   NULL,
    [conta]         VARCHAR (8)   NULL
);

INSERT INTO Categoria(nome) VALUES ('Supermercado')
INSERT INTO Categoria(nome) VALUES ('Restaurante')
INSERT INTO Categoria(nome) VALUES ('Borracharia')
INSERT INTO Categoria(nome) VALUES ('Posto')
INSERT INTO Categoria(nome) VALUES ('Oficina')