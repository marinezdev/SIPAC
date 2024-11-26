-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Seleccionar imagen por id
-- =============================================
CREATE PROCEDURE cat_Servicios_Seleccionar_Imagen_PorId 
	-- Add the parameters for the stored procedure here
	@id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT imagen FROM cat_Servicios WHERE Id=@id
END