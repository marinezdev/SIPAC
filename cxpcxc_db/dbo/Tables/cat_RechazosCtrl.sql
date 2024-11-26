CREATE TABLE [dbo].[cat_RechazosCtrl] (
    [Id]    INT      IDENTITY (1, 1) NOT NULL,
    [Fecha] DATETIME NOT NULL,
    CONSTRAINT [PK_catRechazos] PRIMARY KEY CLUSTERED ([Id] ASC)
);

