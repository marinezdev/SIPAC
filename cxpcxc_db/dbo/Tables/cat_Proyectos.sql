CREATE TABLE [dbo].[cat_Proyectos] (
    [Id]            INT           NOT NULL,
    [IdEmpresa]     INT           NOT NULL,
    [Titulo]        VARCHAR (100) NOT NULL,
    [FechaRegistro] DATETIME      NULL,
    [Activo]        INT           NULL,
    CONSTRAINT [PK_Cat_Proyectos] PRIMARY KEY CLUSTERED ([Id] ASC)
);

