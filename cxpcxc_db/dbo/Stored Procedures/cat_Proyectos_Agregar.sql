-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 28/09/20
-- Description:	Agrega un nuevo registro
-- =============================================
CREATE PROCEDURE cat_Proyectos_Agregar 
	-- Add the parameters for the stored procedure here
	@idempresa INT, 
	@titulo VARCHAR(100),
	@activo INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO cat_proyectos (idempresa, fecharegistro, titulo, activo)
	VALUES (@idempresa, getdate(), @titulo, @activo)
END