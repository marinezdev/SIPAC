-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Selecciona un archivo
-- =============================================
CREATE PROCEDURE [dbo].[trf_NotaCreditoArchivos_Seleccionar_PorIdNotaCreditoTipo] 
	-- Add the parameters for the stored procedure here
	@idnotacredito INT, 
	@tipo INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idnotacredito, tipo, nombre 
	FROM trf_notacreditoarchivos 
	WHERE idnotacredito=@idnotacredito
    AND tipo=@tipo
END