-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 10/10/20
-- Description:	Obtiene el nombre de la moneda
-- =============================================
CREATE PROCEDURE cat_Moneda_Seleccionar_Nombre_PorAbreviacion 
	-- Add the parameters for the stored procedure here
	@abreviacion NVARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT nombre FROM cat_Moneda WHERE abreviacion=@abreviacion
END