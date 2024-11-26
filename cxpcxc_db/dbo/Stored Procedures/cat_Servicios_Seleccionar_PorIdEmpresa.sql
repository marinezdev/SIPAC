-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 08/10/20
-- Description:	Obtiene los registros por empresa
-- =============================================
CREATE PROCEDURE cat_Servicios_Seleccionar_PorIdEmpresa
	-- Add the parameters for the stored procedure here
	@idempresa INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, fecharegistro, idempresa, titulo, imagen, activo FROM cat_Servicios where IdEmpresa=@idempresa
END