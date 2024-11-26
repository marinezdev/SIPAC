-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 08/10/20
-- Description:	Modificar el titulo del registro
-- =============================================
CREATE PROCEDURE cat_Servicios_Modificar_Titulo 
	-- Add the parameters for the stored procedure here
	@id  INT,
	@titulo VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE cat_Servicios SET Titulo=@titulo WHERE Id=@id
END