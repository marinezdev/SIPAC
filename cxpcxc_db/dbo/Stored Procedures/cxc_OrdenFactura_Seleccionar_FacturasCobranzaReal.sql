-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Facturas cobranza real
-- =============================================
CREATE PROCEDURE cxc_OrdenFactura_Seleccionar_FacturasCobranzaReal
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
	SELECT b.idordenfactura,b.fecharegistro,f.cliente,f.numfactura,f.fechafactura,f.proyecto ,f.importe,f.tipomoneda
    FROM cxc_bitacora b 
	INNER JOIN cxc_ordenfactura AS f 
    ON f.idordenfactura=b.idordenfactura 
    WHERE f.idempresa=@idempresa
    AND  f.especial=0
    AND  b.estado=@estadoordenfactura
    AND (b.fecharegistro>@fechainicio AND b.fecharegistro<@fechatermino)
    ORDER BY f.cliente,f.proyecto
END