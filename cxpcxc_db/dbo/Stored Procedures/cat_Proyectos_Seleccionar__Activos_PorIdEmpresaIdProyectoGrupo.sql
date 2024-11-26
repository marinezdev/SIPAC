-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 28/09/20
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE cat_Proyectos_Seleccionar__Activos_PorIdEmpresaIdProyectoGrupo 
	-- Add the parameters for the stored procedure here
	@idempresa INT, 
	@idproyectogrupo INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Titulo 
	FROM Cat_Proyectos 
	WHERE IdEmpresa=@idempresa 
	AND Activo = 1 
	--DEFINIR: AND Cat_Proyectos.Titulo IN (SELECT Proyecto + '...' FROM cat_Proyectos WHERE cat_Proyectos.ProyectoSeg = @idproyectogrupo ORDER BY Titulo
END