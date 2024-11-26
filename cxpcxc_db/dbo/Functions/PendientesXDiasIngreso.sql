


CREATE FUNCTION [dbo].[PendientesXDiasIngreso](@IdEmpresa Int)
RETURNS @TBL_DATOS TABLE (SOLICITUDES varchar(32),
						TOTAL int 
						)
AS

BEGIN
	
	Declare @IdSolicitud int, @FechaREgistro Datetime,@Factura Varchar(32), @FechaFactura datetime,@NumDias int
	Declare @MENOR_15_DIAS int, @DE_15_A_30_DIAS int, @MAYORES_30_DIAS int
	
	SET @MENOR_15_DIAS =0;
	SET @DE_15_A_30_DIAS =0; 
	SET @MAYORES_30_DIAS =0;
	
	DECLARE Lista CURSOR FAST_FORWARD FOR
	select IdSolicitud,FechaRegistro,Factura,FechaFactura,DATEDIFF(d,FechaRegistro,getdate()) as DIAS from trf_Solicitud 
				where  Estado=10 and IdEmpresa =@IdEmpresa
				
	OPEN Lista
	FETCH NEXT FROM Lista INTO @IdSolicitud,@FechaREgistro,@Factura,@FechaFactura,@NumDias
	WHILE @@FETCH_STATUS = 0 BEGIN
		
		if @NumDias <= 15 begin set @MENOR_15_DIAS =@MENOR_15_DIAS + 1 end

		if (@NumDias>15 and  @NumDias<=30) begin set @DE_15_A_30_DIAS =@DE_15_A_30_DIAS+ 1 end

		if @NumDias> 30 begin set @MAYORES_30_DIAS =@MAYORES_30_DIAS + 1 end		


		FETCH NEXT FROM Lista INTO @IdSolicitud,@FechaREgistro,@Factura,@FechaFactura,@NumDias
	END
	close Lista
	deallocate Lista

	insert into @TBL_DATOS values('MENOR 15 DIAS',@MENOR_15_DIAS)	
	insert into @TBL_DATOS values('DE 15 A 30 DIAS',@DE_15_A_30_DIAS)	
	insert into @TBL_DATOS values('MAS DE 30 DIAS',@MAYORES_30_DIAS)	
	
	return 
END

