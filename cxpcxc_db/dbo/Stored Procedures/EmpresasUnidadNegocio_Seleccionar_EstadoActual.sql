-- =============================================
-- Author:		José Luis Villarreal Ruiz
-- Create date: 22/09/20
-- Description:	Obtiene el estado actual del registro
-- =============================================
CREATE PROCEDURE EmpresasUnidadNegocio_Seleccionar_EstadoActual
	-- Add the parameters for the stored procedure here
	@idempresa INT,
	@idudn INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT activo FROM EmpresasUnidadNegocio WHERE idempresa=@idempresa AND idudn=@idudn
END