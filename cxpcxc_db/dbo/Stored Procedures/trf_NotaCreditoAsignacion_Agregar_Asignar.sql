-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 9/10/20
-- Description:	Asigna nota credito
-- =============================================
CREATE PROCEDURE trf_NotaCreditoAsignacion_Agregar_Asignar 
	-- Add the parameters for the stored procedure here
	@idnotacredito INT,
	@idsolicitud INT,
	@monto DECIMAL,
	@idusr INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO trf_NotaCreditoAsignacion (fecharegistro,idnotacredito,idsolicitud,monto,idusr)
    VALUES (getdate(),@idnotacredito,@idsolicitud,@monto,@idusr)
END