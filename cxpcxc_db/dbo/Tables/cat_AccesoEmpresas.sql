CREATE TABLE [dbo].[cat_AccesoEmpresas] (
    [IdUsuario]     INT      NOT NULL,
    [IdEmpresa]     INT      NOT NULL,
    [FechaRegistro] DATETIME NOT NULL,
    CONSTRAINT [PK_cat_AccesoEmpresas] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [IdEmpresa] ASC)
);

