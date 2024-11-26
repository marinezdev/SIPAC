CREATE TABLE [dbo].[trf_NotaCreditoArchivos] (
    [IdNotaCredito] INT          NOT NULL,
    [Tipo]          INT          NOT NULL,
    [Nombre]        VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_trf_NotaCreditoArchivos] PRIMARY KEY CLUSTERED ([IdNotaCredito] ASC)
);

