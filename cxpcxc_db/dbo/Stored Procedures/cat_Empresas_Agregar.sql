-- =============================================
-- Author:		José Luis Villarreal Ruiz
-- Create date: 25/09/20
-- Description:	Agrega un nuevo registro
-- =============================================
CREATE PROCEDURE cat_Empresas_Agregar 
	-- Add the parameters for the stored procedure here
	@rfc VARCHAR(16),
	@nombre VARCHAR(150)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO cat_Empresas (fecharegistro,rfc,nombre,activo)
    VALUES (getdate(),@rfc,@nombre,1)
END