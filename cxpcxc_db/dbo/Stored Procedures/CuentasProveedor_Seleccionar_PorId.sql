-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Seecciona las cuentas por proveedor
-- =============================================
CREATE PROCEDURE CuentasProveedor_Seleccionar_PorId 
	-- Add the parameters for the stored procedure here
	@id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, fecharegistro, banco, cuenta, ctaclabe, sucursal, moneda  
	FROM CuentasProveedor 
	WHERE Id=@id
END