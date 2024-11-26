-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Jose Luis Villarreal Ruiz
-- =============================================
CREATE PROCEDURE cxc_OrdenFactura_Seleccionar_CobranzaRealPorProyecto
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
	SELECT proyecto,
	(SELECT SUM(importe) WHERE f.tipomoneda='Pesos') AS Pesos,
	(SELECT SUM(importe) WHERE f.tipomoneda='Dolares') AS Dolares
    FROM cxc_Bitacora b 
	INNER JOIN cxc_OrdenFactura AS f ON f.idordenfactura=b.IdOrdenFactura
    WHERE f.idempresa=@idempresa 
	AND f.especial=0
	AND b.estado=@estadoordenfactura
    AND (b.fecharegistro>@fechainicio AND b.fecharegistro<@fechatermino)
    GROUP BY f.proyecto,f.tipomoneda
END