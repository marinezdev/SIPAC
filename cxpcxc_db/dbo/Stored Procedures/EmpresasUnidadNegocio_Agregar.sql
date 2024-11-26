-- =============================================
-- Author:		Jose Luis Villarreal
-- Create date: 22/09/20
-- Description:	Agrega un proyecto a una empresa, impide que se repita un proyecto en una empresa
--				si se intenta agregar de nuevo.
-- =============================================
CREATE PROCEDURE EmpresasUnidadNegocio_Agregar
	-- Add the parameters for the stored procedure here
	@idempresa INT, 
	@idudn INT,
	@activo BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO EmpresasUnidadNegocio VALUES(@idempresa, @idudn, @activo);
END