CREATE TABLE [dbo].[cat_Servicios] (
    [Id]            INT           IDENTITY (7, 1) NOT NULL,
    [FechaRegistro] DATETIME      NOT NULL,
    [IdEmpresa]     INT           NOT NULL,
    [Titulo]        VARCHAR (100) NOT NULL,
    [Imagen]        VARCHAR (50)  NULL,
    [Activo]        INT           NULL,
    CONSTRAINT [PK_cat_Servicios] PRIMARY KEY CLUSTERED ([Id] ASC)
);

