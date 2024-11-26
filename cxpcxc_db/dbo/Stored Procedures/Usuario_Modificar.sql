-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Usuario_Modificar 
	-- Add the parameters for the stored procedure here
	@clave VARCHAR(64), 
	@nombre VARCHAR(80), 
	@grupo INT, 
	@estado INT, 
	@unidadnegocio VARCHAR(50), 
	@correo VARCHAR(64), 
	@tiporeccorreo INT,
    @idusr INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE usuario SET clave=@clave, nombre=@nombre, grupo=@grupo, estado=@estado, unidadnegocio=@unidadnegocio, correo=@correo, tiporeccorreo=@tiporeccorreo
    WHERE idusr=@idusr
END