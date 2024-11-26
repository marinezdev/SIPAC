-- =============================================
-- Author:		José Luis Villarreal Ruiz
-- Create date: 25/09/20
-- Description:	Obtiene un registro por su id
-- =============================================
CREATE PROCEDURE cat_Empresas_Seleccionar_PorId
	-- Add the parameters for the stored procedure here
	@id INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id,fecharegistro,rfc,nombre,activo,logo FROM cat_empresas WHERE id=@id
END