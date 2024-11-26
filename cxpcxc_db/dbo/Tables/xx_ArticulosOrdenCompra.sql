CREATE TABLE [dbo].[xx_ArticulosOrdenCompra] (
    [IdOrdenCompra] INT             NOT NULL,
    [NumeroParte]   VARCHAR (50)    NOT NULL,
    [Descripcion]   VARCHAR (300)   NOT NULL,
    [Cantidad]      INT             NOT NULL,
    [Precio]        DECIMAL (18, 2) NOT NULL,
    [Nota]          VARCHAR (255)   NOT NULL,
    [Estado]        INT             NOT NULL,
    CONSTRAINT [PK_xx_ArticulosCompra] PRIMARY KEY CLUSTERED ([IdOrdenCompra] ASC, [NumeroParte] ASC)
);

