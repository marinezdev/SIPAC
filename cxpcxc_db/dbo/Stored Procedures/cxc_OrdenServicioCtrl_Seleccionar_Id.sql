-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 13/10/20
-- Description:	Obtiene el id de control de agregado de registro 
-- =============================================
CREATE PROCEDURE cxc_OrdenServicioCtrl_Seleccionar_Id
	-- Add the parameters for the stored procedure here
	@idobtenido INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO cxc_OrdenServicioCtrl(Fecha) VALUES(getdate())
	SET @idobtenido=@@IDENTITY
END