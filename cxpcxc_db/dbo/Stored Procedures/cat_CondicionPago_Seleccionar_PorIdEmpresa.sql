-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Seleccionar todas las condiciones por empresa
-- =============================================
CREATE PROCEDURE cat_CondicionPago_Seleccionar_PorIdEmpresa
	-- Add the parameters for the stored procedure here
	@idempresa INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, FechaRegistro, idempresa, titulo, dias, activo 
	FROM cat_CondicionPago WHERE IdEmpresa=@idempresa
END