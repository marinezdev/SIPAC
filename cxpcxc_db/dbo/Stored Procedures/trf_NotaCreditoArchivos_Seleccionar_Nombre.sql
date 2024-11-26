-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Selecciona un registro por nombre
-- =============================================
CREATE PROCEDURE trf_NotaCreditoArchivos_Seleccionar_Nombre 
	-- Add the parameters for the stored procedure here
	@idnotacredito INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT nombre FROM trf_notacreditoarchivos WHERE idnotacredito=@idnotacredito AND tipo=1
END