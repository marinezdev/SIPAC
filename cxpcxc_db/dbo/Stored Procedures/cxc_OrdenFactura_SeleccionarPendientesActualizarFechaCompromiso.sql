-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Pendientes de actualizar fecha compromiso
-- =============================================
CREATE PROCEDURE [cxc_OrdenFactura_SeleccionarPendientesActualizarFechaCompromiso] 
	-- Add the parameters for the stored procedure here
	@idempresa INT, 
	@fechainicio DATETIME,
	@estadoordenfactura INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * 
	FROM cxc_OrdenFactura
    WHERE idempresa=@idempresa 
	AND fechacompromisopago<@fechainicio
    AND estado<@estadoordenfactura
END