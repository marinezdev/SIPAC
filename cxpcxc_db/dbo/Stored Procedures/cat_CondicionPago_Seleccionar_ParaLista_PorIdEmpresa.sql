-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Selecciona un listado por empresa para mostrar en una lista desplegable
-- =============================================
CREATE PROCEDURE cat_CondicionPago_Seleccionar_ParaLista_PorIdEmpresa 
	-- Add the parameters for the stored procedure here
	@idempresa INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, Titulo 
	FROM cat_CondicionPago 
	WHERE IdEmpresa=@idempresa 
	AND Activo=1 
	ORDER BY Titulo
END