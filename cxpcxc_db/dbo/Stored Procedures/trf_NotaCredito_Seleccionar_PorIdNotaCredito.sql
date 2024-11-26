-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Seleccionar registro
-- =============================================
CREATE PROCEDURE trf_NotaCredito_Seleccionar_PorIdNotaCredito 
	-- Add the parameters for the stored procedure here
	@idnotacredito INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idnotacredito, fecharegistro, idempresa, fecha, rfc, proveedor, descripcion, importe, moneda, 
	importependiente, estado, idusr, idsolicitudorigen 
	FROM trf_NotaCredito 
	WHERE IdNotaCredito=@idnotacredito
END