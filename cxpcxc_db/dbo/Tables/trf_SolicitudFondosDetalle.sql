CREATE TABLE [dbo].[trf_SolicitudFondosDetalle] (
    [IdFondeo]          INT             NOT NULL,
    [IdSolicitud]       INT             NOT NULL,
    [ImporteAutorizado] DECIMAL (18, 2) NOT NULL,
    [ImporteFondos]     DECIMAL (18, 2) NOT NULL,
    [Estado]            INT             NOT NULL,
    [Marcado]           INT             NULL,
    [Idpago]            INT             NULL,
    CONSTRAINT [PK_trf_SolDetalleFondos] PRIMARY KEY CLUSTERED ([IdFondeo] ASC, [IdSolicitud] ASC)
);

