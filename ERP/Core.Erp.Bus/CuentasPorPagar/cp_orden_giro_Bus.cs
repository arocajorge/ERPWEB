﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Helps;

namespace Core.Erp.Bus.CuentasPorPagar
{
   public class cp_orden_giro_Bus
    {
        cp_orden_giro_Data data = new cp_orden_giro_Data();
        ct_cbtecble_Bus bus_contabilidad = new ct_cbtecble_Bus();
        cp_cuotas_x_doc_Bus bus_cuotas = new cp_cuotas_x_doc_Bus();
        ct_cbtecble_det_Bus bus_comrpbante_det = new ct_cbtecble_det_Bus();
        cp_orden_pago_Bus bus_op;
        cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
        cp_parametros_Info info_parametro = new cp_parametros_Info();
        cp_parametros_Bus bus_parametro = new cp_parametros_Bus();
        cp_orden_giro_pagos_sri_Bus bus_forma_pago = new cp_orden_giro_pagos_sri_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        public List<cp_orden_giro_Info> get_lst(int IdEmpresa,int IdSucursal, DateTime fi, DateTime ff)
        {
            try
            {
                return data.get_lst(IdEmpresa,IdSucursal, fi,ff);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<cp_orden_giro_Info> get_lst_sin_ret(int IdEmpresa, DateTime fi, DateTime ff)
        {
            try
            {
                return data.get_lst_sin_ret(IdEmpresa, fi, ff);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<cp_orden_giro_Info> get_lst_orden_giro_x_pagar(int IdEmpresa)
        {
            try
            {
                return data.get_lst_orden_giro_x_pagar(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(cp_orden_giro_Info info)
        {
            try
            {
                var prov = bus_proveedor.get_info(info.IdEmpresa, info.IdProveedor);
                info.co_baseImponible = info.co_subtotal_iva + info.co_subtotal_siniva;
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.cb_Fecha =(DateTime) info.co_FechaContabilizacion;
                info.info_comrobante.cb_Anio = info.info_comrobante.cb_Fecha.Year;
                info.info_comrobante.cb_mes = info.info_comrobante.cb_Fecha.Month;
                info.info_comrobante.cb_Estado = "A";
                info.info_comrobante.IdPeriodo =Convert.ToInt32( info.info_comrobante.cb_Fecha.Year.ToString() + info.info_comrobante.cb_Fecha.Month.ToString().PadLeft(2,'0'));
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                if (prov != null)
                {
                    if (info.co_observacion == null)
                        info.co_observacion = "";
                    info.info_comrobante.cb_Observacion ="Prov: "+ prov.info_persona.pe_nombreCompleto + " FAC# " + info.co_serie + "-" + info.co_factura + " OBS: " + info.co_observacion;
                    
                }
                else
                    info.info_comrobante.cb_Observacion = info.co_observacion;
                info.co_valorpagar = info.co_total;
                if (info.info_cuota.Total_a_pagar == 0)
                    info.co_FechaFactura_vct = info.co_FechaFactura;
                else
                    info.co_FechaFactura_vct = info.co_FechaFactura_vct;
                info.co_fechaOg = info.co_FechaFactura;
                if (bus_contabilidad.guardarDB(info.info_comrobante))
                {
                    data = new cp_orden_giro_Data();
                    info.IdTipoCbte_Ogiro = info.info_comrobante.IdTipoCbte;
                    info.IdCbteCble_Ogiro = info.info_comrobante.IdCbteCble;
                    data.guardarDB(info);
                }

                if(info.info_cuota.Dias_plazo!=0
                    & info.info_cuota.Total_a_pagar!=0
                    & info.info_cuota.lst_cuotas_det.Count()>0
                  )
                {
                    bus_cuotas = new cp_cuotas_x_doc_Bus();
                    info.info_cuota.IdEmpresa = info.IdEmpresa;
                    info.info_cuota.IdTipoCbte = info.info_comrobante.IdTipoCbte;
                    info.info_cuota.IdCbteCble = info.info_comrobante.IdCbteCble;
                    info.info_cuota.Observacion = info.co_observacion;
                    info.info_cuota.Estado = true;
                    if (info.info_cuota.Fecha_inicio.Year == 1)
                        info.info_cuota.Fecha_inicio = info.co_FechaFactura;
                    bus_cuotas.GuardarDB(info.info_cuota);
                }


                if(info.info_forma_pago.codigo_pago_sri!=null)
                {
                    info.info_forma_pago.IdEmpresa = info.IdEmpresa;
                    info.info_forma_pago.IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro;
                    info.info_forma_pago.IdCbteCble_Ogiro = info.IdCbteCble_Ogiro;
                    info.info_forma_pago.formas_pago_sri = "";
                    bus_forma_pago.GuardarDB(info.info_forma_pago);
                }
                return true;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool modificarDB(cp_orden_giro_Info info)
        {
            try
            {
                info.co_baseImponible = info.co_subtotal_iva + info.co_subtotal_siniva;
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.IdTipoCbte = info.IdTipoCbte_Ogiro;
                info.info_comrobante.IdCbteCble = info.IdCbteCble_Ogiro;
                info.info_comrobante.cb_Fecha = (DateTime)info.co_FechaContabilizacion;
                info.info_comrobante.cb_Anio = info.info_comrobante.cb_Fecha.Year;
                info.info_comrobante.cb_mes = info.info_comrobante.cb_Fecha.Month;
                info.info_comrobante.cb_Estado = "A";
                info.info_comrobante.IdPeriodo = Convert.ToInt32(info.info_comrobante.cb_Fecha.Year.ToString() + info.info_comrobante.cb_Fecha.Month.ToString().PadLeft(2, '0'));
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
               
                    info.info_comrobante.cb_Observacion = info.co_observacion;

                info.co_valorpagar = info.co_total;
                if (info.info_cuota.Total_a_pagar == 0)
                    info.co_FechaFactura_vct = info.co_FechaFactura;
                else
                {
                    info.co_FechaFactura_vct = info.co_FechaFactura.AddDays(info.co_plazo);
                }
                info.co_fechaOg = info.co_FechaFactura;
                if (bus_contabilidad.modificarDB(info.info_comrobante))
                {
                    data = new cp_orden_giro_Data();
                    info.IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro;
                    info.IdCbteCble_Ogiro = info.IdCbteCble_Ogiro;
                    data.modificarDB(info);
                }

                if (info.info_cuota.Dias_plazo != 0
                    & info.info_cuota.Total_a_pagar != 0
                    & info.info_cuota.lst_cuotas_det.Count() > 0
                  )
                {
                    info.info_cuota.IdEmpresa = info.IdEmpresa;
                    info.info_cuota.IdTipoCbte = info.IdTipoCbte_Ogiro;
                    info.info_cuota.IdCbteCble = info.IdCbteCble_Ogiro;
                    info.info_cuota.Observacion = info.co_observacion;
                    info.info_cuota.Estado = true;
                    bus_cuotas.ModificarDB(info.info_cuota);
                }
                if (info.info_forma_pago.codigo_pago_sri != "" && info.info_forma_pago.codigo_pago_sri!=null)
                {
                    bus_forma_pago.EliminarDB(info.IdEmpresa, info.IdTipoCbte_Ogiro, info.IdCbteCble_Ogiro);

                    info.info_forma_pago.IdEmpresa = info.IdEmpresa;
                    info.info_forma_pago.IdTipoCbte_Ogiro = info.IdTipoCbte_Ogiro;
                    info.info_forma_pago.IdCbteCble_Ogiro = info.IdCbteCble_Ogiro;
                    info.info_forma_pago.formas_pago_sri = "";

                    bus_forma_pago.GuardarDB(info.info_forma_pago);
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(cp_orden_giro_Info info)
        {
            try
            {
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.cb_Fecha = (DateTime)info.co_FechaContabilizacion;
                info.info_comrobante.cb_Anio = info.info_comrobante.cb_Fecha.Year;
                info.info_comrobante.cb_mes = info.info_comrobante.cb_Fecha.Month;
                info.info_comrobante.cb_Estado = "A";
                info.info_comrobante.IdPeriodo = Convert.ToInt32(info.info_comrobante.cb_Fecha.Year.ToString() + info.info_comrobante.cb_Fecha.Month.ToString().PadLeft(2, '0'));
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.cb_Observacion = info.co_observacion+" ANULADO";

                info.info_comrobante.IdTipoCbte = info.IdTipoCbte_Ogiro;
                info.info_comrobante.IdCbteCble = info.IdCbteCble_Ogiro;
                info.co_valorpagar = info.co_total;
                if (info.info_cuota.Total_a_pagar == 0)
                    info.co_FechaFactura_vct = info.co_FechaFactura;
                else
                    info.co_FechaFactura_vct = info.info_cuota.Fecha_inicio;
                info.co_fechaOg = info.co_FechaFactura;
              
                if (bus_contabilidad.anularDB(info.info_comrobante))
                {
                    data = new cp_orden_giro_Data();
                    data.anularDB(info);
                }

                if (info.info_cuota.Dias_plazo != 0
                    & info.info_cuota.Total_a_pagar != 0
                    & info.info_cuota.lst_cuotas_det.Count() > 0
                  )
                {
                    info.info_cuota.IdEmpresa = info.IdEmpresa;
                    info.info_cuota.IdTipoCbte = info.IdTipoCbte_Ogiro;
                    info.info_cuota.IdCbteCble = info.IdCbteCble_Ogiro;
                    info.info_cuota.Observacion = info.co_observacion+"ANULADO";
                    bus_cuotas.AnularDB(info.info_cuota);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public cp_orden_giro_Info get_info(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                cp_orden_giro_Info info = new cp_orden_giro_Info();
                info= data.get_info(IdEmpresa,IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
                info.info_comrobante = bus_contabilidad.get_info(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
                info.info_comrobante.lst_ct_cbtecble_det = bus_comrpbante_det.get_list(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
                info.info_cuota = bus_cuotas.get_info(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
                if (info.info_cuota == null)
                    info.info_cuota = new cp_cuotas_x_doc_Info { Fecha_inicio = info.co_FechaFactura};

                info.info_forma_pago = bus_forma_pago.get_info(info.IdEmpresa, info.IdTipoCbte_Ogiro, info.IdCbteCble_Ogiro);
                if (info.info_forma_pago == null)
                    info.info_forma_pago = new cp_orden_giro_pagos_sri_Info();
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string validar(cp_orden_giro_Info info)
        {
            try
            {
                string mensaje = "";
                if (!bus_periodo.ValidarFechaTransaccion(info.IdEmpresa, info.co_FechaFactura, cl_enumeradores.eModulo.CONTA, ref mensaje))
                {
                    return mensaje;
                }

                if (!bus_periodo.ValidarFechaTransaccion(info.IdEmpresa, info.co_FechaFactura, cl_enumeradores.eModulo.CXP, ref mensaje))
                {
                    return mensaje;
                }


                if (info.IdSucursal == 0 | info.IdSucursal == null)
                {
                    mensaje = "Selecciona la sucursal";
                    return mensaje;
                }
                if (info.IdProveedor == 0)
                {
                    mensaje = "Selecciona el proveedor";
                    return mensaje;
                }
                if (info.IdTipoServicio == "")
                {
                    mensaje = "Selecciona el tipo de servicio";
                    return mensaje;
                }
                if (info.IdIden_credito == 0)
                {
                    mensaje = "Selecciona el tipo de sustento tributario";
                    return mensaje;
                }
            
                if (info.IdOrden_giro_Tipo == ""| info.IdOrden_giro_Tipo==null)
                { 
                    mensaje = "Selecciona el tipo de documento";
                    return mensaje;
                }
                if (info.co_serie == "" | info.co_serie == null)
                { 
                    mensaje = "Ingrese seri del documento";
                    return mensaje;
                }
                if (info.co_factura == "" | info.co_factura == null)
                {
                    mensaje = "Ingrese el número del documento";
                    return mensaje;
                }
                if (info.Num_Autorizacion == "" | info.Num_Autorizacion == null)
                { 
                    mensaje = "Ingrese el número de autorización";
                    return mensaje;
                }
                if (info.co_observacion == "" | info.co_observacion == null)
                { 
                    mensaje = "Ingrese la observación";
                    return mensaje;
                }
                if (info.PagoLocExt == "EXT")
                {
                    if(info.PaisPago=="" | info.PaisPago==null)
                    { 
                    mensaje = "Seleccion el país donde se realiza el pago";
                        return mensaje;
                    }
                }
                if (info.co_total == 0 )
                {
                    mensaje = "El monto de la factura no puede ser cero";
                    return mensaje;
                }
                info.info_comrobante.lst_ct_cbtecble_det.ForEach(item =>
                {
                    if (item.IdCtaCble == null | item.IdCtaCble == "")
                        mensaje = "Falta cuenta contable " + item.dc_Observacion;
                });

                double valor = info.info_comrobante.lst_ct_cbtecble_det.Sum(v => v.dc_Valor);
                valor =Math.Round( Convert.ToDouble(valor),2);
                if (valor!=0)
                    mensaje = "El diario contable esta descuadrado ";

                if(info.info_cuota.Total_a_pagar!=0 && info.info_cuota.Num_cuotas!=0&& info.info_cuota.Dias_plazo!=0)
                {
                    if(info.info_cuota.lst_cuotas_det.Count()==0)
                        mensaje = "No existe detalle de pago";

                }

                if(info.co_total>1000)
                {
                    if(info.info_forma_pago.codigo_pago_sri==null)
                    mensaje = "Debe seleccionar la forma de pago";

                }
                if (info.Num_Autorizacion.Length==10 |info.Num_Autorizacion.Length == 37 | info.Num_Autorizacion.Length == 49)
                {
                    
                }
                else
                {
                    mensaje = "EL número de autorización no tiene la longitud correcta";
                }
                return mensaje;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool Generar_OP_x_orden_giro(cp_orden_giro_Info info)
        {
            try
            {
                info_parametro = bus_parametro.get_info(info.IdEmpresa);
                bus_proveedor = new cp_proveedor_Bus();
                decimal IdPersona = bus_proveedor.get_info(info.IdEmpresa, info.IdProveedor).IdPersona;
                cp_orden_pago_tipo_x_empresa_Info info_tipo_op = new cp_orden_pago_tipo_x_empresa_Info();
                cp_orden_pago_tipo_x_empresa_Data op_tipo_data = new cp_orden_pago_tipo_x_empresa_Data();
                info_tipo_op = op_tipo_data.get_info(info.IdEmpresa, "FACT_PROVEE");
                cp_orden_pago_Info info_op = new cp_orden_pago_Info();
                bus_op = new cp_orden_pago_Bus();
                info_op.IdEmpresa = info.IdEmpresa;
                info_op.IdTipo_op = info_tipo_op.IdTipo_op;
                info_op.Observacion = "Por cancelacion de la factura # "+info.co_factura;
                info_op.IdTipo_Persona = "PROVEE";
                info_op.IdPersona = IdPersona;
                info_op.IdEntidad = info.IdProveedor;
                info_op.IdEstadoAprobacion = info_tipo_op.IdEstadoAprobacion;
                info_op.IdFormaPago = "CHEQUE";
                info_op.Fecha_Pago = DateTime.Now ;
                info_op.Estado = "A";
                info_op.Fecha = DateTime.Now;
                
                // crear detalle de op
                cp_orden_pago_det_Info info_op_det = new cp_orden_pago_det_Info();
                info_op_det.IdEmpresa = info.IdEmpresa;
                info_op_det.IdEmpresa_cxp = info.IdEmpresa;
                info_op_det.Secuencia = 1;
                info_op_det.IdCbteCble_cxp = info.IdCbteCble_Ogiro;
                info_op_det.IdTipoCbte_cxp = info.IdTipoCbte_Ogiro;
                info_op_det.Valor_a_pagar = info.co_valorpagar;
                info_op_det.Referencia = "Pago factura # "+info.co_factura;
                info_op_det.IdFormaPago = "CHEQUE";
                info_op_det.Fecha_Pago = DateTime.Now;
                info_op_det.IdEstadoAprobacion = info_tipo_op.IdEstadoAprobacion;
                info_op.detalle.Add(info_op_det);
                bus_op.guardar_op_x_fpDB(info_op);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool si_existe(cp_orden_giro_Info info)
        {
            try
            {
                return data.si_existe(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        }
}
