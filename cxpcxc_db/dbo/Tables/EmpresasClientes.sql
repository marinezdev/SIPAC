CREATE TABLE [dbo].[EmpresasClientes] (
    [IdEmpresa] INT NOT NULL,
    [IdCliente] INT NOT NULL,
    [Activo]    BIT NOT NULL,
    CONSTRAINT [IX_EmpresasClientes] UNIQUE NONCLUSTERED ([IdEmpresa] ASC, [IdCliente] ASC)
);

