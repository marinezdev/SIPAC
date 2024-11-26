-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 17/09/20
-- Description:	Selecciona la relación de empresas y clientes
-- =============================================
CREATE PROCEDURE [dbo].[EmpresasClientes_Seleccionar_PorEmpresa] 
	-- Add the parameters for the stored procedure here
	@idempresa INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT emp.Nombre AS Empresa, cli.Nombre AS Cliente, ec.idempresa,ec.idcliente,ec.activo
	FROM EmpresasClientes ec
	INNER JOIN cat_empresas emp ON emp.Id = ec.IdEmpresa
	INNER JOIN cat_clientes cli ON cli.Id=ec.IdCliente
	WHERE ec.IdEmpresa=@idempresa
END
