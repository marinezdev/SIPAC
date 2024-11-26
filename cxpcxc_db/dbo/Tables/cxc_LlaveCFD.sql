CREATE TABLE [dbo].[cxc_LlaveCFD] (
    [IdOrdenFactura] INT            NOT NULL,
    [FechaRegistro]  DATETIME       NULL,
    [CFD]            NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_cxc_LlaveCFD] PRIMARY KEY CLUSTERED ([IdOrdenFactura] ASC)
);

