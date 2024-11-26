CREATE TABLE [dbo].[CuentasProveedor] (
    [Id]            INT          NOT NULL,
    [FechaRegistro] DATETIME     NOT NULL,
    [Banco]         VARCHAR (32) NULL,
    [Cuenta]        VARCHAR (32) NOT NULL,
    [CtaClabe]      VARCHAR (32) NOT NULL,
    [Sucursal]      VARCHAR (32) NULL,
    [Moneda]        VARCHAR (50) NULL,
    CONSTRAINT [PK_CuentasProveedor_1] PRIMARY KEY CLUSTERED ([Id] ASC, [Cuenta] ASC)
);

