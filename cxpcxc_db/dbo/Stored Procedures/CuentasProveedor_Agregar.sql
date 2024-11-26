-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Agrega una nueva cuenta
-- =============================================
CREATE PROCEDURE CuentasProveedor_Agregar 
	-- Add the parameters for the stored procedure here
	@banco VARCHAR(32),
	@cuenta VARCHAR(32),
	@clabe VARCHAR(32),
	@sucursal VARCHAR(32),
	@moneda VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO CuentasProveedor (fecharegistro,banco,cuenta,ctaclabe,sucursal,moneda)
    VALUES (getdate(), @banco, @cuenta, @clabe, @sucursal, @moneda)
           
END