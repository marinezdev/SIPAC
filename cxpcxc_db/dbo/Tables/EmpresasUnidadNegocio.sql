CREATE TABLE [dbo].[EmpresasUnidadNegocio] (
    [IdEmpresa] INT NOT NULL,
    [IdUDN]     INT NOT NULL,
    [Activo]    BIT NOT NULL,
    CONSTRAINT [IX_EmpresasUnidadNegocio] UNIQUE NONCLUSTERED ([IdEmpresa] ASC, [IdUDN] ASC)
);

