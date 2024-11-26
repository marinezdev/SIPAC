CREATE TABLE [dbo].[trf_SolicitudRechazo] (
    [IdSolicitud] INT           NOT NULL,
    [Motivo]      VARCHAR (100) NULL,
    CONSTRAINT [PK_trf_SolicitudRechazo] PRIMARY KEY CLUSTERED ([IdSolicitud] ASC)
);

