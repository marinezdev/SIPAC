-- =============================================
-- Author:		Jose Luis Villarreal Ruiz	
-- Create date: 18/09/20
-- Description:	Actualiza el estado del registro de activo o inactivo, sólo el estado
-- =============================================
CREATE PROCEDURE EmpresasClientes_Modificar 
	-- Add the parameters for the stored procedure here
	@idempresa INT,
	@idcliente INT,
	@activo BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE EmpresasClientes SET activo=@activo WHERE idempresa=@idempresa AND idcliente=@idcliente
END
