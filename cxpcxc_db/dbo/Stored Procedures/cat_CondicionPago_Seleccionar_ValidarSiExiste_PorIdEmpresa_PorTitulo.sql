-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Valida si existe un registro
-- =============================================
CREATE PROCEDURE cat_CondicionPago_Seleccionar_ValidarSiExiste_PorIdEmpresa_PorTitulo
	-- Add the parameters for the stored procedure here
	@idempresa INT,
	@titulo VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * 
	FROM cat_CondicionPago 
	WHERE IdEmpresa=@idempresa AND titulo=@titulo
END