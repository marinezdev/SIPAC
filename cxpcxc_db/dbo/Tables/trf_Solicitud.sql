﻿CREATE TABLE [dbo].[trf_Solicitud] (
    [IdSolicitud]   INT             NOT NULL,
    [FechaRegistro] DATETIME        NOT NULL,
    [IdEmpresa]     INT             NULL,
    [Factura]       VARCHAR (32)    NULL,
    [FechaFactura]  DATETIME        NULL,
    [CondicionPago] VARCHAR (64)    NULL,
    [Concepto]      VARCHAR (350)   NULL,
    [Importe]       DECIMAL (18, 2) NULL,
    [ImporteLetra]  VARCHAR (128)   NULL,
    [IdProveedor]   INT             NULL,
    [Proveedor]     VARCHAR (128)   NULL,
    [Rfc]           VARCHAR (20)    NULL,
    [Banco]         VARCHAR (32)    NULL,
    [Cuenta]        VARCHAR (32)    NULL,
    [CtaClabe]      VARCHAR (32)    NULL,
    [Sucursal]      VARCHAR (32)    NULL,
    [Proyecto]      VARCHAR (50)    NULL,
    [DescProyecto]  VARCHAR (128)   NULL,
    [Moneda]        VARCHAR (8)     NULL,
    [Estado]        INT             NULL,
    [ConFactura]    INT             NULL,
    [IdUsr]         INT             NULL,
    [UnidadNegocio] INT             NULL,
    [CantidadPagar] DECIMAL (18, 2) NULL,
    [Marcado]       INT             NULL,
    [Prioridad]     INT             NULL,
    [TipoSolicitud] INT             NULL,
    [ReporteIva]    INT             NULL,
    CONSTRAINT [PK_Solicitud] PRIMARY KEY CLUSTERED ([IdSolicitud] ASC)
);

