CREATE TABLE [dbo].[trf_SolicitudLlave] (
    [IdSolicitud]   INT            NOT NULL,
    [FechaRegistro] DATETIME       NULL,
    [Llave]         NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_trf_SolicitudLlave] PRIMARY KEY CLUSTERED ([IdSolicitud] ASC)
);

