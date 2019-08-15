﻿CREATE VIEW [web].[VWBAN_001]
AS
SELECT dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan, dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdCbteCble, dbo.ba_Cbte_Ban.IdTipocbte, dbo.ba_Cbte_Ban.IdBanco, dbo.ba_Banco_Cuenta.ba_descripcion, dbo.ba_Cbte_Ban.cb_Fecha, 
                  dbo.ba_Cbte_Ban.cb_Observacion, dbo.ba_Cbte_Ban.Estado, dbo.ba_Cbte_Ban.IdTipoNota, dbo.ba_tipo_nota.Descripcion AS Descripcion_TipoNota, 'Ninguno' AS NomBeneficiario, dbo.ct_cbtecble_det.IdCtaCble, 
                  dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe, 
                  CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber, dbo.seg_usuario.Nombre, dbo.tb_sucursal.Su_Descripcion, pc.nom_punto_cargo, pg.nom_punto_cargo_grupo
FROM     dbo.ba_tipo_nota RIGHT OUTER JOIN
                  dbo.ba_Cbte_Ban_tipo INNER JOIN
                  dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo ON dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan = dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.CodTipoCbteBan INNER JOIN
                  dbo.ba_Cbte_Ban ON dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdEmpresa = dbo.ba_Cbte_Ban.IdEmpresa AND dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdTipoCbteCble = dbo.ba_Cbte_Ban.IdTipocbte INNER JOIN
                  dbo.ba_Banco_Cuenta ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND dbo.ba_Cbte_Ban.IdBanco = dbo.ba_Banco_Cuenta.IdBanco INNER JOIN
                  dbo.ct_cbtecble_det INNER JOIN
                  dbo.ct_plancta ON dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_plancta.IdEmpresa AND dbo.ct_cbtecble_det.IdCtaCble = dbo.ct_plancta.IdCtaCble ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND 
                  dbo.ba_Cbte_Ban.IdTipocbte = dbo.ct_cbtecble_det.IdTipoCbte AND dbo.ba_Cbte_Ban.IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble ON dbo.ba_tipo_nota.IdEmpresa = dbo.ba_Cbte_Ban.IdEmpresa AND 
                  dbo.ba_tipo_nota.IdTipoNota = dbo.ba_Cbte_Ban.IdTipoNota LEFT OUTER JOIN
                  dbo.seg_usuario ON dbo.ba_Cbte_Ban.IdUsuario = dbo.seg_usuario.IdUsuario LEFT OUTER JOIN
                  dbo.tb_sucursal ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ba_Cbte_Ban.IdSucursal = dbo.tb_sucursal.IdSucursal left JOIN 
				  ct_punto_cargo AS PC ON  ct_cbtecble_det.IdEmpresa = PC.IdEmpresa AND ct_cbtecble_det.IdPunto_cargo = PC.IdPunto_cargo left JOIN 
				  ct_punto_cargo_grupo AS Pg ON  ct_cbtecble_det.IdEmpresa = Pg.IdEmpresa AND ct_cbtecble_det.IdPunto_cargo_grupo = Pg.IdPunto_cargo_grupo 
WHERE  (NOT EXISTS
                      (SELECT IdEmpresa, Idcancelacion, Secuencia, IdEmpresa_op, IdOrdenPago_op, Secuencia_op, IdEmpresa_op_padre, IdOrdenPago_op_padre, Secuencia_op_padre, IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp, IdEmpresa_pago, 
                                         IdTipoCbte_pago, IdCbteCble_pago, MontoAplicado, SaldoAnterior, SaldoActual, Observacion, fechaTransaccion
                       FROM      dbo.cp_orden_pago_cancelaciones AS f
                       WHERE   (IdEmpresa_pago = dbo.ba_Cbte_Ban.IdEmpresa) AND (IdTipoCbte_pago = dbo.ba_Cbte_Ban.IdTipocbte) AND (IdCbteCble_pago = dbo.ba_Cbte_Ban.IdCbteCble)))
