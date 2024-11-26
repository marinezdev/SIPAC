-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 25/09/20
-- Description:	Selecciona todos los registros
-- =============================================
CREATE PROCEDURE cat_Empresas_Seleccionar 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id,fecharegistro,rfc,nombre,activo,logo FROM cat_empresas
END