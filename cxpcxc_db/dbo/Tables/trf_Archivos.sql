CREATE TABLE [dbo].[trf_Archivos] (
    [IdSolicitud]    INT             NOT NULL,
    [FechaRegistro]  DATETIME        NOT NULL,
    [Tipo]           INT             NOT NULL,
    [IdDocumento]    INT             NOT NULL,
    [ArchivoOrigen]  VARCHAR (64)    NULL,
    [ArchivoDestino] VARCHAR (64)    NULL,
    [Cantidad]       DECIMAL (18, 2) NULL,
    [TipoCambio]     DECIMAL (18, 2) NULL,
    [Pesos]          DECIMAL (18, 2) NULL,
    [Nota]           VARCHAR (255)   NULL,
    [Idpago]         INT             NULL,
    CONSTRAINT [PK_trf_Archivos] PRIMARY KEY CLUSTERED ([IdSolicitud] ASC, [Tipo] ASC, [IdDocumento] ASC)
);

