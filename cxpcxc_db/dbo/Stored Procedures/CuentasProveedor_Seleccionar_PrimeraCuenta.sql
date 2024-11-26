-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Obtiene la primera cuenta del proveedor
-- =============================================
CREATE PROCEDURE CuentasProveedor_Seleccionar_PrimeraCuenta 
	-- Add the parameters for the stored procedure here
	@id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 id, fecharegistro, banco, cuenta, ctaclabe, sucursal, moneda   
	FROM CuentasProveedor 
	where Id=@id
END