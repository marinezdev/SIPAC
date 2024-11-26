CREATE TABLE [dbo].[cxc_Archivos] (
    [IdOrdenFactura] INT           NOT NULL,
    [FechaRegistro]  DATETIME      NOT NULL,
    [Tipo]           INT           NOT NULL,
    [IdDocumento]    INT           NOT NULL,
    [ArchivoOrigen]  VARCHAR (64)  NULL,
    [ArchivoDestino] VARCHAR (64)  NULL,
    [Nota]           VARCHAR (255) NULL,
    CONSTRAINT [PK_cxc_Archivos] PRIMARY KEY CLUSTERED ([IdOrdenFactura] ASC, [Tipo] ASC, [IdDocumento] ASC)
);

