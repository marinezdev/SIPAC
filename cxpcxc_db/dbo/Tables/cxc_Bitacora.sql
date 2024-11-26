CREATE TABLE [dbo].[cxc_Bitacora] (
    [IdServicio]     INT          NOT NULL,
    [IdOrdenFactura] INT          NOT NULL,
    [FechaRegistro]  DATETIME     NOT NULL,
    [Estado]         INT          NOT NULL,
    [IdUsr]          INT          NOT NULL,
    [Nombre]         VARCHAR (80) NOT NULL,
    CONSTRAINT [PK_cxc_Bitacora] PRIMARY KEY CLUSTERED ([IdServicio] ASC, [IdOrdenFactura] ASC, [FechaRegistro] ASC, [Estado] ASC)
);

