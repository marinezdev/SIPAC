-- =============================================
-- Author:		José Luis Villarreal Ruiz
-- Create date: 25/09/20
-- Description:	Selecciona registros con o sin factura por empresa
-- =============================================
CREATE PROCEDURE cat_Proveedor_Seleccionar_ConSinFactura_PorEmpresa 
	-- Add the parameters for the stored procedure here
	@idempresa INT, 
	@confactura INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id,fecharegistro,idempresa,nombre,rfc,contacto,correo,telefono,extencion,sinfactura,direccion,ciudad,estado,cp,activo  
	FROM cat_Proveedor WHERE IdEmpresa=@idempresa
	AND SinFactura=@confactura 
	ORDER BY Nombre
END