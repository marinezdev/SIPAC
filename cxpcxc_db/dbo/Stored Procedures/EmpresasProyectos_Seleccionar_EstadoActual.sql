-- =============================================
-- Author:		José Luis Villarreal Ruiz
-- Create date: 22/09/20
-- Description:	Obtiene el estado actual del registro
-- =============================================
CREATE PROCEDURE [dbo].[EmpresasProyectos_Seleccionar_EstadoActual] 
	-- Add the parameters for the stored procedure here
	@idempresa INT,
	@idproyecto INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT activo FROM EmpresasProyectos WHERE idempresa=@idempresa AND idproyecto=@idproyecto
END