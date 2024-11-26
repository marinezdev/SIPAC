CREATE TABLE [dbo].[trf_ConciliarPago] (
    [Id]            INT             NOT NULL,
    [FechaRegistro] DATETIME        NOT NULL,
    [Referencia]    VARCHAR (50)    NULL,
    [Banco]         VARCHAR (50)    NULL,
    [FechaPago]     DATETIME        NOT NULL,
    [TipoCambio]    INT             NOT NULL,
    [Importe]       DECIMAL (18, 2) NOT NULL,
    [Moneda]        INT             NOT NULL,
    [Idusr]         INT             NOT NULL,
    [Estado]        INT             NOT NULL,
    CONSTRAINT [PK_trf_ConciliarPago] PRIMARY KEY CLUSTERED ([Id] ASC)
);

