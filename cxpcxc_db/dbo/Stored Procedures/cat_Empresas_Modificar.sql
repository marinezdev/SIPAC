-- =============================================
-- Author:		José Luis Villarreal Ruiz
-- Create date: 25/09/20
-- Description:	Modifica el registro
-- =============================================
CREATE PROCEDURE cat_Empresas_Modificar 
	-- Add the parameters for the stored procedure here
	@id INT,
	@rfc VARCHAR(16),
	@nombre VARCHAR(150),
	@activo INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE cat_Empresas SET Rfc=@rfc,Nombre=@nombre,Activo=@activo
    WHERE Id=@id
END