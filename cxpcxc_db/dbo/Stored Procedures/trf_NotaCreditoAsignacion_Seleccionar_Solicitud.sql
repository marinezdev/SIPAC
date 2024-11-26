﻿-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 9/10/20
-- Description:	Solicitudes
-- =============================================
CREATE PROCEDURE trf_NotaCreditoAsignacion_Seleccionar_Solicitud 
	-- Add the parameters for the stored procedure here
	@idsolicitud INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT na.fecharegistro, na.idnotacredito, na.idsolicitud, na.monto, na.idusr,
	nc.fecha,nc.descripcion,nc.importe  
	FROM trf_notacreditoasignacion na 
	INNER JOIN trf_notacredito nc ON nc.idnotacredito=na.idnotacredito 
    WHERE na.idsolicitud=@idsolicitud
END