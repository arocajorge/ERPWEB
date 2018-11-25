﻿using Core.Erp.Data.Contabilidad;
using Core.Erp.Data.Inventario;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.Facturacion;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
   public class fa_factura_Data
    {
        public List<fa_factura_consulta_Info> get_list(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                List<fa_factura_consulta_Info> Lista;
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_factura
                             where q.IdEmpresa == IdEmpresa
                             && Fecha_ini <= q.vt_fecha && q.vt_fecha <= Fecha_fin
                             orderby q.IdCbteVta descending
                             select new fa_factura_consulta_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCbteVta = q.IdCbteVta,

                                 vt_NumFactura = q.vt_NumFactura,
                                 vt_fecha = q.vt_fecha,
                                 NomContacto = q.Nombres,
                                 Ve_Vendedor = q.Ve_Vendedor,
                                 vt_Subtotal0 = q.vt_Subtotal0,
                                 vt_SubtotalIVA = q.vt_SubtotalIVA,
                                 vt_iva = q.vt_iva,
                                 vt_total = q.vt_total,
                                 Estado = q.Estado,
                                 esta_impresa = q.esta_impresa,

                                 IdEmpresa_in_eg_x_inv = q.IdEmpresa_in_eg_x_inv,
                                 IdSucursal_in_eg_x_inv = q.IdSucursal_in_eg_x_inv,
                                 IdMovi_inven_tipo_in_eg_x_inv = q.IdMovi_inven_tipo_in_eg_x_inv,
                                 IdNumMovi_in_eg_x_inv = q.IdNumMovi_in_eg_x_inv,

                                 EstadoBool = q.Estado == "A" ? true : false

                             }).ToList();
                }
                
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<fa_factura_Info> get_list_fac_sin_guia(int IdEmpresa, decimal IdCliente)
        {
            try
            {
                List<fa_factura_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_factura_sin_guia
                             where q.IdEmpresa == IdEmpresa
                             && q.IdCliente==IdCliente
                             select new fa_factura_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCbteVta = q.IdCbteVta,
                                 vt_serie1=q.vt_serie1,
                                 vt_serie2=q.vt_serie2,
                                 vt_NumFactura=q.vt_NumFactura,
                                 vt_Observacion=q.vt_Observacion,
                                 vt_fecha=q.vt_fecha
                               
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool factura_existe(int IdEmpresa, string Serie1, string Serie2, string NumFactura)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_factura
                              where q.IdEmpresa == IdEmpresa
                              && q.vt_serie1 == Serie1
                              && q.vt_serie2 == Serie2
                              && q.vt_NumFactura == NumFactura
                              select q;

                    if (lst.Count() > 0)
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public fa_factura_Info get_info(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                fa_factura_Info info = new fa_factura_Info();
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_factura Entity = Context.fa_factura.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCbteVta == IdCbteVta);
                    if (Entity == null) return null;
                    info = new fa_factura_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        IdCbteVta = Entity.IdCbteVta,
                        CodCbteVta = Entity.CodCbteVta,
                        vt_tipoDoc = Entity.vt_tipoDoc,
                        vt_serie1 = Entity.vt_serie1,
                        vt_serie2 = Entity.vt_serie2,
                        vt_NumFactura = Entity.vt_NumFactura,
                        Fecha_Autorizacion = Entity.fecha_primera_cuota,
                        vt_anio = Entity.vt_anio,
                        vt_autorizacion = Entity.vt_autorizacion,
                        vt_fecha = Entity.vt_fecha,
                        vt_fech_venc = Entity.vt_fech_venc,
                        vt_mes = Entity.vt_mes,                       
                        IdCliente = Entity.IdCliente,
                        IdContacto = Entity.IdContacto,
                        IdVendedor = Entity.IdVendedor,
                        vt_plazo = Entity.vt_plazo,
                        vt_Observacion = Entity.vt_Observacion,
                        IdPeriodo = Entity.IdPeriodo,
                        vt_tipo_venta = Entity.vt_tipo_venta,
                        IdCaja = Entity.IdCaja,
                        IdPuntoVta = Entity.IdPuntoVta,
                        fecha_primera_cuota = Entity.fecha_primera_cuota,
                        Fecha_Transaccion = Entity.fecha_primera_cuota,
                        Estado = Entity.Estado,
                        esta_impresa = Entity.esta_impresa,
                        valor_abono = Entity.valor_abono
                    };
                    var Entity_fp = Context.fa_factura_x_formaPago.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCbteVta == IdCbteVta).FirstOrDefault();
                    if (Entity_fp != null)
                        info.IdFormaPago = Entity_fp.IdFormaPago;
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal get_id(int IdEmpresa, int IdSucursal, int IdBodega)
        {
            try
            {
                decimal ID = 1;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_factura
                              where q.IdEmpresa == IdEmpresa
                              && q.IdSucursal == IdSucursal
                              && q.IdBodega == IdBodega
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCbteVta) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_factura_Info info)
        {
            Entities_facturacion db_f = new Entities_facturacion();
            try
            {
                #region Variables
                int secuencia = 1;
                in_Ing_Egr_Inven_Data data_inv = new in_Ing_Egr_Inven_Data();
                ct_cbtecble_Data data_ct = new ct_cbtecble_Data();
                #endregion

                #region Factura

                #region Cabecera
                db_f.fa_factura.Add(new fa_factura
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdBodega = info.IdBodega,
                    IdCbteVta = info.IdCbteVta = get_id(info.IdEmpresa, info.IdSucursal, info.IdBodega),
                    CodCbteVta = info.CodCbteVta,
                    vt_tipoDoc = info.vt_tipoDoc,
                    vt_serie1 = info.vt_serie1,
                    vt_serie2 = info.vt_serie2,
                    vt_NumFactura = info.vt_NumFactura,
                    Fecha_Autorizacion = info.Fecha_Autorizacion,
                    vt_anio = info.vt_anio,
                    vt_autorizacion = info.vt_autorizacion,
                    vt_fecha = info.vt_fecha.Date,
                    vt_fech_venc = info.vt_fech_venc.Date,
                    vt_mes = info.vt_mes,
                    IdCliente = info.IdCliente,
                    IdContacto = info.IdContacto,
                    IdVendedor = info.IdVendedor,
                    vt_plazo = info.vt_plazo,
                    vt_Observacion = string.IsNullOrEmpty(info.vt_Observacion) ? "" : info.vt_Observacion,
                    IdPeriodo = info.IdPeriodo,
                    vt_tipo_venta = info.vt_tipo_venta,
                    IdCaja = info.IdCaja,
                    IdPuntoVta = info.IdPuntoVta,
                    fecha_primera_cuota = info.fecha_primera_cuota,
                    Fecha_Transaccion = DateTime.Now,
                    Estado = info.Estado = "A",
                    esta_impresa = info.esta_impresa,
                    valor_abono = info.valor_abono,
                    IdUsuario = info.IdUsuario
                });
                #endregion

                #region Detalle
                foreach (var item in info.lst_det)
                {
                    db_f.fa_factura_det.Add(new fa_factura_det
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdCbteVta = info.IdCbteVta,
                        Secuencia = item.Secuencia = secuencia++,

                        IdProducto = item.IdProducto,
                        vt_cantidad = item.vt_cantidad,
                        vt_Precio = item.vt_Precio,
                        vt_PorDescUnitario = item.vt_PorDescUnitario,
                        vt_DescUnitario = item.vt_DescUnitario,
                        vt_PrecioFinal = item.vt_PrecioFinal,
                        vt_Subtotal = item.vt_Subtotal,
                        vt_por_iva = item.vt_por_iva,
                        IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                        vt_iva = item.vt_iva,
                        vt_total = item.vt_total,
                        vt_estado = item.vt_estado = "A",

                        IdEmpresa_pf = item.IdEmpresa_pf,
                        IdSucursal_pf = item.IdSucursal_pf,
                        IdProforma = item.IdProforma,
                        Secuencia_pf = item.Secuencia_pf,

                        IdCentroCosto = item.IdCentroCosto,
                        IdCentroCosto_sub_centro_costo = item.IdCentroCosto_sub_centro_costo,
                        IdPunto_Cargo = item.IdPunto_Cargo,
                        IdPunto_cargo_grupo = item.IdPunto_cargo_grupo
                    });
                }
                #endregion

                #region Forma de pago
                db_f.fa_factura_x_formaPago.Add(new fa_factura_x_formaPago
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdBodega = info.IdBodega,
                    IdCbteVta = info.IdCbteVta,
                    IdFormaPago = info.IdFormaPago,
                    observacion = "FACT# " + info.vt_serie1 + "-" + info.vt_serie2 + "-" + info.vt_NumFactura
                });
                #endregion

                #region Cuotas
                secuencia = 1;
                foreach (var item in info.lst_cuota)
                {
                    db_f.fa_cuotas_x_doc.Add(new fa_cuotas_x_doc
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdCbteVta = info.IdCbteVta,
                        secuencia = secuencia++,

                        Estado = item.Estado,
                        fecha_vcto_cuota = item.fecha_vcto_cuota.Date,
                        num_cuota = item.num_cuota,
                        valor_a_cobrar = item.valor_a_cobrar
                    });
                }
                #endregion

                #endregion

                var contacto = db_f.fa_cliente_contactos.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente && q.IdContacto == info.IdContacto).FirstOrDefault();

                db_f.SaveChanges();

                #region Talonario
                using (Entities_general Context = new Entities_general())
                {
                    var talonario = Context.tb_sis_Documento_Tipo_Talonario.Where(q => q.IdEmpresa == info.IdEmpresa && q.CodDocumentoTipo == info.vt_tipoDoc && q.Establecimiento == info.vt_serie1 && q.PuntoEmision == info.vt_serie2 && q.NumDocumento == info.vt_NumFactura).FirstOrDefault();
                    if (talonario != null)
                        talonario.Usado = true;
                    Context.SaveChanges();
                }
                #endregion

                #region Inventario
                var parametro = db_f.fa_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                in_Ing_Egr_Inven_Info movimiento = armar_movi_inven(info, Convert.ToInt32(parametro.IdMovi_inven_tipo_Factura), contacto == null ? "" : contacto.Nombres);
                if (movimiento != null)
                    if (data_inv.guardarDB(movimiento, "-"))
                    {
                        db_f.fa_factura_x_in_Ing_Egr_Inven.Add(new fa_factura_x_in_Ing_Egr_Inven
                        {
                            IdEmpresa_fa = info.IdEmpresa,
                            IdSucursal_fa = info.IdSucursal,
                            IdBodega_fa = info.IdBodega,
                            IdCbteVta_fa = info.IdCbteVta,

                            IdEmpresa_in_eg_x_inv = movimiento.IdEmpresa,
                            IdSucursal_in_eg_x_inv = movimiento.IdSucursal,
                            IdMovi_inven_tipo_in_eg_x_inv = movimiento.IdMovi_inven_tipo,
                            IdNumMovi_in_eg_x_inv = movimiento.IdNumMovi,
                        });

                        foreach (var item in movimiento.lst_in_Ing_Egr_Inven_det)
                        {
                            db_f.fa_factura_det_x_in_Ing_Egr_Inven_det.Add(new fa_factura_det_x_in_Ing_Egr_Inven_det
                            {
                                IdEmpresa_fa = info.IdEmpresa,
                                IdSucursal_fa = info.IdSucursal,
                                IdBodega_fa = info.IdBodega,
                                IdCbteVta_fa = info.IdCbteVta,
                                Secuencia_fa = item.RelacionDetalleFactura.Secuencia_fa,

                                IdEmpresa_eg = movimiento.IdEmpresa,
                                IdSucursal_eg = movimiento.IdSucursal,
                                IdMovi_inven_tipo_eg = movimiento.IdMovi_inven_tipo,
                                IdNumMovi_eg = movimiento.IdNumMovi,
                                Secuencia_eg = item.Secuencia
                            });
                        }

                        db_f.SaveChanges();
                    }
                #endregion

                #region Contabilidad
                var cliente = db_f.fa_cliente.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente).FirstOrDefault();
                if (!string.IsNullOrEmpty(cliente.IdCtaCble_cxc_Credito))
                {
                    ct_cbtecble_Info diario = armar_diario(info, Convert.ToInt32(parametro.IdTipoCbteCble_Factura), cliente.IdCtaCble_cxc_Credito, parametro.pa_IdCtaCble_descuento, contacto == null ? "" : contacto.Nombres);
                    if (diario != null)
                        if (data_ct.guardarDB(diario))
                        {
                            db_f.fa_factura_x_ct_cbtecble.Add(new fa_factura_x_ct_cbtecble
                            {
                                vt_IdEmpresa = info.IdEmpresa,
                                vt_IdSucursal = info.IdSucursal,
                                vt_IdBodega = info.IdBodega,
                                vt_IdCbteVta = info.IdCbteVta,

                                ct_IdEmpresa = diario.IdEmpresa,
                                ct_IdTipoCbte = diario.IdTipoCbte,
                                ct_IdCbteCble = diario.IdCbteCble,
                            });
                            db_f.SaveChanges();
                        }
                }
                #endregion

                db_f.Dispose();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ct_cbtecble_Info armar_diario(fa_factura_Info info, int IdTipoCbte, string IdCtaCble_Cliente, string IdCtaCble_Dscto, string nomContacto)
        {
            try
            {
                #region Variables
                string IdCtaCble_VentasIVA = string.Empty;
                string IdCtaCble_Ventas0 = string.Empty;
                string IdCtaCble_IVA = string.Empty;
                #endregion

                #region Validar cuentas

                using (Entities_general Context = new Entities_general())
                {
                    var porcentajes = (from q in info.lst_det
                                             group q by new { q.IdCod_Impuesto_Iva} into g
                                             select g.Key).ToList();

                    foreach (var item in porcentajes)
                    {
                        var impuesto = Context.tb_sis_Impuesto_x_ctacble.Include("tb_sis_Impuesto").Where(q => q.IdEmpresa_cta == info.IdEmpresa && q.IdCod_Impuesto == item.IdCod_Impuesto_Iva).FirstOrDefault();
                        if (impuesto != null)
                        {
                            if (impuesto.tb_sis_Impuesto.porcentaje > 0)
                            {
                                IdCtaCble_VentasIVA = impuesto.IdCtaCble_vta;
                                IdCtaCble_IVA = impuesto.IdCtaCble;
                            }else
                            {
                                IdCtaCble_Ventas0 = impuesto.IdCtaCble_vta;
                            }
                        }                            
                    }                    
                }

                #endregion

                ct_cbtecble_Info diario = new ct_cbtecble_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdTipoCbte = IdTipoCbte,
                    IdCbteCble = 0,
                    cb_Fecha = info.vt_fecha.Date,
                    cb_Anio = info.vt_fecha.Date.Year,
                    cb_mes = info.vt_fecha.Date.Month,
                    IdPeriodo = info.IdPeriodo,
                    IdUsuario = info.IdUsuario,
                    IdUsuarioUltModi = info.IdUsuarioUltModi,
                    cb_Observacion = "FACT# " + info.vt_serie1 + "-" + info.vt_serie2 + "-" + info.vt_NumFactura + " " + "CLIENTE: " + nomContacto + " " + info.vt_Observacion,
                    CodCbteCble = "FACT# " + info.vt_NumFactura,
                    cb_Valor = 0,
                    lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>()
                };
                int secuencia = 1;

                #region Ventas con IVA
                if (!string.IsNullOrEmpty(IdCtaCble_VentasIVA))
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_VentasIVA,
                        dc_Valor = string.IsNullOrEmpty(IdCtaCble_Dscto) ? (Math.Round(info.lst_det.Where(q => q.vt_por_iva > 0).Sum(q => q.vt_Subtotal), 2, MidpointRounding.AwayFromZero) * -1) : (Math.Round(info.lst_det.Where(q => q.vt_por_iva > 0).Sum(q => q.vt_cantidad * q.vt_Precio), 2, MidpointRounding.AwayFromZero) * -1)
                    });
                #endregion

                #region Ventas IVA 0
                if (!string.IsNullOrEmpty(IdCtaCble_Ventas0))
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_Ventas0,
                        dc_Valor = string.IsNullOrEmpty(IdCtaCble_Dscto) ? (Math.Round(info.lst_det.Where(q => q.vt_por_iva == 0).Sum(q => q.vt_Subtotal), 2, MidpointRounding.AwayFromZero) * -1) : (Math.Round(info.lst_det.Where(q => q.vt_por_iva == 0).Sum(q => q.vt_cantidad * q.vt_Precio), 2, MidpointRounding.AwayFromZero) * -1)
                    });
                #endregion

                #region IVA
                if (!string.IsNullOrEmpty(IdCtaCble_IVA))
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_IVA,
                        dc_Valor = Math.Round(info.lst_det.Where(q => q.vt_por_iva > 0).Sum(q => q.vt_iva), 2, MidpointRounding.AwayFromZero) * -1
                    });
                #endregion

                #region Cliente
                if (!string.IsNullOrEmpty(IdCtaCble_Cliente))
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_Cliente,
                        dc_Valor = Math.Round(info.lst_det.Sum(q => q.vt_total), 2, MidpointRounding.AwayFromZero)
                    });
                #endregion

                #region Descuento
                if (!string.IsNullOrEmpty(IdCtaCble_Dscto))
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_Dscto,
                        dc_Valor = Math.Round(info.lst_det.Sum(q => q.vt_cantidad * q.vt_DescUnitario), 2, MidpointRounding.AwayFromZero)
                    });
                #endregion

                if (info.lst_det.Count == 0)
                    return null;

                diario.lst_ct_cbtecble_det.RemoveAll(q=>q.dc_Valor == 0);

                if (diario.lst_ct_cbtecble_det.Count == 0)
                    return null;                

                if (diario.lst_ct_cbtecble_det.Where(q=>q.dc_Valor == 0).Count() > 0)
                    return null;

                double descuadre = Math.Round(diario.lst_ct_cbtecble_det.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero);
                if (descuadre < -0.02 || 0.02 <= descuadre)
                    return null;

                if (descuadre <= 0.02 || -0.02 <= descuadre && descuadre != 0)
                {
                    if (descuadre > 0)
                        diario.lst_ct_cbtecble_det.Where(q => q.dc_Valor < 0).FirstOrDefault().dc_Valor -= descuadre;
                    else
                        diario.lst_ct_cbtecble_det.Where(q => q.dc_Valor > 0).FirstOrDefault().dc_Valor += (descuadre*-1);
                }

                descuadre = Math.Round(diario.lst_ct_cbtecble_det.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero);
                if (descuadre != 0)
                    return null;

                return diario;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public in_Ing_Egr_Inven_Info armar_movi_inven(fa_factura_Info info, int IdMoviInven_tipo, string nomContacto)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var motivo = Context.in_Motivo_Inven.Where(q => q.IdEmpresa == info.IdEmpresa && q.Tipo_Ing_Egr == "EGR" && q.Genera_Movi_Inven == "S").FirstOrDefault();
                    if (motivo == null)
                        return null;

                    var tipo = Context.in_movi_inven_tipo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMovi_inven_tipo == IdMoviInven_tipo).FirstOrDefault();
                    if (tipo == null)
                        return null;

                    int secuencia = 1;

                    in_Ing_Egr_Inven_Info movimiento = new in_Ing_Egr_Inven_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdMovi_inven_tipo = IdMoviInven_tipo,
                        IdNumMovi = 0,
                        cm_fecha = info.vt_fecha.Date,
                        cm_observacion = "FACT# " + info.vt_serie1 + "-" + info.vt_serie2 + "-" + info.vt_NumFactura + " " + "CLIENTE: "+nomContacto+" "+ info.vt_Observacion,
                        IdUsuario = info.IdUsuario,
                        IdUsuarioUltModi = info.IdUsuarioUltModi,
                        IdMotivo_Inv = motivo.IdMotivo_Inv,
                        signo = "-",
                        CodMoviInven = "FACT# " + info.vt_NumFactura,
                        lst_in_Ing_Egr_Inven_det = new List<in_Ing_Egr_Inven_det_Info>()
                    };
                    foreach (var item in info.lst_det)
                    {                        
                        var lst = Context.in_Producto_Composicion.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdProductoPadre == item.IdProducto).ToList();
                        if (lst.Count == 0)
                        {
                            var producto = (from p in Context.in_Producto
                                           join t in Context.in_ProductoTipo
                                           on new { p.IdEmpresa, p.IdProductoTipo } equals new { t.IdEmpresa, t.IdProductoTipo }
                                           where p.IdEmpresa == info.IdEmpresa && p.IdProducto == item.IdProducto
                                           && t.tp_ManejaInven == "S"
                                           select p).FirstOrDefault();

                            if (producto != null)
                            {
                                movimiento.lst_in_Ing_Egr_Inven_det.Add(new in_Ing_Egr_Inven_det_Info
                                {
                                    IdEmpresa = movimiento.IdEmpresa,
                                    IdSucursal = movimiento.IdSucursal,
                                    IdBodega = (int)movimiento.IdBodega,
                                    IdMovi_inven_tipo = movimiento.IdMovi_inven_tipo,
                                    IdNumMovi = 0,
                                    Secuencia = secuencia++,
                                    IdProducto = item.IdProducto,
                                    dm_cantidad = item.vt_cantidad * -1,
                                    dm_cantidad_sinConversion = item.vt_cantidad*-1,
                                    mv_costo = 0,
                                    mv_costo_sinConversion = 0,
                                    IdUnidadMedida = producto.IdUnidadMedida_Consumo,
                                    IdUnidadMedida_sinConversion = producto.IdUnidadMedida_Consumo,

                                    //FK Factura detalle x egreso detalle
                                    RelacionDetalleFactura = new fa_factura_det_x_in_Ing_Egr_Inven_det_Info
                                    {
                                        IdEmpresa_eg = movimiento.IdEmpresa,
                                        IdSucursal_eg = movimiento.IdSucursal,
                                        IdMovi_inven_tipo_eg = movimiento.IdMovi_inven_tipo,
                                        IdNumMovi_eg = 0,
                                        IdEmpresa_fa = item.IdEmpresa,
                                        IdSucursal_fa = item.IdSucursal,
                                        IdBodega_fa = item.IdBodega,
                                        IdCbteVta_fa = item.IdCbteVta,
                                        Secuencia_fa = item.Secuencia
                                    }
                                });
                            }
                        }else
                        {
                            foreach (var comp in lst)
                            {
                                var producto = (from p in Context.in_Producto
                                                join t in Context.in_ProductoTipo
                                                on new { p.IdEmpresa, p.IdProductoTipo } equals new { t.IdEmpresa, t.IdProductoTipo }
                                                where p.IdEmpresa == info.IdEmpresa && p.IdProducto == item.IdProducto
                                                && t.tp_ManejaInven == "S"
                                                select p).FirstOrDefault();

                                if (producto != null)
                                {
                                    movimiento.lst_in_Ing_Egr_Inven_det.Add(new in_Ing_Egr_Inven_det_Info
                                    {
                                        IdEmpresa = movimiento.IdEmpresa,
                                        IdSucursal = movimiento.IdSucursal,
                                        IdBodega = (int)movimiento.IdBodega,
                                        IdMovi_inven_tipo = movimiento.IdMovi_inven_tipo,
                                        IdNumMovi = 0,
                                        Secuencia = secuencia++,
                                        IdProducto = comp.IdProductoHijo,
                                        dm_cantidad = item.vt_cantidad * -1,
                                        dm_cantidad_sinConversion = item.vt_cantidad * -1,
                                        mv_costo = 0,
                                        mv_costo_sinConversion = 0,
                                        IdUnidadMedida = producto.IdUnidadMedida_Consumo,
                                        IdUnidadMedida_sinConversion = producto.IdUnidadMedida_Consumo,

                                        //FK Factura detalle x egreso detalle
                                        RelacionDetalleFactura = new fa_factura_det_x_in_Ing_Egr_Inven_det_Info
                                        {
                                            IdEmpresa_eg = movimiento.IdEmpresa,
                                            IdSucursal_eg = movimiento.IdSucursal,
                                            IdMovi_inven_tipo_eg = movimiento.IdMovi_inven_tipo,
                                            IdNumMovi_eg = 0,
                                            IdEmpresa_fa = item.IdEmpresa,
                                            IdSucursal_fa = item.IdSucursal,
                                            IdBodega_fa = item.IdBodega,
                                            IdCbteVta_fa = item.IdCbteVta,
                                            Secuencia_fa = item.Secuencia
                                        }
                                    });
                                }
                            }
                        }
                    }
                    if (movimiento.lst_in_Ing_Egr_Inven_det.Count == 0)
                        return null;
                    return movimiento;
                }                         
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(fa_factura_Info info)
        {
            Entities_facturacion db_f = new Entities_facturacion();
            try
            {
                #region Variables
                int secuencia = 1;
                in_Ing_Egr_Inven_Data data_inv = new in_Ing_Egr_Inven_Data();
                ct_cbtecble_Data data_ct = new ct_cbtecble_Data();
                #endregion

                #region Factura

                #region Cabecera
                fa_factura Entity = db_f.fa_factura.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdCbteVta == info.IdCbteVta);
                if (Entity == null) return false;

                Entity.vt_anio = info.vt_anio;
                Entity.vt_fecha = info.vt_fecha.Date;
                Entity.vt_fech_venc = info.vt_fech_venc.Date;
                Entity.vt_mes = info.vt_mes;
                Entity.IdCliente = info.IdCliente;
                Entity.IdContacto = info.IdContacto;
                Entity.IdVendedor = info.IdVendedor;
                Entity.vt_plazo = info.vt_plazo;
                Entity.vt_Observacion = string.IsNullOrEmpty(info.vt_Observacion) ? "" : info.vt_Observacion;
                Entity.IdPeriodo = info.IdPeriodo;
                Entity.vt_tipo_venta = info.vt_tipo_venta;
                Entity.fecha_primera_cuota = info.fecha_primera_cuota;
                Entity.valor_abono = info.valor_abono;

                Entity.IdUsuarioUltModi = info.IdUsuarioUltModi;
                Entity.Fecha_UltMod = DateTime.Now;


                #endregion
                var contacto = db_f.fa_cliente_contactos.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente && q.IdContacto == info.IdContacto).FirstOrDefault();
                #region Detalle
                var lst_det = db_f.fa_factura_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdCbteVta == info.IdCbteVta).ToList();
                db_f.fa_factura_det.RemoveRange(lst_det);

                foreach (var item in info.lst_det)
                {
                    db_f.fa_factura_det.Add(new fa_factura_det
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdCbteVta = info.IdCbteVta,
                        Secuencia = item.Secuencia = secuencia++,

                        IdProducto = item.IdProducto,
                        vt_cantidad = item.vt_cantidad,
                        vt_Precio = item.vt_Precio,
                        vt_PorDescUnitario = item.vt_PorDescUnitario,
                        vt_DescUnitario = item.vt_DescUnitario,
                        vt_PrecioFinal = item.vt_PrecioFinal,
                        vt_Subtotal = item.vt_Subtotal,
                        vt_por_iva = item.vt_por_iva,
                        IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                        vt_iva = item.vt_iva,
                        vt_total = item.vt_total,
                        vt_estado = item.vt_estado = "A",

                        IdEmpresa_pf = item.IdEmpresa_pf,
                        IdSucursal_pf = item.IdSucursal_pf,
                        IdProforma = item.IdProforma,
                        Secuencia_pf = item.Secuencia_pf,

                        IdCentroCosto = item.IdCentroCosto,
                        IdCentroCosto_sub_centro_costo = item.IdCentroCosto_sub_centro_costo,
                        IdPunto_Cargo = item.IdPunto_Cargo,
                        IdPunto_cargo_grupo = item.IdPunto_cargo_grupo
                    });
                }
                #endregion

                #region Forma de pago
                var fp = db_f.fa_factura_x_formaPago.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdCbteVta == info.IdCbteVta).FirstOrDefault();
                db_f.fa_factura_x_formaPago.Remove(fp);
                db_f.fa_factura_x_formaPago.Add(new fa_factura_x_formaPago
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdBodega = info.IdBodega,
                    IdCbteVta = info.IdCbteVta,
                    IdFormaPago = info.IdFormaPago,
                    observacion = "FACT# " + info.vt_serie1 + "-" + info.vt_serie2 + "-" + info.vt_NumFactura
                });
                #endregion

                #region Cuotas
                var lst_cuotas = db_f.fa_cuotas_x_doc.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdCbteVta == info.IdCbteVta).ToList();
                db_f.fa_cuotas_x_doc.RemoveRange(lst_cuotas);

                secuencia = 1;
                foreach (var item in info.lst_cuota)
                {
                    db_f.fa_cuotas_x_doc.Add(new fa_cuotas_x_doc
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdCbteVta = info.IdCbteVta,
                        secuencia = secuencia++,

                        Estado = item.Estado,
                        fecha_vcto_cuota = item.fecha_vcto_cuota.Date,
                        num_cuota = item.num_cuota,
                        valor_a_cobrar = item.valor_a_cobrar
                    });
                }
                #endregion

                #endregion
                db_f.SaveChanges();

                #region Inventario
                var parametro = db_f.fa_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                var egr = db_f.fa_factura_x_in_Ing_Egr_Inven.Where(q => q.IdEmpresa_fa == info.IdEmpresa && q.IdSucursal_fa == info.IdSucursal && q.IdBodega_fa == info.IdBodega && q.IdCbteVta_fa == info.IdCbteVta).FirstOrDefault();
                if (egr == null)
                {
                    in_Ing_Egr_Inven_Info movimiento = armar_movi_inven(info, Convert.ToInt32(parametro.IdMovi_inven_tipo_Factura), contacto == null ? "" : contacto.Nombres);
                    if (movimiento != null)
                    {
                        if (data_inv.guardarDB(movimiento, "-"))
                        {
                            db_f.fa_factura_x_in_Ing_Egr_Inven.Add(new fa_factura_x_in_Ing_Egr_Inven
                            {
                                IdEmpresa_fa = info.IdEmpresa,
                                IdSucursal_fa = info.IdSucursal,
                                IdBodega_fa = info.IdBodega,
                                IdCbteVta_fa = info.IdCbteVta,

                                IdEmpresa_in_eg_x_inv = movimiento.IdEmpresa,
                                IdSucursal_in_eg_x_inv = movimiento.IdSucursal,
                                IdMovi_inven_tipo_in_eg_x_inv = movimiento.IdMovi_inven_tipo,
                                IdNumMovi_in_eg_x_inv = movimiento.IdNumMovi,
                            });

                            var lstegr = db_f.fa_factura_det_x_in_Ing_Egr_Inven_det.Where(q => q.IdEmpresa_fa == info.IdEmpresa && q.IdSucursal_fa == info.IdSucursal && q.IdBodega_fa == info.IdBodega && q.IdCbteVta_fa == info.IdCbteVta).ToList();
                            db_f.fa_factura_det_x_in_Ing_Egr_Inven_det.RemoveRange(lstegr);

                            foreach (var item in movimiento.lst_in_Ing_Egr_Inven_det)
                            {
                                db_f.fa_factura_det_x_in_Ing_Egr_Inven_det.Add(new fa_factura_det_x_in_Ing_Egr_Inven_det
                                {
                                    IdEmpresa_fa = info.IdEmpresa,
                                    IdSucursal_fa = info.IdSucursal,
                                    IdBodega_fa = info.IdBodega,
                                    IdCbteVta_fa = info.IdCbteVta,
                                    Secuencia_fa = item.RelacionDetalleFactura.Secuencia_fa,

                                    IdEmpresa_eg = movimiento.IdEmpresa,
                                    IdSucursal_eg = movimiento.IdSucursal,
                                    IdMovi_inven_tipo_eg = movimiento.IdMovi_inven_tipo,
                                    IdNumMovi_eg = movimiento.IdNumMovi,
                                    Secuencia_eg = item.Secuencia
                                });
                            }
                            db_f.SaveChanges();
                        }
                    }
                }
                else
                {

                    in_Ing_Egr_Inven_Info movimiento = armar_movi_inven(info, Convert.ToInt32(parametro.IdMovi_inven_tipo_Factura), contacto == null ? "" : contacto.Nombres);
                    if (movimiento != null)
                    {
                        movimiento.IdNumMovi = egr.IdNumMovi_in_eg_x_inv;
                        if(data_inv.modificarDB(movimiento))
                        {
                            var lstegr = db_f.fa_factura_det_x_in_Ing_Egr_Inven_det.Where(q => q.IdEmpresa_fa == info.IdEmpresa && q.IdSucursal_fa == info.IdSucursal && q.IdBodega_fa == info.IdBodega && q.IdCbteVta_fa == info.IdCbteVta).ToList();
                            db_f.fa_factura_det_x_in_Ing_Egr_Inven_det.RemoveRange(lstegr);

                            foreach (var item in movimiento.lst_in_Ing_Egr_Inven_det)
                            {
                                db_f.fa_factura_det_x_in_Ing_Egr_Inven_det.Add(new fa_factura_det_x_in_Ing_Egr_Inven_det
                                {
                                    IdEmpresa_fa = info.IdEmpresa,
                                    IdSucursal_fa = info.IdSucursal,
                                    IdBodega_fa = info.IdBodega,
                                    IdCbteVta_fa = info.IdCbteVta,
                                    Secuencia_fa = item.RelacionDetalleFactura.Secuencia_fa,

                                    IdEmpresa_eg = movimiento.IdEmpresa,
                                    IdSucursal_eg = movimiento.IdSucursal,
                                    IdMovi_inven_tipo_eg = movimiento.IdMovi_inven_tipo,
                                    IdNumMovi_eg = movimiento.IdNumMovi,
                                    Secuencia_eg = item.Secuencia
                                });
                            }
                            db_f.SaveChanges();
                        }
                    }
                }
                #endregion

                #region Contabilidad
                var cliente = db_f.fa_cliente.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente).FirstOrDefault();
                if (!string.IsNullOrEmpty(cliente.IdCtaCble_cxc_Credito))
                {
                    var conta = db_f.fa_factura_x_ct_cbtecble.Where(q => q.vt_IdEmpresa == info.IdEmpresa && q.vt_IdSucursal == info.IdSucursal && q.vt_IdBodega == info.IdBodega && q.vt_IdCbteVta == info.IdCbteVta).FirstOrDefault();
                    if (conta == null)
                    {
                        ct_cbtecble_Info diario = armar_diario(info, Convert.ToInt32(parametro.IdTipoCbteCble_Factura), cliente.IdCtaCble_cxc_Credito, parametro.pa_IdCtaCble_descuento, contacto == null ? "" : contacto.Nombres);
                        if (diario != null)
                        {
                            if (data_ct.guardarDB(diario))
                            {
                                db_f.fa_factura_x_ct_cbtecble.Add(new fa_factura_x_ct_cbtecble
                                {
                                    vt_IdEmpresa = info.IdEmpresa,
                                    vt_IdSucursal = info.IdSucursal,
                                    vt_IdBodega = info.IdBodega,
                                    vt_IdCbteVta = info.IdCbteVta,

                                    ct_IdEmpresa = diario.IdEmpresa,
                                    ct_IdTipoCbte = diario.IdTipoCbte,
                                    ct_IdCbteCble = diario.IdCbteCble,
                                });
                                db_f.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        ct_cbtecble_Info diario = armar_diario(info, Convert.ToInt32(parametro.IdTipoCbteCble_Factura), cliente.IdCtaCble_cxc_Credito, parametro.pa_IdCtaCble_descuento, contacto == null ? "" : contacto.Nombres);
                        if (diario != null)
                        {
                            diario.IdCbteCble = conta.ct_IdCbteCble;
                            data_ct.modificarDB(diario);
                        }
                    }
                }
                #endregion

                db_f.Dispose();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarEstadoImpresion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, bool estado_impresion)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var Entity = Context.fa_factura.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCbteVta == IdCbteVta).FirstOrDefault();
                    if (Entity != null)
                    {
                        Entity.esta_impresa = estado_impresion;
                        Context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool anularDB(fa_factura_Info info)
        {
            try
            {
                #region Variables
                ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
                in_Ing_Egr_Inven_Data odata_inv = new in_Ing_Egr_Inven_Data();
                #endregion

                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_factura Entity = Context.fa_factura.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdCbteVta == info.IdCbteVta);
                    if (Entity == null) return false;
                    {
                        Entity.MotivoAnulacion = info.MotivoAnulacion;
                        Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                        Entity.Estado = "I";
                    }
                    
                    var conta = Context.fa_factura_x_ct_cbtecble.Where(q => q.vt_IdEmpresa == info.IdEmpresa && q.vt_IdSucursal == info.IdSucursal && q.vt_IdBodega == info.IdBodega && q.vt_IdCbteVta == info.IdCbteVta).FirstOrDefault();
                    if (conta != null)
                        if (!odata_ct.anularDB(new ct_cbtecble_Info { IdEmpresa = conta.ct_IdEmpresa, IdTipoCbte = conta.ct_IdTipoCbte, IdCbteCble = conta.ct_IdCbteCble, IdUsuarioAnu = info.IdUsuarioUltAnu, cb_MotivoAnu = info.MotivoAnulacion }))
                        {
                            Entity.MotivoAnulacion = null;
                            Entity.IdUsuarioUltAnu = null;
                            Entity.Estado = "A";
                        }

                    var inv = Context.fa_factura_x_in_Ing_Egr_Inven.Where(q => q.IdEmpresa_fa == info.IdEmpresa && q.IdSucursal_fa == info.IdSucursal && q.IdBodega_fa == info.IdBodega && q.IdCbteVta_fa == info.IdCbteVta).FirstOrDefault();
                    if(inv != null)
                        if(!odata_inv.anularDB(new in_Ing_Egr_Inven_Info { IdEmpresa = inv.IdEmpresa_in_eg_x_inv, IdSucursal = inv.IdSucursal_in_eg_x_inv, IdMovi_inven_tipo = inv.IdMovi_inven_tipo_in_eg_x_inv, IdNumMovi = inv.IdNumMovi_in_eg_x_inv, IdusuarioUltAnu = info.IdUsuarioUltAnu, MotivoAnulacion = info.MotivoAnulacion }))
                        {
                            Entity.MotivoAnulacion = null;
                            Entity.IdUsuarioUltAnu = null;
                            Entity.Estado = "A";
                        }

                    Context.SaveChanges();
                }



                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool MostrarCuotasRpt(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var fa = (from f in Context.fa_factura
                              where f.IdEmpresa == IdEmpresa
                              && f.IdSucursal == IdSucursal
                              && f.IdBodega == IdBodega
                              && f.IdCbteVta == IdCbteVta
                             join t in Context.fa_TerminoPago
                             on new { IdTerminoPago = f.vt_tipo_venta } equals new { t.IdTerminoPago }
                             select new
                             {
                                 Num_Coutas = t.Num_Coutas
                             }).FirstOrDefault();
                    if (fa.Num_Coutas > 0)
                        return true;
                }
                return false;
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidarCarteraVencida (int IdEmpresa, decimal IdCliente, ref string mensaje)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    DateTime FechaCorte = DateTime.Now.Date;
                    var cartera = Context.vwcxc_cartera_x_cobrar.Where(q => q.IdEmpresa == IdEmpresa && q.IdCliente == IdCliente && q.vt_fech_venc < FechaCorte && q.Saldo > 0 && q.Estado == "A").ToList();
                    if (cartera.Count > 0)
                    {
                        mensaje = "El cliente "+cartera.First().NomCliente.Trim()+" adeuda $"+Math.Round((double)cartera.Sum(q=>q.Saldo),2,MidpointRounding.AwayFromZero)+" en cartera vencida";
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Contabilizar(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string NombreContacto)
        {
            Entities_facturacion db = new Entities_facturacion();
            ct_cbtecble_Data data_ct = new ct_cbtecble_Data();
            try
            {
                var factura = get_info(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
                if (factura != null)
                {
                    fa_factura_det_Data odata_det = new fa_factura_det_Data();
                    factura.lst_det = odata_det.get_list(factura.IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
                }
                var parametro = db.fa_parametro.Where(q => q.IdEmpresa == factura.IdEmpresa).FirstOrDefault();
                var cliente = db.fa_cliente.Where(q => q.IdEmpresa == factura.IdEmpresa && q.IdCliente == factura.IdCliente).FirstOrDefault();
                if (!string.IsNullOrEmpty(cliente.IdCtaCble_cxc_Credito))
                {
                    var conta = db.fa_factura_x_ct_cbtecble.Where(q => q.vt_IdEmpresa == factura.IdEmpresa && q.vt_IdSucursal == factura.IdSucursal && q.vt_IdBodega == factura.IdBodega && q.vt_IdCbteVta == factura.IdCbteVta).FirstOrDefault();
                    if (conta == null)
                    {
                        ct_cbtecble_Info diario = armar_diario(factura, Convert.ToInt32(parametro.IdTipoCbteCble_Factura), cliente.IdCtaCble_cxc_Credito, parametro.pa_IdCtaCble_descuento, NombreContacto);
                        if (diario != null)
                        {
                            if (data_ct.guardarDB(diario))
                            {
                                db.fa_factura_x_ct_cbtecble.Add(new fa_factura_x_ct_cbtecble
                                {
                                    vt_IdEmpresa = factura.IdEmpresa,
                                    vt_IdSucursal = factura.IdSucursal,
                                    vt_IdBodega = factura.IdBodega,
                                    vt_IdCbteVta = factura.IdCbteVta,

                                    ct_IdEmpresa = diario.IdEmpresa,
                                    ct_IdTipoCbte = diario.IdTipoCbte,
                                    ct_IdCbteCble = diario.IdCbteCble,
                                });
                                db.SaveChanges();
                                return true;
                            }
                        }
                    }
                    else
                    {
                        ct_cbtecble_Info diario = armar_diario(factura, Convert.ToInt32(parametro.IdTipoCbteCble_Factura), cliente.IdCtaCble_cxc_Credito, parametro.pa_IdCtaCble_descuento, NombreContacto);
                        if (diario != null)
                        {
                            diario.IdCbteCble = conta.ct_IdCbteCble;
                            data_ct.modificarDB(diario);
                            return true;
                        }
                    }
                }

                db.Dispose();
                return false;
            }
            catch (Exception)
            {
                db.Dispose();
                throw;
            }
        }

        public bool ValidarDocumentoAnulacion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string vt_tipoDoc, ref string mensaje)
        {
            try
            {
                using (Entities_cuentas_por_cobrar db = new Entities_cuentas_por_cobrar())
                {
                    var obj = db.cxc_cobro_det.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega_Cbte == IdBodega && q.IdCbte_vta_nota == IdCbteVta && q.dc_TipoDocumento == vt_tipoDoc && q.estado == "A").Count();
                    if (obj > 0)
                    {
                        mensaje = "El documento no puede ser anulado porque se encuentra parcial o totalmente cobrado";
                        return false;
                    }
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
