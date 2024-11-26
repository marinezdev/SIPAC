CREATE TABLE [dbo].[cat_Rechazos] (
    [Id]            INT           NOT NULL,
    [IdEmpresa]     INT           NOT NULL,
    [Titulo]        VARCHAR (100) NOT NULL,
    [FechaRegistro] DATETIME      NULL,
    [Activo]        INT           NULL,
    CONSTRAINT [PK_cat_Rechazos] PRIMARY KEY CLUSTERED ([Id] ASC)
);

