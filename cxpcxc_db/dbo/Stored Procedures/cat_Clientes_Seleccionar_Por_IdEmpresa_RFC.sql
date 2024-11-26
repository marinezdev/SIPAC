-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 10/10/20
-- Description:	Selecciona el detalle de cliente por la empresa a la que pertenece y su rfc
-- =============================================
CREATE PROCEDURE cat_Clientes_Seleccionar_Por_IdEmpresa_RFC 
	-- Add the parameters for the stored procedure here
	@idempresa INT, 
	@rfc VARCHAR(16)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, fecharegistro, idempresa, nombre, rfc, direccion, ciudad,estado, cp, contactoproy, contactofact,
	correo, telefono, extencion, clientesolomon, activo 
	FROM cat_Clientes 
	WHERE idempresa=@idempresa 
	AND  rfc=@rfc
END