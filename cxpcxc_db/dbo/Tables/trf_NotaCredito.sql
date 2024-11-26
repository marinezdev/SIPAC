CREATE TABLE [dbo].[trf_NotaCredito] (
    [IdNotaCredito]     INT             IDENTITY (1, 1) NOT NULL,
    [FechaRegistro]     DATETIME        NOT NULL,
    [IdEmpresa]         INT             NOT NULL,
    [Fecha]             DATETIME        NOT NULL,
    [RFC]               VARCHAR (16)    NOT NULL,
    [Proveedor]         VARCHAR (80)    NOT NULL,
    [Descripcion]       VARCHAR (256)   NOT NULL,
    [Importe]           DECIMAL (18, 2) NOT NULL,
    [Moneda]            VARCHAR (8)     NOT NULL,
    [ImportePendiente]  DECIMAL (18, 2) NOT NULL,
    [Estado]            INT             NOT NULL,
    [IdUsr]             INT             NOT NULL,
    [IdSolicitudOrigen] INT             NULL,
    CONSTRAINT [PK_trf_NotaCredito] PRIMARY KEY CLUSTERED ([IdNotaCredito] ASC),
    CONSTRAINT [FK_trf_NotaCredito_trf_NotaCredito] FOREIGN KEY ([IdNotaCredito]) REFERENCES [dbo].[trf_NotaCredito] ([IdNotaCredito])
);

