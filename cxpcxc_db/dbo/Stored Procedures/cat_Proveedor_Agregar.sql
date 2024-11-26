-- =============================================
-- Author:		Jose Luis Villarreal Ruiz
-- Create date: 25/09/20
-- Description:	Agrega un nuevo registro
-- =============================================
CREATE PROCEDURE cat_Proveedor_Agregar 
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
	@activo INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO cat_Proveedor (FechaRegistro,IdEmpresa,Nombre,Rfc,Direccion,Ciudad,Estado,Cp,Contacto,Correo,Telefono,
	Extencion,SinFactura,Activo)
	VALUES (getdate(),@idempresa,@nombre,@rfc,@direccion,@ciudad,@estado,@cp,@contacto,@correo,@telefono,
	@extencion,@sinfactura,@activo)
END