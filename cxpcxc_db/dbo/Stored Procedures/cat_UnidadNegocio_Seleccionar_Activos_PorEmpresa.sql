-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 08/10/20
-- Description:	Selecciona registros para mostrar en una lista desplegable
-- =============================================
CREATE PROCEDURE cat_UnidadNegocio_Seleccionar_Activos_PorEmpresa
	-- Add the parameters for the stored procedure here
	@idempresa INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, titulo FROM cat_unidadnegocio WHERE idempresa=@idempresa
END