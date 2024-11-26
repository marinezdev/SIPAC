

CREATE FUNCTION [dbo].[ConsultaSeguimientoLotes](@IdEmpresa Int,@FechaInicio Datetime,@FechaTermino Datetime)
RETURNS @TBL_DATOS TABLE (IdFondeo int,
						FechaRegistro Datetime,
						Total Decimal(18,2),
						TotalAprob Decimal(18,2),
						NoSolicitudes Int,
						Pagadas Int,
						NoPagadas Int,
						Estado Varchar(64))
AS

BEGIN
	
	
		
		insert into @TBL_DATOS 
		
		select SF.IdFondeo,SF.FechaRegistro,SF.Total, SF.TotalAprob, SF.NoSolicitudes, 
		(select count(*) from trf_Solicitud where IdSolicitud in(select IdSolicitud  from trf_SolicitudFondosDetalle where IdFondeo =SF.IdFondeo and estado>0)and  estado in(50,40)) as Pagadas, 
		(select count(*) from trf_Solicitud where IdSolicitud in(select IdSolicitud  from trf_SolicitudFondosDetalle where IdFondeo =SF.IdFondeo and estado>0) and  estado=30) as NoPagadas, 
		CASE SF.Estado WHEN  10 THEN 'AUTORIZADO' WHEN  20 THEN 'CON FONDOS' WHEN  30 THEN 'AUTORIZADO SIN FONDOS' END  AS Estado  
		from  trf_SolicitudFondos as SF where SF.IdEmpresa =@IdEmpresa and (FechaRegistro>=@FechaInicio and FechaRegistro<DATEADD(dd,1,@FechaTermino))
		order by SF.IdFondeo 
		
	return 
END

