-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Agrega el registro de la orden de pago
-- =============================================
CREATE PROCEDURE cxc_Bitacora_Agregar_RegistroPago 
	-- Add the parameters for the stored procedure here
	@idservicio INT, 
	@idordenfactura INT, 
	@estado INT,
	@idusr INT,
	@nombre VARCHAR(80)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO cxc_Bitacora (idservicio,idordenfactura,fecharegistro,estado,idusr,nombre)
    VALUES (@idservicio,@idordenfactura, getdate(),@estado, @idusr,@nombre);
END