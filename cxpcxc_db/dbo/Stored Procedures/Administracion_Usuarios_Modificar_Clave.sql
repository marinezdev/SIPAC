-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 18/09/20
-- Description:	Actualiza la clave y contraseña del usuario
-- =============================================
CREATE PROCEDURE Administracion_Usuarios_Modificar_Clave 
	-- Add the parameters for the stored procedure here
	@idusuario INT,
	@usuario VARCHAR(64),
	@clave VARCHAR(64)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE usuario SET usuario=@usuario,
    Clave=@clave
    WHERE IdUsr=@idusuario
END