-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Modifica el registro
-- =============================================
CREATE PROCEDURE cxc_ArchivoContrato_Modificar
	-- Add the parameters for the stored procedure here
	@idservicio INT,
	@archivodestino VARCHAR(65)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE cxc_ArchivoContrato SET ArchivoDestino=@archivodestino WHERE IdServicio=@idservicio
END