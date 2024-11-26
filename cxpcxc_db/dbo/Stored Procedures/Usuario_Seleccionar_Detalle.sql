-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 12/10/20
-- Description:	Obtiene el detalle de un usuario
-- =============================================
CREATE PROCEDURE Usuario_Seleccionar_Detalle
	-- Add the parameters for the stored procedure here
	@idusr INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT us.IdUsr, us.Usuario,us.Clave,us.Nombre,us.UnidadNegocio AS IdUDN,udn.Titulo AS UDN,us.Grupo, 
	CASE 
		WHEN us.grupo = 101 THEN 'SYSTEMALL'
		WHEN us.grupo = 100 THEN 'ADMSYS'
		WHEN us.grupo = 70 THEN 'ADMINISTRADOR'
		WHEN us.grupo = 65 THEN 'PRESIDENCIA'
		WHEN us.grupo = 60 THEN 'DIRECCION'
		WHEN us.grupo = 57 THEN 'SUBDIRECCION'
		WHEN us.grupo = 55 THEN 'COORDINADOR'
		WHEN us.grupo = 50 THEN 'CONTABILIDAD'
		WHEN us.grupo = 40 THEN 'APLICACIONPAGO'
		WHEN us.grupo = 35 THEN 'CONSULTA'
		WHEN us.grupo = 30 THEN 'CAPTURA'
		WHEN us.grupo = 26 THEN 'FACTURACIONSOLICITA'
		WHEN us.grupo = 25 THEN 'FACTURACION'
		WHEN us.grupo = 20 THEN 'AUTORIZACION'
		WHEN us.grupo = 10 THEN 'SOLICITANTE'
	END AS GrupoDescripcion,
	us.Estado,us.Conectado,us.Fecha, us.IdEmpresa, emp.Nombre AS Empresa, us.Correo,us.TipoRecCorreo
	FROM usuario us
	INNER JOIN cat_UnidadNegocio udn ON udn.Id=us.UnidadNegocio
	INNER JOIN cat_Empresas emp ON emp.Id=us.IdEmpresa


END