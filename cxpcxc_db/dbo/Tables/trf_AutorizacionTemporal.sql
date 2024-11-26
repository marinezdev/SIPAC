CREATE TABLE [dbo].[trf_AutorizacionTemporal] (
    [IdEmpresa]         INT             NOT NULL,
    [IdUsr]             INT             NOT NULL,
    [IdSolicitud]       INT             NOT NULL,
    [ImporteAutorizado] DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_trf_AutorizacionTemporal] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdUsr] ASC, [IdSolicitud] ASC)
);

