-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Selecciona la bitacora por idsolicitud
-- =============================================
CREATE PROCEDURE BitacoraSolicitud_Seleccionar_PorIdSolicitud 
	-- Add the parameters for the stored procedure here
	@idsolicitud INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT idsolicitud, fecharegistro, estado, idusr, nombre, importe  
	FROM BitacoraSolicitud WHERE IdUsr>=0 AND IdSolicitud=@idsolicitud
END