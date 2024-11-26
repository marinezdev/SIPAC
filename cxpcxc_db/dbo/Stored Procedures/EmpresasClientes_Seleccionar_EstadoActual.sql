-- =============================================
-- Author:		José Luis Villarreal Ruiz
-- Create date: 18/09/20
-- Description:	Obtiene el estado actual del registro
-- =============================================
CREATE PROCEDURE EmpresasClientes_Seleccionar_EstadoActual 
	-- Add the parameters for the stored procedure here
	@idempresa INT,
	@idcliente INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT activo FROM EmpresasClientes WHERE idempresa=@idempresa AND idcliente=@idcliente
END
