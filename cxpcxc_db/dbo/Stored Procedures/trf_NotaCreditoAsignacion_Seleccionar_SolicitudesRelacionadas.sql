-- =============================================
-- Author:		Jose Luis Villarrea Ruiz
-- Create date: 9/10/20
-- Description:	Selecciona solicitudes relacionadas
-- =============================================
CREATE PROCEDURE trf_NotaCreditoAsignacion_Seleccionar_SolicitudesRelacionadas 
	-- Add the parameters for the stored procedure here
	@idnotacredito INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT s.idsolicitud,s.fecharegistro,s.idempresa,s.factura,s.fechafactura,s.condicionpago,s.concepto,s.importe,
	s.importeletra,s.idproveedor,s.proveedor,s.rfc,s.banco,s.cuenta,s.ctaclabe,s.sucursal,s.proyecto,s.descproyecto,
	s.moneda,s.estado,s.confactura,s.idusr,s.unidadnegocio,s.cantidadpagar,s.marcado,s.prioridad,s.tiposolicitud,
	s.reporteiva 
	FROM trf_notacreditoasignacion AS nc 
	INNER JOIN trf_solicitud  AS s ON s.idsolicitud = nc.idsolicitud 
    WHERE idnotacredito=@idnotacredito
END