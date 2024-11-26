-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Selecciona notas de credito del proveedor
-- =============================================
CREATE PROCEDURE [trf_NotaCredito_Seleccionar_NotasCreditoProveedor]
	-- Add the parameters for the stored procedure here
	@idempresa INT, 
	@rfc VARCHAR(16),
	@estado INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idnotacredito, fecharegistro, idempresa, fecha, rfc, proveedor, descripcion, importe, moneda, 
	importependiente, estado, idusr, idsolicitudorigen   
	FROM trf_NotaCredito 
	WHERE idempresa=@idempresa
	AND rfc=@rfc
    AND estado=@estado
    ORDER BY idnotacredito
END