-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Obtiene el numero de comprobante de un archivo en una solicitud de pago
-- =============================================
CREATE PROCEDURE trf_Archivos_Seleccionar_NumeroComprobante 
	-- Add the parameters for the stored procedure here
	@idsolicitud INT,
	@tipo INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT SUM(Cantidad) AS total  FROM trf_Archivos WHERE IdSolicitud=@idsolicitud AND tipo=@tipo
END