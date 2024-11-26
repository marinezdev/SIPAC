-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Obtiene una lista de comprobantes
-- =============================================
CREATE PROCEDURE trf_Archivos_Seleccionar_ListaComprobantes
	-- Add the parameters for the stored procedure here
	@idsolicitud INT,
	@tipo INT  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idsolicitud, fecharegistro, tipo, iddocumento, archivoorigen, archivodestino, cantidad, tipocambio, pesos, nota, idpago 
	FROM trf_Archivos WHERE IdSolicitud=@idsolicitud AND Tipo=@tipo ORDER BY IdDocumento
END