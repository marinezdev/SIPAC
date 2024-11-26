-- =============================================
-- Author:		Jose Luis Villarreal 
-- Create date: 17/09/20
-- Description:	Agrega un cliente a una empresa, impide que se repita un cliente en una empresa
--				si se intenta agregar de nuevo.
-- =============================================
CREATE PROCEDURE EmpresasClientes_Agregar
	@idempresa INT, 
	@idcliente INT,
	@activo BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO EmpresasClientes VALUES(@idempresa, @idcliente, @activo);
END
