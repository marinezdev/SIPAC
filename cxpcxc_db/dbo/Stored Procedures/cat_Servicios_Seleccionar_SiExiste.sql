-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 08/10/20
-- Description:	Valida si existe un registro
-- =============================================
CREATE PROCEDURE cat_Servicios_Seleccionar_SiExiste 
	-- Add the parameters for the stored procedure here
	@idempresa INT, 
	@titulo VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, fecharegistro, idempresa, titulo, imagen, activo FROM cat_Servicios 
	WHERE IdEmpresa=@idempresa AND Titulo=@titulo
END