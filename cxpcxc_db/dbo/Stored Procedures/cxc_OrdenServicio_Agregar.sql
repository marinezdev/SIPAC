-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 9/10/20
-- Description:	Agrega un nuevo registro
-- =============================================
CREATE PROCEDURE [dbo].[cxc_OrdenServicio_Agregar] 
	-- Add the parameters for the stored procedure here
	@idservicio INT,  
	@idcliente INT, 
	@cliente VARCHAR(80),
	@rfc VARCHAR(16),	
	@idempresa INT, 
	@empresa VARCHAR(80), 
	@tiposolicitud INT, 
	@fechainicio DATETIME, 
	@fechatermino DATETIME, 
	@importe DECIMAL, 
	@periodos INT, 
	@tipoperiodo INT, 
	@condicionpago VARCHAR(64), 
	@condicionpagodias INT,
	@proyecto VARCHAR(100),
	@tipomoneda VARCHAR(8), 
	@idcatservicio INT,
	@servicio VARCHAR(100),
	@idusr INT, 
	@unidadnegocio INT, 
	@contrato INT,
	@enviacorreoclte  INT,
	@estado INT,
	@especial INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION

	BEGIN TRY

		-- Insert statements for procedure here
		INSERT INTO cxc_OrdenServicio (
		idservicio, 
		fecharegistro,
		idcliente,
		cliente,
		rfc,
		idempresa,
		empresa,
		tiposolicitud,
		fechainicio,
		fechatermino,
		importe,
		periodos,
		tipoperiodo,
		condicionpago,
		condicionpagodias,
		proyecto,
		tipomoneda,
		idcatservicio,
		servicio,
		idusr,
		unidadnegocio,
		contrato,
		enviacorreoclte,
		estado,
		especial) 
		VALUES(
		@idservicio, 
		getdate(),
		@idcliente,
		@cliente,
		@rfc,
		@idempresa,
		@empresa,
		@tiposolicitud,
		@fechainicio,
		@fechatermino,
		@importe,
		@periodos,
		@tipoperiodo,
		@condicionpago,
		@condicionpagodias,
		@proyecto,
		@tipomoneda,
		@idcatservicio,
		@servicio,
		@idusr,
		@unidadnegocio,
		@contrato,
		@enviacorreoclte,
		@estado,
		@especial)

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
	
		ROLLBACK TRANSACTION
	
	END CATCH

END