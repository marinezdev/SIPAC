-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Valida si existe un registro
-- =============================================
CREATE PROCEDURE cat_UnidadNegocio_Validar_SiExiste 
	-- Add the parameters for the stored procedure here
	@idempresa INT, 
	@titulo VARCHAR(150)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM cat_UnidadNegocio WHERE IdEmpresa=@idempresa AND Titulo=@titulo
END