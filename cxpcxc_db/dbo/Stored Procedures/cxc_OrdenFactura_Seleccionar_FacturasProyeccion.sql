-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Facturas proyeccion
-- =============================================
CREATE PROCEDURE cxc_OrdenFactura_Seleccionar_FacturasProyeccion 
	-- Add the parameters for the stored procedure here
	@idempresa INT, 
	@fechainicio DATETIME,
	@fechatermino DATETIME,
	@estadoordenfactura INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * 
	FROM cxc_ordenfactura
    WHERE idempresa=@idempresa
	AND (fechacompromisopago>=@fechainicio AND fechacompromisopago<@fechatermino)
    AND Estado<@estadoordenfactura 
END