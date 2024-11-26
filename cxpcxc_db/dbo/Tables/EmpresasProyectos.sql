CREATE TABLE [dbo].[EmpresasProyectos] (
    [IdEmpresa]  INT NOT NULL,
    [IdProyecto] INT NOT NULL,
    [Activo]     BIT NOT NULL,
    CONSTRAINT [IX_EmpresasProyectos] UNIQUE NONCLUSTERED ([IdEmpresa] ASC, [IdProyecto] ASC)
);

