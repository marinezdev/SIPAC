CREATE TABLE [dbo].[trf_SolicitudCtrl] (
    [IdSolicitud] INT      IDENTITY (1, 1) NOT NULL,
    [Fecha]       DATETIME NOT NULL,
    CONSTRAINT [PK_SolicitudCtrl] PRIMARY KEY CLUSTERED ([IdSolicitud] ASC)
);

