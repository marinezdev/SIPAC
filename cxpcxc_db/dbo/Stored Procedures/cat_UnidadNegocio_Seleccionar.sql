-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 08/10/20
-- Description:	Selecciona todos los registros
-- =============================================
CREATE PROCEDURE cat_UnidadNegocio_Seleccionar 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, idempresa, titulo, fecharegistro, activo FROM cat_UnidadNegocio
END