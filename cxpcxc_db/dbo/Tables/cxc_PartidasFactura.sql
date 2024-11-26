CREATE TABLE [dbo].[cxc_PartidasFactura] (
    [IdOrdenFactura] INT           NOT NULL,
    [NoPartida]      INT           NOT NULL,
    [Cantidad]       INT           NULL,
    [Codigo]         VARCHAR (32)  NULL,
    [Descripcion]    VARCHAR (250) NULL,
    [Precio]         NCHAR (10)    NULL,
    CONSTRAINT [PK_cxc_PartidasFactura] PRIMARY KEY CLUSTERED ([IdOrdenFactura] ASC, [NoPartida] ASC)
);

