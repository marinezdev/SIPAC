CREATE TABLE [dbo].[cxc_Facturas] (
    [NumFactura]   VARCHAR (32)    NOT NULL,
    [FechaFactura] DATETIME        NOT NULL,
    [Importe]      DECIMAL (18, 2) NOT NULL,
    [Rfc]          VARCHAR (16)    NOT NULL,
    [IdCliente]    INT             NOT NULL,
    [Cliente]      VARCHAR (80)    NOT NULL,
    [IdEmpresa]    INT             NOT NULL,
    [Empresa]      VARCHAR (80)    NOT NULL,
    [Anotaciones]  VARCHAR (250)   NULL,
    [Estado]       INT             NOT NULL
);

