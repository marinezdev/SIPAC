CREATE TABLE [dbo].[xx_CatArticulos] (
    [NumeroParte]   VARCHAR (50)    NOT NULL,
    [FechaRegistro] DATETIME        NOT NULL,
    [Descripcion]   VARCHAR (300)   NOT NULL,
    [Precio]        DECIMAL (18, 2) NOT NULL,
    [Estado]        INT             NOT NULL,
    CONSTRAINT [PK_xx_CatArticulos] PRIMARY KEY CLUSTERED ([NumeroParte] ASC)
);

