-- =============================================
-- Author:		José Luis Villarreal Ruiz
-- Create date: 9/10/20
-- Description:	Agrega orden factura
-- =============================================
CREATE PROCEDURE [dbo].[cxc_OrdenFactura_Agregar] 
	-- Add the parameters for the stored procedure here
    @idordenfactura INT,
    @fechainicio DATETIME,
    @fechafactura DATETIME,
    @idcliente INT,
    @rfc VARCHAR(16),
    @cliente VARCHAR(80),
    @idempresa INT,
    @empresa VARCHAR(80),
    @tiposolicitud INT,
    @importe DECIMAL,
    @condicionpago VARCHAR(64),
    @condicionpagodias INT,
    @proyecto VARCHAR(100),
    @tipomoneda VARCHAR(8),
    @idcatservicio INT,
    @servicio VARCHAR(100),
    @descripcion VARCHAR(128),
    @anotaciones VARCHAR(255),
    @idusr INT,
    @unidadnegocio INT,
    @estado INT,
    @factura INT,
    @enviacorreoclte INT,
    @especial INT,
    @marcado INT,
    @fechacompromisopago DATETIME,
	@idobtenido INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @idorden INT;
	INSERT INTO cxc_OrdenFacturaCtrl(fecha) VALUES(getdate())
	SET @idorden = @@IDENTITY;

	INSERT INTO cxc_ordenfactura(
            idservicio
            ,idordenfactura
            ,fechainicio
            ,fechafactura
            ,idcliente
            ,rfc
            ,cliente
            ,idempresa
            ,empresa
            ,tiposolicitud
            ,importe
            ,condicionpago
            ,condicionpagodias
            ,proyecto
            ,tipomoneda
            ,idcatservicio
            ,servicio
            ,descripcion
            ,anotaciones
            ,idusr
            ,unidadnegocio
            ,estado
            ,factura
            ,enviacorreoclte
            ,especial
            ,marcado
            ,fechacompromisopago)                        
    VALUES (
            @idorden,
            @idordenfactura,
            @fechainicio,
            @fechafactura,
            @idcliente,
            @rfc,
            @cliente,
            @idempresa,
            @empresa,
            @tiposolicitud,
            @importe,
            @condicionpago,
            @condicionpagodias,
            @proyecto,
            @tipomoneda,
            @idcatservicio,
            @servicio,
            @descripcion,
            @anotaciones,
            @idusr,
            @unidadnegocio,
            @estado,
            @factura,
            @enviacorreoclte,
            @especial,
            @marcado,
            @fechacompromisopago)
	SELECT @idobtenido = @idorden
END