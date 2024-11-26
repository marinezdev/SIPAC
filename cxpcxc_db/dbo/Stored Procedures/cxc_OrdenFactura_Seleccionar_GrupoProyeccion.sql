-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Obtiene la proyeccion de grupo
-- =============================================
CREATE PROCEDURE [cxc_OrdenFactura_Seleccionar_GrupoProyeccion] 
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
	SELECT proyecto,tipomoneda,SUM(importe) AS Total 
	FROM cxc_OrdenFactura
    WHERE idempresa=@idempresa 
	AND (fechacompromisopago>=@fechainicio 
	AND fechacompromisopago<@fechatermino)
    AND estado<@estadoordenfactura
    GROUP BY proyecto,tipomoneda 
END