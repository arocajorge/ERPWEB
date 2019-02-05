﻿CREATE PROCEDURE [web].[SPBAN_008]
(
@IdEmpresa int,
@FechaIni date,
@FechaFin date,
@IdBanco int
)
AS
/*
set @IdEmpresa = 1
set @Fechaini = DATEFROMPARTS(2019,1,5)
set @FechaFin = DATEFROMPARTS(2019,1,31)
set @IdBanco = 1
*/
DECLARE @i_SaldoInicial numeric(18,2),
@i_IdCtaCbleBanco varchar(20),
@i_ba_descripcion varchar(5000)

select @i_IdCtaCbleBanco = IdCtaCble, @i_ba_descripcion = ba_descripcion from ba_Banco_Cuenta 
where IdEmpresa = @IdEmpresa
and IdBanco = @IdBanco

select @i_SaldoInicial = sum(d.dc_Valor)
from ct_cbtecble_det as d inner join ct_cbtecble as c
on c.IdEmpresa = d.IdEmpresa and c.IdTipoCbte = d.IdTipoCbte and c.IdCbteCble = d.IdCbteCble
where c.IdEmpresa = @IdEmpresa and c.cb_Fecha < @FechaIni 
and d.IdCtaCble = @i_IdCtaCbleBanco

set @i_SaldoInicial = isnull(@i_SaldoInicial,0)


select d.IdEmpresa,d.IdTipoCbte,d.IdCbteCble,d.secuencia, @i_SaldoInicial as SaldoInicial, b.cb_Observacion, b.cb_Cheque, b.cb_giradoA,c.cb_Fecha, cast(d.dc_Valor as numeric(18,2)) as Valor,
case when d.dc_Valor > 0 then 'INGRESOS' ELSE 'EGRESOS' END AS Tipo, abs(cast(d.dc_Valor as numeric(18,2))) as ValorAbsoluto, case when d.dc_Valor > 0 then 1 ELSE 2 END AS Orden,
isnull(b.cb_Cheque,b.IdCbteCble) as Referencia, @i_ba_descripcion ba_descripcion, @IdBanco IdBanco, b.Estado, c.IdSucursal, s.Su_Descripcion
from ct_cbtecble as c inner join ct_cbtecble_det
as d on c.IdEmpresa = d.IdEmpresa and c.IdTipoCbte = d.IdTipoCbte and c.IdCbteCble = d.IdCbteCble 
inner join ba_Cbte_Ban as b on b.IdEmpresa = c.IdEmpresa and b.IdTipocbte = c.IdTipoCbte
and b.IdCbteCble = c.IdCbteCble INNER JOIN tb_sucursal as s
on c.idempresa = s.idempresa and c.IdSucursal = c.IdSucursal
where c.IdEmpresa = @IdEmpresa and d.IdCtaCble = @i_IdCtaCbleBanco and c.cb_Fecha between @FechaIni and @FechaFin
and not exists(
select r.IdEmpresa from ct_cbtecble_Reversado as r
inner join ct_cbtecble as cr on r.IdEmpresa_Anu = cr.IdEmpresa
and r.IdTipoCbte_Anu = cr.IdTipoCbte
and r.IdCbteCble_Anu = cr.IdCbteCble
where r.idempresa = c.idempresa
and r.idtipocbte = c.idtipocbte
and r.idcbtecble = c.idcbtecble
and cr.cb_Fecha <= @FechaFin
)
UNION ALL
SELECT @IdEmpresa, 0, 0, 0, @i_SaldoInicial, 'SALDO INICIAL',NULL,NULL,DATEADD(DAY,-1, @FechaIni), @i_SaldoInicial,
'INGRESOS',@i_SaldoInicial,1,'S.I.', @i_ba_descripcion, @IdBanco, 'A',0,NULL