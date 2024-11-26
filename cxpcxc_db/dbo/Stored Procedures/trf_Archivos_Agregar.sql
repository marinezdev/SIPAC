-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Agrega un registro
-- =============================================
CREATE PROCEDURE trf_Archivos_Agregar 
	-- Add the parameters for the stored procedure here
	@idsolicitud INT,
    @tipo INT,
    @iddocumento INT,
    @archvioorigen VARCHAR(64),
    @archivodestino VARCHAR(64),
    @cantidad DECIMAL,
    @tipocambio DECIMAL,
    @pesos DECIMAL,
    @nota VARCHAR(255),
    @idpago INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO trf_Archivos (IdSolicitud,FechaRegistro,Tipo,IdDocumento,ArchivoOrigen,ArchivoDestino,Cantidad,TipoCambio,Pesos,Nota,IdPago) 
	VALUES (@idsolicitud, getdate(),@tipo,@iddocumento,@archvioorigen,@archivodestino,@cantidad,@tipocambio,@pesos,@nota,@idpago) 	
END