CREATE TABLE [dbo].[cat_Clientes] (
    [Id]             INT           NOT NULL,
    [FechaRegistro]  DATETIME      NOT NULL,
    [IdEmpresa]      INT           NULL,
    [Nombre]         VARCHAR (80)  NOT NULL,
    [Rfc]            VARCHAR (16)  NOT NULL,
    [Direccion]      VARCHAR (255) NULL,
    [Ciudad]         VARCHAR (128) NULL,
    [Estado]         VARCHAR (32)  NULL,
    [Cp]             VARCHAR (5)   NULL,
    [ContactoProy]   VARCHAR (128) NULL,
    [ContactoFact]   VARCHAR (128) NULL,
    [Correo]         VARCHAR (64)  NULL,
    [Telefono]       VARCHAR (32)  NULL,
    [Extencion]      VARCHAR (16)  NULL,
    [ClienteSolomon] VARCHAR (16)  NULL,
    [Activo]         INT           NULL,
    CONSTRAINT [PK_Cat_Clientes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

