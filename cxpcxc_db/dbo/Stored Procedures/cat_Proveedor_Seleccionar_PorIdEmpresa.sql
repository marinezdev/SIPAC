-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 25/09/20
-- Description:	Obtiene todos los registros por empresa
-- =============================================
CREATE PROCEDURE cat_Proveedor_Seleccionar_PorIdEmpresa
	@idempresa INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id,fecharegistro,idempresa,nombre,rfc,contacto,correo,telefono,extencion,sinfactura,direccion,ciudad,estado,cp,activo 
	FROM cat_Proveedor WHERE IdEmpresa=@idempresa 
	ORDER BY Nombre
END