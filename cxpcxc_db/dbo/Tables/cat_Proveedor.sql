CREATE TABLE [dbo].[cat_Proveedor] (
    [Id]            INT           NOT NULL,
    [FechaRegistro] DATETIME      NOT NULL,
    [IdEmpresa]     NCHAR (10)    NULL,
    [Nombre]        VARCHAR (80)  NULL,
    [Rfc]           VARCHAR (16)  NULL,
    [Contacto]      VARCHAR (80)  NULL,
    [Correo]        VARCHAR (64)  NULL,
    [Telefono]      VARCHAR (32)  NULL,
    [Extencion]     VARCHAR (16)  NULL,
    [SinFactura]    INT           NULL,
    [Direccion]     VARCHAR (255) NULL,
    [Ciudad]        VARCHAR (128) NULL,
    [Estado]        VARCHAR (32)  NULL,
    [CP]            VARCHAR (5)   NULL,
    [Activo]        INT           NULL,
    CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED ([Id] ASC)
);

