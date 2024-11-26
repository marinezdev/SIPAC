-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 9/10/20
-- Description:	Selecciona registro por id
-- =============================================
CREATE PROCEDURE cxc_OrdenServicio_Seleccionar_PorIdServicio 
	-- Add the parameters for the stored procedure here
	@idservicio INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idservicio,fecharegistro,idcliente,rfc,cliente,idempresa,empresa,tiposolicitud,fechainicio,fechatermino,
	importe,periodos,tipoperiodo,condicionpago,condicionpagodias,proyecto,tipomoneda,idcatservicio,servicio,descripcion,
	anotaciones,idusr,unidadnegocio,ctesolomon,contrato,enviacorreoclte,estado,especial
	FROM cxc_OrdenServicio 
	WHERE IdServicio=@idservicio
END