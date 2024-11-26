CREATE TABLE [dbo].[trf_BitacoraEventos] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [IdSolicitud]   INT           NOT NULL,
    [FechaRegistro] DATETIME      NOT NULL,
    [IdUsr]         INT           NOT NULL,
    [Nombre]        VARCHAR (80)  NOT NULL,
    [Descripcion]   VARCHAR (128) NOT NULL,
    CONSTRAINT [PK_trf_BitacoraEventos] PRIMARY KEY CLUSTERED ([Id] ASC)
);

