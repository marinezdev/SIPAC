
CREATE FUNCTION [dbo].[ConsultaDireccion](@IdEmpresa Int,@Estado Int,@FechaInicio Datetime,@FechaTermino Datetime)
RETURNS @TBL_DATOS TABLE (IdSolicitud int,
						IdUnidadNegocio int, 
						UnidadNegocio varchar(150),
						FechaRegistro Datetime, 
						Factura Varchar(32), 
						FechaFactura datetime,
						Rfc Varchar(20),
						Proveedor Varchar(128),
						importe Decimal(18,2),
						Moneda Varchar(8),
						Estado Int,
						ConFactura Varchar(2))
AS

BEGIN
	
	Declare @IdSolicitud int, @FechaProceso Datetime,@Factura Varchar(32), @FechaFactura datetime,@Proveedor Varchar(128)
	Declare @importe Decimal(18,2), @Moneda Varchar(8), @ConFactura Int,@Fecha Datetime
	Declare @IdUnidadNegocio int, @UnidadNegocio varchar(150),@Rfc varchar(20), @DescConFactura varchar(2)
	

	DECLARE Lista CURSOR FAST_FORWARD FOR
	select s.IdSolicitud,s.UnidadNegocio,Un.Titulo as UnidadNegocio,s.Factura,s.FechaFactura,s.rfc,s.Proveedor,s.Moneda,s.Importe,s.ConFactura 
		from trf_Solicitud as S left join cat_UnidadNegocio as Un on Un.Id =s.UnidadNegocio 	
				where IdSolicitud in(
				select distinct(IdSolicitud) from BitacoraSolicitud  
					where  Estado=@Estado and (FechaRegistro>=@FechaInicio and FechaRegistro<DATEADD(dd,1,@FechaTermino))
				)and s.IdEmpresa =@IdEmpresa
				
	OPEN Lista
	FETCH NEXT FROM Lista INTO @IdSolicitud,@IdUnidadNegocio,@UnidadNegocio,@Factura,@FechaFactura,@Rfc,@Proveedor,@Moneda,@Importe,@ConFactura
	WHILE @@FETCH_STATUS = 0 BEGIN
		
		set @DescConFactura='SI'
		if @ConFactura = 0 begin set @DescConFactura='NO' end
		
		if @estado=50 begin
			select @importe =sum(Importe) from BitacoraSolicitud where Estado in(40,50)and IdSolicitud=@IdSolicitud and (FechaRegistro>=@FechaInicio and FechaRegistro<DATEADD(dd,1,@FechaTermino))
		end else begin
			select @importe =sum(Importe) from BitacoraSolicitud where Estado=@ESTADO and IdSolicitud=@IdSolicitud and (FechaRegistro>=@FechaInicio and FechaRegistro<DATEADD(dd,1,@FechaTermino))
		end 
		
		select top 1 @Fecha =FechaRegistro from BitacoraSolicitud where Estado=@ESTADO and  IdSolicitud =@IdSolicitud order by FechaRegistro  Desc			
		
		insert into @TBL_DATOS values(@IdSolicitud,@IdUnidadNegocio,@UnidadNegocio, @Fecha,@Factura,@FechaFactura ,@Rfc,@Proveedor,@importe,@Moneda,@Estado,@DescConFactura)	


		FETCH NEXT FROM Lista INTO @IdSolicitud,@IdUnidadNegocio,@UnidadNegocio,@Factura,@FechaFactura,@Rfc,@Proveedor,@Moneda,@Importe,@ConFactura
	END
	
	close Lista
	deallocate Lista
	
	return 
END