-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Selecciona todos los registros
-- =============================================
CREATE PROCEDURE [cxc_ArchivoContrato_Seleccionar_PorIdServicio] 
	-- Add the parameters for the stored procedure here
	@idservicio INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idservicio, fecharegistro, archivodestino FROM cxc_ArchivoContrato  WHERE IdServicio=@idservicio
END