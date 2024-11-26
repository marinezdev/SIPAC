-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Agregar registro
-- =============================================
CREATE PROCEDURE trf_BitacoraEventos_Agregar 
	-- Add the parameters for the stored procedure here
	@idsolicitud INT,
	@idusr INT,
	@nombre VARCHAR(80),
	@descripcion VARCHAR(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO trf_BitacoraEventos (IdSolicitud,FechaRegistro,IdUsr,Nombre,Descripcion)
    VALUES (@idsolicitud, getdate(), @idusr, @nombre, @descripcion)
END