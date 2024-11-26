-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 9/10/20
-- Description:	Agrega un registro
-- =============================================
CREATE PROCEDURE cat_NotaCreditoArchivos_Agregar 
	-- Add the parameters for the stored procedure here
	@idnotacredito INT,
	@tipo INT,
	@nombre VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO trf_notacreditoarchivos (idnotacredito,tipo,nombre)
    VALUES (@idnotacredito,@tipo,@nombre)
END