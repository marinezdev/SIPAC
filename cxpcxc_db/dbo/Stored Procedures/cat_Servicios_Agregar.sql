-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Agrega un registro
-- =============================================
CREATE PROCEDURE cat_Servicios_Agregar 
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
	INSERT INTO cat_Servicios (FechaRegistro, IdEmpresa,Titulo,Activo) 
	VALUES (getdate(), @idempresa, @titulo, @activo)

END