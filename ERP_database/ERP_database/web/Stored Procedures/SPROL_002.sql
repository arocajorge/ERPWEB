﻿

CREATE  PROCEDURE [web].[SPROL_002]  
	@idempresa int,
	@idnomina_tipo int,
	@idnomina_Tipo_liq int,
	@idperiodo int
AS

BEGIN

--declare
--    @idempresa int,
--	@idnomina_tipo int,
--	@idnomina_Tipo_liq int,
--	@idperiodo int


--	set @idempresa =1
--	set @idnomina_tipo =1
--	set @idnomina_Tipo_liq =2
--	set @idperiodo =201901
	
	declare
	@IdRubroTotalPagar int

	select @IdRubroTotalPagar=IdRubro_tot_pagar from ro_rubros_calculados where IdEmpresa=@idempresa

	delete web.ro_SPROL_002 where IdEmpresa=@idempresa and IdPeriodo=@idperiodo

	insert  into web.ro_SPROL_002(IdEmpresa,IdNominaTipo,IdNominaTipoLiqui,IdPeriodo,IdEmpleado,IdRubro,Valor,Idsucursal, IdArea)	
	select ro.IdEmpresa,ro.IdNominaTipo,ro.IdNominaTipoLiqui,ro.IdPeriodo,D.IdEmpleado,D.IdRubro,Valor, D.IdSucursal,emp.IdArea
	
	FROM            dbo.ro_rol_detalle AS D INNER JOIN
                         dbo.ro_empleado AS emp ON D.IdEmpresa = emp.IdEmpresa AND D.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_rol AS Ro ON D.IdEmpresa = Ro.IdEmpresa AND D.IdRol = Ro.IdRol INNER JOIN
                         dbo.ro_rubro_tipo AS R ON D.IdEmpresa = R.IdEmpresa AND D.IdRubro = R.IdRubro

	  where D.IdEmpresa=@idempresa
	  and IdNominaTipo=@idnomina_tipo
	  and IdNominaTipoLiqui=@idnomina_Tipo_liq
	  and IdPeriodo=@idperiodo
	  and R.IdEmpresa=D.IdEmpresa
	  and D.IdRubro=R.IdRubro
	   and ro.IdEmpresa=D.IdEmpresa
	   and ro.IdRol=D.IdRol
	  And R.ru_tipo='I'
	  and D.Valor>0


	  -- inserto si tiene saldo zero a pagar 
	insert  into web.ro_SPROL_002(IdEmpresa,IdNominaTipo,IdNominaTipoLiqui,IdPeriodo,IdEmpleado,IdRubro,Valor,Idsucursal, IdArea)	
	select ro.IdEmpresa,ro.IdNominaTipo,ro.IdNominaTipoLiqui,ro.IdPeriodo,D.IdEmpleado,D.IdRubro,Valor , D.IdSucursal,emp.IdArea

	FROM            dbo.ro_rol_detalle AS D INNER JOIN
                         dbo.ro_empleado AS emp ON D.IdEmpresa = emp.IdEmpresa AND D.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_rol AS Ro ON D.IdEmpresa = Ro.IdEmpresa AND D.IdRol = Ro.IdRol INNER JOIN
                         dbo.ro_rubro_tipo AS R ON D.IdEmpresa = R.IdEmpresa AND D.IdRubro = R.IdRubro

	  where D.IdEmpresa=@idempresa
	  and IdNominaTipo=@idnomina_tipo
	  and IdNominaTipoLiqui=@idnomina_Tipo_liq
	  and IdPeriodo=@idperiodo
	  and R.IdEmpresa=D.IdEmpresa
	  and D.IdRubro=R.IdRubro
	   and ro.IdEmpresa=D.IdEmpresa
	   and ro.IdRol=D.IdRol
	  And R.ru_tipo='A'
	  and D.Valor=0
	  and D.IdRubro=950



	insert  into web.ro_SPROL_002(IdEmpresa,IdNominaTipo,IdNominaTipoLiqui,IdPeriodo,IdEmpleado,IdRubro,Valor ,Idsucursal, IdArea)	
	select ro.IdEmpresa,ro.IdNominaTipo,ro.IdNominaTipoLiqui,ro.IdPeriodo,D.IdEmpleado,D.IdRubro,Valor*-1 ,D.IdSucursal,emp.IdArea
	
FROM            dbo.ro_rol_detalle AS D INNER JOIN
                         dbo.ro_empleado AS emp ON D.IdEmpresa = emp.IdEmpresa AND D.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_rol AS Ro ON D.IdEmpresa = Ro.IdEmpresa AND D.IdRol = Ro.IdRol INNER JOIN
                         dbo.ro_rubro_tipo AS R ON D.IdEmpresa = R.IdEmpresa AND D.IdRubro = R.IdRubro

	  where D.IdEmpresa=@idempresa
	  and IdNominaTipo=@idnomina_tipo
	  and IdNominaTipoLiqui=@idnomina_Tipo_liq
	  and IdPeriodo=@idperiodo
	  and R.IdEmpresa=D.IdEmpresa
	  and D.IdRubro=R.IdRubro
	   and ro.IdEmpresa=D.IdEmpresa
	   and ro.IdRol=D.IdRol
	  And R.ru_tipo='E'
	  and D.Valor>0


END
