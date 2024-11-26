-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Elimina una cuenta
-- =============================================
CREATE PROCEDURE CuentasProveedor_Eliminar 
	-- Add the parameters for the stored procedure here
	@id INT,
	@cuenta VARCHAR(32)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM cuentasproveedor WHERE id=@id AND cuenta=@cuenta
END