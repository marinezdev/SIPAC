-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 24/09/20
-- Description:	Actualiza un registro
-- =============================================
CREATE PROCEDURE [cat_CondicionPago_Modificar_Titulo_Dias] 
	-- Add the parameters for the stored procedure here
	@id INT, 
	@titulo VARCHAR(100),
	@dias INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE cat_CondicionPago SET Titulo=@titulo,Dias=@dias WHERE Id=@id
END