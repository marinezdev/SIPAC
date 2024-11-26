-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Agrega un pago a conmciliacion
-- =============================================
CREATE PROCEDURE trf_ConciliarPago_Agregar 
	-- Add the parameters for the stored procedure here
	@referencia VARCHAR(50), 
	@banco VARCHAR(50),
	@fechapago DATETIME,
	@tipocambio INT,
	@importe DECIMAL,
	@moneda INT,
	@idusr INT,
	@estado INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO trf_ConciliarPago (fecharegistro,referencia,banco,fechapago,tipocambio,importe,moneda,idusr,estado)
    VALUES (getdate(),@referencia,@banco,@fechapago,@tipocambio,@importe,@moneda,@idusr,@estado)

END