﻿using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.CuentasPorCobrar;
using Core.Erp.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.CuentasPorCobrar
{
    public class cxc_cobro_Data
    {
        public List<cxc_cobro_Info> get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                List<cxc_cobro_Info> Lista;

                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    Lista = (from q in Context.vwcxc_cobro
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && Fecha_ini <= q.cr_fecha && q.cr_fecha <= Fecha_fin
                             orderby q.IdCobro
                             select new cxc_cobro_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdCobro = q.IdCobro,
                                 IdCliente = q.IdCliente,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 IdCobro_tipo = q.IdCobro_tipo,
                                 tc_descripcion = q.tc_descripcion,
                                 cr_fecha = q.cr_fecha,
                                 cr_TotalCobro = q.cr_TotalCobro,
                                 cr_estado = q.cr_estado,
                                 Su_Descripcion = q.Su_Descripcion
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private decimal get_id(int IdEmpesa, int IdSucursal)
        {
            try
            {
                decimal ID = 1;

                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    var lst = from q in Context.cxc_cobro
                              where q.IdEmpresa == IdEmpesa
                              && q.IdSucursal == IdSucursal
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCobro) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cxc_cobro_Info get_info(int IdEmpresa, int IdSucursal, decimal IdCobro)
        {
            try
            {
                cxc_cobro_Info info;

                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    var Entity = Context.cxc_cobro.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdCobro == IdCobro).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new cxc_cobro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdCobro = Entity.IdCobro,
                        cr_Codigo = Entity.cr_Codigo,
                        IdCobro_tipo = Entity.IdCobro_tipo,
                        IdCliente = Entity.IdCliente,
                        cr_TotalCobro = Entity.cr_TotalCobro,
                        cr_fecha = Entity.cr_fecha,
                        cr_fechaDocu = Entity.cr_fechaDocu,
                        cr_fechaCobro = Entity.cr_fechaCobro,
                        cr_observacion = Entity.cr_observacion,
                        cr_Banco = Entity.cr_Banco,
                        cr_cuenta = Entity.cr_cuenta,
                        cr_NumDocumento = Entity.cr_NumDocumento,
                        cr_Tarjeta = Entity.cr_Tarjeta,
                        cr_propietarioCta = Entity.cr_propietarioCta,
                        cr_estado = Entity.cr_estado,
                        cr_recibo = Entity.cr_recibo,
                        cr_es_anticipo = Entity.cr_es_anticipo,
                        IdBanco = Entity.IdBanco,
                        IdCaja = Entity.IdCaja
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cxc_cobro_Info info)
        {
            Entities_cuentas_por_cobrar Context_cxc = new Entities_cuentas_por_cobrar();
            Entities_facturacion Context_fac = new Entities_facturacion();
            Entities_caja Context_caj = new Entities_caja();
            Entities_contabilidad Context_ct = new Entities_contabilidad();
            ct_cbtecble_Data data_ct = new ct_cbtecble_Data();
            try
            {
                #region Variables
                int Secuencia = 1;
                bool generar_diario = true;
                string IdCtaCble_debe = string.Empty;
                string IdCtaCble_haber = string.Empty;
                int IdTipoCbte = 0;
                int IdTipoMoviCaja = 0;
                #endregion

                #region Consultas para generar diario
                #region CtaCble debe
                var cliente = Context_fac.fa_cliente.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente).FirstOrDefault();
                if (cliente == null)
                    return false;
                IdCtaCble_haber = cliente.IdCtaCble_cxc;
                #endregion

                var tipo_cobro = Context_cxc.cxc_cobro_tipo.Where(q => q.IdCobro_tipo == info.IdCobro_tipo).FirstOrDefault();
                if (tipo_cobro == null)
                    return false;

                #region CtaCble Haber
                if (tipo_cobro.tc_Tomar_Cta_Cble_De == cl_enumeradores.eTipoCobroTomaCuentaDe.CAJA.ToString())
                {
                    var caja = Context_caj.caj_Caja.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCaja == info.IdCaja).FirstOrDefault();
                    if (caja == null)
                        return false;
                    IdCtaCble_debe = caja.IdCtaCble;
                }
                else
                    if (tipo_cobro.tc_Tomar_Cta_Cble_De == cl_enumeradores.eTipoCobroTomaCuentaDe.TIP_COBRO.ToString())
                {
                    var cta_x_tipo = Context_cxc.cxc_cobro_tipo_Param_conta_x_sucursal.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCobro_tipo == info.IdCobro_tipo && q.IdSucursal == info.IdSucursal).FirstOrDefault();
                    if (cta_x_tipo != null)
                        IdCtaCble_debe = cta_x_tipo.IdCtaCble;
                }
                if (string.IsNullOrEmpty(IdCtaCble_debe) || string.IsNullOrEmpty(IdCtaCble_haber))
                    generar_diario = false;
                
                #endregion

                if(generar_diario)
                {
                    #region TipoCbte
                    var param_cxc = Context_cxc.cxc_Parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                    if (param_cxc == null)
                        return false;
                                        
                    if (tipo_cobro.tc_Que_Tipo_Registro_Genera == cl_enumeradores.eTipoCobroGenera.DIARIO.ToString())
                    {
                        IdTipoCbte = param_cxc.pa_IdTipoCbteCble_CxC == null ? 0 : Convert.ToInt32(param_cxc.pa_IdTipoCbteCble_CxC);
                    }
                    else
                        if (tipo_cobro.tc_Que_Tipo_Registro_Genera == cl_enumeradores.eTipoCobroGenera.MOVI_CAJA.ToString())
                    {
                        var param_caja = Context_caj.caj_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                        if (param_caja == null)
                            return false;
                        IdTipoCbte = param_caja.IdTipoCbteCble_MoviCaja_Ing;
                        IdTipoMoviCaja = param_cxc.pa_IdTipoMoviCaja_x_Cobros_x_cliente == null ? 0 : Convert.ToInt32(param_cxc.pa_IdTipoMoviCaja_x_Cobros_x_cliente);
                    }

                    if (IdTipoCbte == 0 || IdTipoMoviCaja == 0)
                        generar_diario = false;
                    
                    #endregion
                }
                #endregion

                #region Cabecera cobro
                cxc_cobro cab = new cxc_cobro
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdCobro = info.IdCobro = get_id(info.IdEmpresa, info.IdSucursal),
                    cr_Codigo = info.cr_Codigo,
                    IdCobro_tipo = info.IdCobro_tipo,
                    IdCliente = info.IdCliente,
                    cr_TotalCobro = info.cr_TotalCobro,
                    cr_fecha = info.cr_fecha,
                    cr_fechaDocu = info.cr_fechaDocu,
                    cr_fechaCobro = info.cr_fechaCobro,
                    cr_observacion = info.cr_observacion,

                    cr_Banco = info.cr_Banco,
                    cr_cuenta = info.cr_cuenta,
                    cr_NumDocumento = info.cr_NumDocumento,
                    cr_Tarjeta = info.cr_Tarjeta,
                    cr_propietarioCta = info.cr_propietarioCta,
                    cr_estado = "A",
                    cr_es_anticipo = "N",

                    IdBanco = info.IdBanco,
                    IdCaja = info.IdCaja,

                    Fecha_Transac = DateTime.Now,
                    IdUsuario = info.IdUsuario
                };
                Context_cxc.cxc_cobro.Add(cab);
                #endregion

                #region Detalle cobro
                foreach (var item in info.lst_det)
                {
                    cxc_cobro_det det = new cxc_cobro_det
                    {
                        IdEmpresa = item.IdEmpresa = cab.IdEmpresa,
                        IdSucursal = item.IdSucursal = cab.IdSucursal,
                        IdCobro = item.IdCobro = cab.IdCobro,
                        secuencial = item.secuencial = Secuencia++,
                        dc_TipoDocumento = item.dc_TipoDocumento,
                        IdBodega_Cbte = item.IdBodega_Cbte,
                        IdCbte_vta_nota = item.IdCbte_vta_nota,
                        dc_ValorPago = item.dc_ValorPago,
                        IdUsuario = cab.IdUsuario,
                        Fecha_Transac = DateTime.Now,
                        estado = "A"
                    };
                    Context_cxc.cxc_cobro_det.Add(det);
                }
                #endregion               

                if (generar_diario)
                {
                    #region Diario
                    ct_cbtecble diario = new ct_cbtecble
                    {
                        IdEmpresa = cab.IdEmpresa,
                        IdTipoCbte = IdTipoCbte,
                        IdCbteCble = data_ct.get_id(info.IdEmpresa, IdTipoCbte),
                        cb_Fecha = cab.cr_fecha.Date,
                        cb_Anio = cab.cr_fecha.Year,
                        cb_mes = cab.cr_fecha.Month,
                        IdPeriodo = Convert.ToInt32(cab.cr_fecha.ToString("yyyyMM")),
                        cb_Observacion = cab.cr_observacion,
                        cb_Estado = "A",

                        IdUsuario = cab.IdUsuario,
                        cb_FechaTransac = DateTime.Now,
                        cb_Valor = Math.Round(info.lst_det.Sum(q => q.dc_ValorPago), 2, MidpointRounding.AwayFromZero),
                    };
                    Context_ct.ct_cbtecble.Add(diario);

                    ct_cbtecble_det Debe = new ct_cbtecble_det
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = 1,
                        IdCtaCble = IdCtaCble_debe,
                        dc_Valor = Math.Round(Convert.ToDouble(diario.cb_Valor), 2, MidpointRounding.AwayFromZero),
                    };

                    ct_cbtecble_det Haber = new ct_cbtecble_det
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = 2,
                        IdCtaCble = IdCtaCble_haber,
                        dc_Valor = Math.Round(Convert.ToDouble(diario.cb_Valor), 2, MidpointRounding.AwayFromZero) * -1,
                    };
                    Context_ct.ct_cbtecble_det.Add(Debe);
                    Context_ct.ct_cbtecble_det.Add(Haber);
                    #endregion

                    #region Relacion cobro - diario
                    cxc_cobro_x_ct_cbtecble relacion_diario_cobro = new cxc_cobro_x_ct_cbtecble
                    {
                        cbr_IdEmpresa = info.IdEmpresa,
                        cbr_IdCobro = cab.IdCobro,
                        cbr_IdSucursal = cab.IdSucursal,
                        ct_IdEmpresa = diario.IdEmpresa,
                        ct_IdTipoCbte = diario.IdTipoCbte,
                        ct_IdCbteCble = diario.IdCbteCble
                    };
                    Context_cxc.cxc_cobro_x_ct_cbtecble.Add(relacion_diario_cobro);
                    #endregion
                    
                    if (IdTipoMoviCaja != 0)
                    {
                        #region Movimiento de caja
                        caj_Caja_Movimiento mov_caja = new caj_Caja_Movimiento
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipocbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            IdTipoMovi = IdTipoMoviCaja,
                            cm_fecha = diario.cb_Fecha,
                            cm_valor = diario.cb_Valor,
                            cm_Signo = "+",
                            cm_observacion = diario.cb_Observacion,
                            Estado = "A",
                            IdPeriodo = diario.IdPeriodo,
                            IdCaja = info.IdCaja,
                            IdTipo_Persona = cl_enumeradores.eTipoPersona.CLIENTE.ToString(),
                            IdEntidad = info.IdEntidad,
                            IdPersona = cliente.IdPersona,

                            IdUsuario = info.IdUsuario,
                            Fecha_Transac = DateTime.Now
                        };
                        Context_caj.caj_Caja_Movimiento.Add(mov_caja);
                        caj_Caja_Movimiento_det mov_caja_det = new caj_Caja_Movimiento_det
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipocbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            IdCobro_tipo = info.IdCobro_tipo,
                            cr_Valor = diario.cb_Valor,
                            Secuencia = 1
                        };
                        Context_caj.caj_Caja_Movimiento_det.Add(mov_caja_det);
                        #endregion
                    }
                }

                Context_ct.SaveChanges();
                Context_cxc.SaveChanges();
                Context_caj.SaveChanges();               

                Context_cxc.Dispose();
                Context_fac.Dispose();
                Context_caj.Dispose();
                Context_ct.Dispose();
                return true;
            }
            catch (Exception)
            {
                Context_cxc.Dispose();
                Context_fac.Dispose();
                Context_caj.Dispose();
                Context_ct.Dispose();
                throw;
            }
        }

        public bool modificarDB(cxc_cobro_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    var Entity = Context.cxc_cobro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdCobro == info.IdCobro).FirstOrDefault();
                    if (Entity == null) return false;
                    Entity.cr_Codigo = info.cr_Codigo;
                    Entity.IdCobro_tipo = info.IdCobro_tipo;
                    Entity.IdCliente = info.IdCliente;
                    Entity.cr_TotalCobro = info.cr_TotalCobro;
                    Entity.cr_fecha = info.cr_fecha;
                    Entity.cr_fechaDocu = info.cr_fechaDocu;
                    Entity.cr_fechaCobro = info.cr_fechaCobro;
                    Entity.cr_observacion = info.cr_observacion;
                    Entity.cr_Banco = info.cr_Banco;
                    Entity.cr_cuenta = info.cr_cuenta;
                    Entity.cr_NumDocumento = info.cr_NumDocumento;
                    Entity.cr_Tarjeta = info.cr_Tarjeta;
                    Entity.cr_propietarioCta = info.cr_propietarioCta;
                    Entity.IdBanco = info.IdBanco;
                    Entity.IdCaja = info.IdCaja;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool anularDB(cxc_cobro_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    var Entity = Context.cxc_cobro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdCobro == info.IdCobro).FirstOrDefault();
                    if (Entity == null) return false;
                    Entity.cr_estado = "I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
