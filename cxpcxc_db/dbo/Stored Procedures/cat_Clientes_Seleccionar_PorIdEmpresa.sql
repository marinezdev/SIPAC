-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE cat_Clientes_Seleccionar_PorIdEmpresa 
	-- Add the parameters for the stored procedure here
	@idempresa INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, nombre FROM cat_Clientes WHERE IdEmpresa=@idempresa ORDER BY Nombre
END