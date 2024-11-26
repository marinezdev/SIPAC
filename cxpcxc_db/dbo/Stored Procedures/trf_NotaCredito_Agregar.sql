-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 8/10/20
-- Description:	Agrega un nuevo registro
-- =============================================
CREATE PROCEDURE [trf_NotaCredito_Agregar]
	-- Add the parameters for the stored procedure here
	@idempresa INT,
	@fecha DATETIME,
	@rfc VARCHAR(16),
	@proveedor VARCHAR(80),
	@descripcion VARCHAR(256),
	@importe DECIMAL,
	@moneda VARCHAR(8),
	@importependiente DECIMAL,
	@estado INT,
	@idusr INT,
	@idsolicitudorigen INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO trf_notacredito (fecharegistro,idempresa,fecha,rfc,proveedor,descripcion,importe,moneda,importependiente,
	estado,idusr,idsolicitudorigen)
	VALUES (getdate(),@idempresa,@fecha,@rfc,@proveedor,@descripcion,@importe,@moneda,@importependiente,
	@estado,@idusr,@idsolicitudorigen)
END