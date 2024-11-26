-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 25/09/20
-- Description:	Valida si un registro existe por rfc y nombre
-- =============================================
CREATE PROCEDURE cat_Empresas_Seleccionar_Validar_PorRFCNombre 
	-- Add the parameters for the stored procedure here
	@rfc VARCHAR(16),
	@nombre VARCHAR(150)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM cat_Empresas WHERE Rfc=@rfc AND Nombre=@nombre
END