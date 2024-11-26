-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 7/10/20
-- Description:	Para recordar como funciona el OUTPUT
-- Ejemplo de uso:
-- DECLARE @contados INT;
-- EXEC usuario_contar @usuarioscontados = @contados OUTPUT
-- SELECT @contados AS 'Usuarios Totales'
-- =============================================
CREATE PROCEDURE Usuario_Contar 
	-- Add the parameters for the stored procedure here
	@usuarioscontados INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM usuario;
	SELECT @usuarioscontados = @@ROWCOUNT;
END