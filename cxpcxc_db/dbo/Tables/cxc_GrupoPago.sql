CREATE TABLE [dbo].[cxc_GrupoPago] (
    [Idgrupo]        INT NOT NULL,
    [IdOrdenFactura] INT NOT NULL,
    CONSTRAINT [PK_cxc_GrupoPago] PRIMARY KEY CLUSTERED ([Idgrupo] ASC, [IdOrdenFactura] ASC)
);

