-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Modifica el titulo 
-- =============================================
CREATE PROCEDURE cat_UnidadNegocio_Modificar_Titulo
	-- Add the parameters for the stored procedure here
	@id INT, 
	@titulo VARCHAR(150)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE cat_unidadnegocio SET titulo=@titulo WHERE id=@id
END