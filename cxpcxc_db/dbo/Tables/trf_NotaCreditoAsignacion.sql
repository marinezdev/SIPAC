CREATE TABLE [dbo].[trf_NotaCreditoAsignacion] (
    [FechaRegistro] DATETIME        NOT NULL,
    [IdNotaCredito] INT             NOT NULL,
    [IdSolicitud]   INT             NOT NULL,
    [Monto]         DECIMAL (18, 2) NOT NULL,
    [IdUsr]         INT             NOT NULL,
    CONSTRAINT [PK_trf_NotaCreditoAsignacion] PRIMARY KEY CLUSTERED ([IdNotaCredito] ASC, [IdSolicitud] ASC)
);

