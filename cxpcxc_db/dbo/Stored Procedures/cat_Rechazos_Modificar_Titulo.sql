-- =============================================
-- Author:		José Luis Villarreal Ruiz
-- Create date: 28/09/20
-- Description:	Modifica el título de un registro
-- =============================================
CREATE PROCEDURE cat_Rechazos_Modificar_Titulo 
	-- Add the parameters for the stored procedure here
	@id INT, 
	@titulo VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE cat_rechazos SET titulo=@titulo WHERE id=@id
END