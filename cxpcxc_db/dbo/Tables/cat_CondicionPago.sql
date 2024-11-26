CREATE TABLE [dbo].[cat_CondicionPago] (
    [Id]            INT           NOT NULL,
    [FechaRegistro] DATETIME      NULL,
    [IdEmpresa]     INT           NOT NULL,
    [Titulo]        VARCHAR (100) NOT NULL,
    [Dias]          INT           NULL,
    [Activo]        INT           NULL,
    CONSTRAINT [PK_cat_CondicionPago_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

