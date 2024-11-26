CREATE TABLE [dbo].[xx_OrdenCompra] (
    [IdOrdenCompra] INT             NOT NULL,
    [FechaRegistro] DATETIME        NOT NULL,
    [rfc]           VARCHAR (50)    NOT NULL,
    [Proveedor]     VARCHAR (80)    NOT NULL,
    [Total]         DECIMAL (18, 2) NULL,
    [TipoMoneda]    VARCHAR (36)    NULL,
    [Estado]        INT             NOT NULL,
    [Nota]          VARCHAR (300)   NULL,
    CONSTRAINT [PK_xx_OrdenCompra] PRIMARY KEY CLUSTERED ([IdOrdenCompra] ASC)
);

