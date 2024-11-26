-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 28/09/20
-- Description:	Verifica si un registro existe
-- =============================================
CREATE PROCEDURE cat_Proyectos_Seleccionar_SiExiste 
	-- Add the parameters for the stored procedure here
	@idempresa INT, 
	@titulo VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, idempresa, titulo, fecharegistro, activo 
	FROM Cat_Proyectos Where IdEmpresa=@idempresa AND Titulo=@titulo
END