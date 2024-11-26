-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 28/09/20
-- Description:	Obtiene un registro por su id
-- =============================================
CREATE PROCEDURE [cat_Rechazos_Seleccionar_PorId] 
	-- Add the parameters for the stored procedure here
	@id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, idempresa, titulo, fecharegistro, activo 
	FROM cat_rechazos WHERE id=@id
END