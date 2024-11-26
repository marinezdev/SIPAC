-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Agrega un registro
-- =============================================
CREATE PROCEDURE cxc_ArchivoContrato_Agregar 
	-- Add the parameters for the stored procedure here
	@idservicio INT,
	@archivodestino VARCHAR(65)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO cxc_ArchivoContrato (IdServicio,FechaRegistro,ArchivoDestino) 
	VALUES (@idservicio, getdate(), @archivodestino)
            
END