-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 25/09/20
-- Description:	Valida si un registro existe
-- =============================================
CREATE PROCEDURE cat_Proveedor_Seleccionar_SiExiste 
	-- Add the parameters for the stored procedure here
	@idempresa INT, 
	@rfc VARCHAR(16)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 1  
	FROM cat_Proveedor WHERE IdEmpresa=@idempresa AND Rfc=@rfc
END