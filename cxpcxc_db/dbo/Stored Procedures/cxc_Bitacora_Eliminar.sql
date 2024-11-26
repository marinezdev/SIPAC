-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Elimina un registro por su idordenfactura
-- =============================================
CREATE PROCEDURE cxc_Bitacora_Eliminar 
	-- Add the parameters for the stored procedure here
	@idordenfactura INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE cxc_Bitacora WHERE IdOrdenFactura=@idordenfactura
END