CREATE TABLE [dbo].[cat_UnidadNegocio] (
    [Id]            INT           NOT NULL,
    [IdEmpresa]     INT           NOT NULL,
    [Titulo]        VARCHAR (150) NOT NULL,
    [FechaRegistro] DATETIME      NULL,
    [Activo]        INT           NULL,
    CONSTRAINT [PK_cat_UnidadNegocio] PRIMARY KEY CLUSTERED ([Id] ASC)
);

