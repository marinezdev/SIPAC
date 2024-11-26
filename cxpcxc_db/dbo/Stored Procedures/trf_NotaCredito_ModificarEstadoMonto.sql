-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Modifica el estado y el monto
-- =============================================
CREATE PROCEDURE trf_NotaCredito_ModificarEstadoMonto
	-- Add the parameters for the stored procedure here
	@idnotacredito INT, 
	@estado INT,
	@importe DECIMAL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE trf_notacredito SET estado=@estado, importependiente=@importe
    WHERE idnotacredito=@idnotacredito
END