CREATE TABLE [dbo].[trf_SolicitudFondos] (
    [IdFondeo]           INT             NOT NULL,
    [FechaRegistro]      DATETIME        NOT NULL,
    [IdEmpresa]          INT             NOT NULL,
    [Empresa]            VARCHAR (150)   NOT NULL,
    [NoSolicitudes]      INT             NOT NULL,
    [ImporteMx]          DECIMAL (18, 2) NOT NULL,
    [ImporteDlls]        DECIMAL (18, 2) NOT NULL,
    [TipoCambio]         DECIMAL (18, 2) NOT NULL,
    [Total]              DECIMAL (18, 2) NOT NULL,
    [FechaFondos]        DATETIME        NULL,
    [NoSolicitudesAprob] INT             NULL,
    [TotalAprob]         DECIMAL (18, 2) NULL,
    [IdUsr]              INT             NOT NULL,
    [Estado]             INT             NOT NULL,
    [IdUsrFondos]        INT             NULL,
    CONSTRAINT [PK_trf_SolicitudFondos] PRIMARY KEY CLUSTERED ([IdFondeo] ASC)
);

