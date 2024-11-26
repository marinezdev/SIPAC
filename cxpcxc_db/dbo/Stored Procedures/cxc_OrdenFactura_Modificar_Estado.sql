-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 9/10/20
-- Description:	Cambia el estado
-- =============================================
CREATE PROCEDURE cxc_OrdenFactura_Modificar_Estado 
	-- Add the parameters for the stored procedure here
	@idordenfactura INT,
	@estado INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE cxc_ordenfactura SET estado=@estado WHERE idordenfactura=@idordenfactura
END