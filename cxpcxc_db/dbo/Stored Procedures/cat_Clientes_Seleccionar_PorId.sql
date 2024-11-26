-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 9/10/20
-- Description:	Obtiene un registro por su id
-- =============================================
CREATE PROCEDURE cat_Clientes_Seleccionar_PorId
	-- Add the parameters for the stored procedure here
	@id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, fecharegistro, idempresa, nombre, rfc, direccion, ciudad,estado, cp, contactoproy, contactofact,
	correo, telefono, extencion, clientesolomon, activo
	FROM cat_clientes WHERE id=@id
END