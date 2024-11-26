-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 12/10/20
-- Description:	Agrega un nuevo usuario
-- =============================================
CREATE PROCEDURE [dbo].[Usuario_Agregar]
	-- Add the parameters for the stored procedure here
	@usuario VARCHAR(64),
	@clave VARCHAR(64),
	@nombre VARCHAR(80),
	@unidadnegocio VARCHAR(50),
	@grupo INT,
	@estado INT,
	@conectado VARCHAR(64),
	@idempresa INT,
	@correo VARCHAR(64),
	@tiporeccorreo INT,
	@idobtenido INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION

	BEGIN TRY

		-- Insert statements for procedure here
		DECLARE @idorden INT;
		INSERT INTO usrctrl (fecha) VALUES(GETDATE());
		SET @idorden = @@IDENTITY;
		INSERT INTO usuario (idusr,usuario,clave,nombre,unidadnegocio,grupo,estado,conectado,fecha,idempresa,correo,tiporeccorreo) 
		VALUES(@idorden,@usuario,@clave,@nombre,@unidadnegocio,@grupo,@estado,@conectado,getdate(),@idempresa,@correo,@tiporeccorreo)
		SELECT @idobtenido = @idorden
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	
		ROLLBACK TRANSACTION
	
	END CATCH
END