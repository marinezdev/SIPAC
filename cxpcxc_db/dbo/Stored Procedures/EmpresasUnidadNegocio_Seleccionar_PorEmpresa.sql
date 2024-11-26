-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 22/09/20
-- Description:	Selecciona la relación de empresas y unidades de negocio
-- =============================================
CREATE PROCEDURE EmpresasUnidadNegocio_Seleccionar_PorEmpresa
	-- Add the parameters for the stored procedure here
	@idempresa INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT emp.Nombre AS Empresa, udn.Titulo AS Titulo, eudn.idempresa,eudn.IdUDN,eudn.activo
	FROM EmpresasUnidadNegocio eudn
	INNER JOIN cat_empresas emp ON emp.Id = eudn.IdEmpresa
	INNER JOIN cat_UnidadNegocio udn ON udn.id=eudn.IdUDN
	WHERE eudn.IdEmpresa=@idempresa
END