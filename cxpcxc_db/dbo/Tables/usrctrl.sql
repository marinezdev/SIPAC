CREATE TABLE [dbo].[usrctrl] (
    [IdUsr] INT      IDENTITY (1, 1) NOT NULL,
    [Fecha] DATETIME NOT NULL,
    CONSTRAINT [PK_usrctrl] PRIMARY KEY CLUSTERED ([IdUsr] ASC)
);

