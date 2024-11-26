-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Agrega un registro
-- =============================================
CREATE PROCEDURE cxc_Archivos_Agregar
	-- Add the parameters for the stored procedure here
	@idordenfactura INT,
	@tipo INT,
	@iddocumento INT ,
	@archivoorigen VARCHAR(65),
	@archivodestino VARCHAR(65),
	@nota VARCHAR(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO cxc_Archivos (IdOrdenFactura,FechaRegistro,Tipo,IdDocumento,ArchivoOrigen,ArchivoDestino,Nota)
	VALUES (@idordenfactura,getdate(),@tipo,@iddocumento,@archivoorigen,@archivodestino,@nota)
END