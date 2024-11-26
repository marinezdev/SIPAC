-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Agrega un registro
-- =============================================
CREATE PROCEDURE [BitacoraSolicitud_Agregar] 
	-- Add the parameters for the stored procedure here
	@idsolicitud INT,
	@estado INT,
	@idusr INT,
	@nombre VARCHAR(80),
	@importe DECIMAL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO BitacoraSolicitud (IdSolicitud,FechaRegistro,Estado,IdUsr,Nombre,Importe)
    VALUES (@idsolicitud,getdate(),@estado,@idusr,@nombre,@importe)
END