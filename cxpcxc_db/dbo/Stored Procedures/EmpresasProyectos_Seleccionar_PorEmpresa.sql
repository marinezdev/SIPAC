-- =============================================
-- Author:		José Luis Villarreal Ruiz
-- Create date: 22/09/20
-- Description:	Selecciona la relación de empresas y clientes
-- =============================================
CREATE PROCEDURE [dbo].[EmpresasProyectos_Seleccionar_PorEmpresa] 
	-- Add the parameters for the stored procedure here
	@idempresa INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT emp.Nombre AS Empresa, pro.Titulo AS Titulo, ep.idempresa,ep.idproyecto,ep.activo
	FROM EmpresasProyectos ep
	INNER JOIN cat_empresas emp ON emp.Id = ep.IdEmpresa
	INNER JOIN cat_proyectos pro ON pro.id=ep.idproyecto
	WHERE ep.IdEmpresa=@idempresa
END