CREATE TABLE [dbo].[trf_ArchivoFondos] (
    [IdFondeo]       INT           NOT NULL,
    [IdDocumento]    INT           NOT NULL,
    [FechaRegistro]  DATETIME      NOT NULL,
    [ArchivoOrigen]  VARCHAR (64)  NOT NULL,
    [ArchivoDestino] VARCHAR (64)  NOT NULL,
    [Nota]           VARCHAR (255) NULL,
    CONSTRAINT [PK_trf_ArchivoFondos] PRIMARY KEY CLUSTERED ([IdFondeo] ASC, [IdDocumento] ASC)
);

