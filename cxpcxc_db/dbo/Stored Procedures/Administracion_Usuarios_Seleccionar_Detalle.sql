-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 17/09/20
-- Description:	Obtiene el detalle del usuario
-- =============================================
CREATE PROCEDURE [dbo].[Administracion_Usuarios_Seleccionar_Detalle] 
	-- Add the parameters for the stored procedure here
	@usuario VARCHAR(150), 
	@clave VARCHAR(150)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idusr, usuario, clave, nombre, UnidadNegocio, grupo, estado, conectado, fecha, idempresa, correo, TipoRecCorreo 
	FROM usuario 
	WHERE usuario=@usuario 
	AND clave=@clave 
	AND estado=1;
END
