-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Selecciona una factura
-- =============================================
CREATE PROCEDURE cxc_Archivos_Seleccionar_Factura 
	-- Add the parameters for the stored procedure here
	@idordenfactura INT,
	@tipo INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idordenfactura, fecharegistro, tipo, iddocumento, archivoorigen, ArchivoDestino, nota  
	FROM cxc_Archivos 
	WHERE IdOrdenFactura=@idordenfactura AND tipo=@tipo
END