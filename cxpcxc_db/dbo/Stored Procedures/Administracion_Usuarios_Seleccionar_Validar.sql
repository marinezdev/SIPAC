-- =============================================
-- Author:		Jose Luis Villarreal	
-- Create date: 17/09/2020
-- Description:	Valida si la clae y contraseña del usuario existen en el sistema y si esta activo.
-- =============================================
CREATE PROCEDURE Administracion_Usuarios_Seleccionar_Validar 
	-- Add the parameters for the stored procedure here
	@usuario VARCHAR(150), 
	@clave VARCHAR(150)
AS
BEGIN
	IF EXISTS(SELECT usuario FROM usuario WHERE usuario=@usuario AND Clave=@clave AND Estado=1)
		SELECT 1;
	ELSE
		SELECT 0;
END
