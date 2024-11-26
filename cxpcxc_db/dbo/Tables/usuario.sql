CREATE TABLE [dbo].[usuario] (
    [IdUsr]         INT          NOT NULL,
    [Usuario]       VARCHAR (64) NULL,
    [Clave]         VARCHAR (64) NULL,
    [Nombre]        VARCHAR (80) NULL,
    [UnidadNegocio] VARCHAR (50) NULL,
    [Grupo]         INT          NULL,
    [Estado]        INT          NULL,
    [Conectado]     VARCHAR (64) NULL,
    [Fecha]         DATETIME     NULL,
    [IdEmpresa]     INT          NULL,
    [correo]        VARCHAR (64) NULL,
    [TipoRecCorreo] INT          NULL,
    CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED ([IdUsr] ASC)
);

