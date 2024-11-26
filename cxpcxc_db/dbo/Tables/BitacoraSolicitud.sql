CREATE TABLE [dbo].[BitacoraSolicitud] (
    [IdSolicitud]   INT             NOT NULL,
    [FechaRegistro] DATETIME        NOT NULL,
    [Estado]        INT             NOT NULL,
    [IdUsr]         INT             NOT NULL,
    [Nombre]        VARCHAR (80)    NOT NULL,
    [Importe]       DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_BitacoraSolicitud] PRIMARY KEY CLUSTERED ([IdSolicitud] ASC, [FechaRegistro] ASC, [Estado] ASC)
);

