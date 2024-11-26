-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Elimina un registro
-- =============================================
CREATE PROCEDURE trf_NotaCredito_Eliminar 
	-- Add the parameters for the stored procedure here
	@idnotacredito INT, 
	@estado INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE trf_notacredito WHERE idnotacredito=@idnotacredito AND estado=@estado
END