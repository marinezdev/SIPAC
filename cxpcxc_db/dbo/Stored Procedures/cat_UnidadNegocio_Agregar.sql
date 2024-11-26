-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Agrega un nuevo registro
-- =============================================
CREATE PROCEDURE cat_UnidadNegocio_Agregar 
	-- Add the parameters for the stored procedure here
	@idempresa INT,
	@titulo VARCHAR(150),
	@activo INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO cat_unidadnegocio (idempresa, titulo, fecharegistro, activo)
	VALUES (@idempresa, @titulo, getdate(), @activo)
END