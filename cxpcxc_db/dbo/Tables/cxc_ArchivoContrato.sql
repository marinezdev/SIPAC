CREATE TABLE [dbo].[cxc_ArchivoContrato] (
    [IdServicio]     INT          NOT NULL,
    [FechaRegistro]  DATETIME     NOT NULL,
    [ArchivoDestino] VARCHAR (65) NOT NULL,
    CONSTRAINT [PK_cxc_ArchivoContratos] PRIMARY KEY CLUSTERED ([IdServicio] ASC)
);

