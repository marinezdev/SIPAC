-- =============================================
-- Author:		José Luis Villarreal Ruiz
-- Create date: 28/09/20
-- Description:	Selecciona todos los registros
-- =============================================
CREATE PROCEDURE cat_Proyectos_Seleccionar
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, idempresa, titulo, fecharegistro, activo FROM cat_Proyectos
END