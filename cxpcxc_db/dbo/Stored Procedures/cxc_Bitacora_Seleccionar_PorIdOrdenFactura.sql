-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 08/10/20
-- Description:	Obtiene el seguimiento de la bitácora
-- =============================================
CREATE PROCEDURE cxc_Bitacora_Seleccionar_PorIdOrdenFactura
	-- Add the parameters for the stored procedure here
	@idsolicitud INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idservicio, idordenfactura, fecharegistro, estado, idusr, nombre 
	FROM cxc_Bitacora 
	WHERE IdUsr>=0 AND IdOrdenFactura=@idsolicitud
END