UNION ALL
SELECT dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan, dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdCbteCble, dbo.ba_Cbte_Ban.IdTipocbte, dbo.ba_Cbte_Ban.IdBanco, dbo.ba_Banco_Cuenta.ba_descripcion, dbo.ba_Cbte_Ban.cb_Fecha, 
                  dbo.ba_Cbte_Ban.cb_Observacion, dbo.ba_Cbte_Ban.Estado, dbo.ba_Cbte_Ban.IdTipoNota, dbo.ba_tipo_nota.Descripcion AS Descripcion_TipoNota, dbo.tb_persona.pe_nombreCompleto, dbo.ct_cbtecble_det.IdCtaCble, 
                  dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe, 
                  CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber, dbo.seg_usuario.Nombre, dbo.tb_sucursal.Su_Descripcion,
				  pc.nom_punto_cargo, pg.nom_punto_cargo_grupo
FROM     dbo.ba_Banco_Cuenta INNER JOIN
                  dbo.ba_Cbte_Ban ON dbo.ba_Banco_Cuenta.IdEmpresa = dbo.ba_Cbte_Ban.IdEmpresa AND dbo.ba_Banco_Cuenta.IdBanco = dbo.ba_Cbte_Ban.IdBanco INNER JOIN
                  dbo.ct_cbtecble_det ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.ba_Cbte_Ban.IdTipocbte = dbo.ct_cbtecble_det.IdTipoCbte AND 
                  dbo.ba_Cbte_Ban.IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble INNER JOIN
                  dbo.ct_plancta ON dbo.ct_cbtecble_det.IdCtaCble = dbo.ct_plancta.IdCtaCble AND dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_plancta.IdEmpresa INNER JOIN
                  dbo.tb_persona INNER JOIN
                  dbo.cp_orden_pago ON dbo.tb_persona.IdPersona = dbo.cp_orden_pago.IdPersona INNER JOIN
                  dbo.cp_orden_pago_cancelaciones ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_op AND dbo.cp_orden_pago.IdOrdenPago = dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op ON 
                  dbo.ba_Cbte_Ban.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago AND dbo.ba_Cbte_Ban.IdCbteCble = dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago AND 
                  dbo.ba_Cbte_Ban.IdTipocbte = dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago INNER JOIN
                  dbo.ba_Cbte_Ban_tipo INNER JOIN
                  dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo ON dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan = dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.CodTipoCbteBan ON 
                  dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdEmpresa AND dbo.ba_Cbte_Ban.IdTipocbte = dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdTipoCbteCble LEFT OUTER JOIN
                  dbo.ba_tipo_nota ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_tipo_nota.IdEmpresa AND dbo.ba_Cbte_Ban.IdTipoNota = dbo.ba_tipo_nota.IdTipoNota LEFT OUTER JOIN
                  dbo.seg_usuario ON dbo.ba_Cbte_Ban.IdUsuario = dbo.seg_usuario.IdUsuario LEFT OUTER JOIN
                  dbo.tb_sucursal ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ba_Cbte_Ban.IdSucursal = dbo.tb_sucursal.IdSucursal left JOIN 
				  ct_punto_cargo AS PC ON  ct_cbtecble_det.IdEmpresa = PC.IdEmpresa AND ct_cbtecble_det.IdPunto_cargo = PC.IdPunto_cargo left JOIN 
				  ct_punto_cargo_grupo AS Pg ON  ct_cbtecble_det.IdEmpresa = Pg.IdEmpresa AND ct_cbtecble_det.IdPunto_cargo_grupo = Pg.IdPunto_cargo_grupo 
GROUP BY dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan, dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdCbteCble, dbo.ba_Cbte_Ban.IdTipocbte, dbo.ba_Cbte_Ban.IdBanco, dbo.ba_Banco_Cuenta.ba_descripcion, dbo.ba_Cbte_Ban.cb_Fecha, 
                  dbo.ba_Cbte_Ban.cb_Observacion, dbo.ba_Cbte_Ban.Estado, dbo.ba_Cbte_Ban.IdTipoNota, dbo.ba_tipo_nota.Descripcion, dbo.tb_persona.pe_nombreCompleto, dbo.ct_cbtecble_det.IdCtaCble, dbo.ct_plancta.pc_Cuenta, 
                  dbo.ct_cbtecble_det.dc_Valor, dbo.ct_cbtecble_det.secuencia, dbo.seg_usuario.Nombre, dbo.tb_sucursal.Su_Descripcion,pc.nom_punto_cargo, pg.nom_punto_cargo_grupo