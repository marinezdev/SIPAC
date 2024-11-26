CREATE TABLE [dbo].[cat_Empresas] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [FechaRegistro] DATETIME      NULL,
    [Rfc]           VARCHAR (16)  NULL,
    [Nombre]        VARCHAR (150) NULL,
    [Activo]        INT           NULL,
    [Logo]          VARCHAR (20)  NULL,
    CONSTRAINT [PK_cat_Empresas] PRIMARY KEY CLUSTERED ([Id] ASC)
);

