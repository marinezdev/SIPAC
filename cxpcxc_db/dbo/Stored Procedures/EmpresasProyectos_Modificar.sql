-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 22/09/20
-- Description:	Actualiza el estado del registro de activo o inactivo, sólo el estado
-- =============================================
CREATE PROCEDURE [dbo].[EmpresasProyectos_Modificar] 
	-- Add the parameters for the stored procedure here
	@idempresa INT,
	@idproyecto INT,
	@activo BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE EmpresasProyectos SET activo=@activo WHERE idempresa=@idempresa AND idproyecto=@idproyecto
END