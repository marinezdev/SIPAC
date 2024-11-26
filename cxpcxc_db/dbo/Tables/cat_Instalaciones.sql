CREATE TABLE [dbo].[cat_Instalaciones] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [IdParent] INT            NOT NULL,
    [Nombre]   NVARCHAR (100) NOT NULL,
    [M2]       INT            NOT NULL,
    [Ocupado]  BIT            NOT NULL,
    [Orden]    INT            NOT NULL,
    [Activo]   BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

