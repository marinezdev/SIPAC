-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 11/10/20
-- Description:	Obtiene todos los registros
-- =============================================
CREATE PROCEDURE [dbo].[cat_Moneda_Seleccionar]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT abreviacion,nombre FROM cat_Moneda
END