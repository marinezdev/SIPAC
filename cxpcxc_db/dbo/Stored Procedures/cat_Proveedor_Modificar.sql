-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 25/09/20
-- Description:	Modifica un registro
-- =============================================
CREATE PROCEDURE cat_Proveedor_Modificar 
	-- Add the parameters for the stored procedure here
	@idempresa NCHAR(10),
	@nombre VARCHAR(80),
	@rfc VARCHAR(16),
	@direccion VARCHAR(255),
	@ciudad VARCHAR(128),
	@estado VARCHAR(32),
	@cp VARCHAR(5),
	@contacto VARCHAR(80),
	@correo VARCHAR(64),
	@telefono VARCHAR(32),
	@extencion VARCHAR(16),
	@sinfactura INT,
	@activo INT,
	@id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE cat_Proveedor SET
     Nombre=@nombre
    ,Rfc=@rfc
    ,Direccion=@direccion
    ,Ciudad=@ciudad
    ,Estado=@estado
    ,Cp=@cp
    ,Contacto=@contacto
    ,Correo=@correo
    ,Telefono=@telefono
    ,Extencion=@extencion
    ,SinFactura=@sinfactura
    ,Activo=@activo
    WHERE Id=@id
END