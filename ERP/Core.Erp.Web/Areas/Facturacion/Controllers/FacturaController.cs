﻿using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.Facturacion;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Bus.SeguridadAcceso;
using Core.Erp.Info.Facturacion;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Areas.Inventario.Controllers;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    [SessionTimeout]
    public class FacturaController : Controller
    {
        #region Variables
        fa_factura_Bus bus_factura = new fa_factura_Bus();
        fa_PuntoVta_Bus bus_punto_venta = new fa_PuntoVta_Bus();
        fa_cliente_contactos_Bus bus_contactos = new fa_cliente_contactos_Bus();
        fa_Vendedor_Bus bus_vendedor = new fa_Vendedor_Bus();
        fa_TerminoPago_Bus bus_termino_pago = new fa_TerminoPago_Bus();
        fa_factura_det_List List_det = new fa_factura_det_List();
        string mensaje = string.Empty;
        in_Producto_List List_producto = new in_Producto_List();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        in_ProductoTipo_Bus bus_producto_tipo = new in_ProductoTipo_Bus();
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
        fa_cliente_contactos_Bus bus_contacto = new fa_cliente_contactos_Bus();
        fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        fa_cliente_x_fa_Vendedor_x_sucursal_Bus bus_v_x_c = new fa_cliente_x_fa_Vendedor_x_sucursal_Bus();
        fa_formaPago_Bus bus_forma_pago = new fa_formaPago_Bus();
        fa_cuotas_x_doc_List List_cuotas = new fa_cuotas_x_doc_List();
        fa_TerminoPago_Distribucion_Bus bus_termino_pago_distribucion = new fa_TerminoPago_Distribucion_Bus();
        tb_sis_Documento_Tipo_Talonario_Bus bus_talonario = new tb_sis_Documento_Tipo_Talonario_Bus();
        fa_cuotas_x_doc_Bus bus_cuotas = new fa_cuotas_x_doc_Bus();
        fa_factura_det_Bus bus_det = new fa_factura_det_Bus();
        fa_parametro_Bus bus_param = new fa_parametro_Bus();
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
        tbl_TransaccionesAutorizadas_Bus bus_transaccionesAut = new tbl_TransaccionesAutorizadas_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_factura(DateTime? Fecha_ini, DateTime? Fecha_fin)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            var model = bus_factura.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_factura", model);
        }
        #endregion

        #region Metodos ComboBox bajo demanda cliente
        public ActionResult CmbCliente_Factura()
        {
            decimal model = new decimal();
            return PartialView("_CmbCliente_Factura", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.CLIENTE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.CLIENTE.ToString());
        }
        #endregion

        #region Metodos ComboBox bajo demanda producto
        public ActionResult ChangeValuePartial(decimal value = 0)
        {
            return PartialView("_CmbProducto_Factura", value);
        }
        public ActionResult CmbProducto_Factura()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_Factura", model);
        }
        public List<in_Producto_Info> get_list_bajo_demandaProducto(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            List<in_Producto_Info> Lista = bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORMODULO, cl_enumeradores.eModulo.FAC, 0);
            return Lista;
        }
        public in_Producto_Info get_info_bajo_demandaProducto(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Metodos
        private void cargar_combos(fa_factura_Info model)
        {
            var lst_sucursal = bus_sucursal.get_list(model.IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_punto_venta = bus_punto_venta.get_list(model.IdEmpresa, model.IdSucursal, false);
            ViewBag.lst_punto_venta = lst_punto_venta;

            var lst_contacto = bus_contacto.get_list(model.IdEmpresa, model.IdCliente);
            ViewBag.lst_contacto = lst_contacto;

            var lst_vendedor = bus_vendedor.get_list(model.IdEmpresa, false);
            ViewBag.lst_vendedor = lst_vendedor;

            var lst_pago = bus_termino_pago.get_list(false);
            ViewBag.lst_pago = lst_pago;

            var lst_forma_pago = bus_forma_pago.get_list();
            ViewBag.lst_forma_pago = lst_forma_pago;
        }
        private bool validar(fa_factura_Info i_validar, ref string msg)
        {
            string MsgValidaciones = string.Empty;
            i_validar.PedirDesbloqueo = false;

            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa,i_validar.vt_fecha,cl_enumeradores.eModulo.FAC,ref msg))
            {
                return false;
            }
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.vt_fecha, cl_enumeradores.eModulo.INV, ref msg))
            {
                return false;
            }

            i_validar.lst_det = List_det.get_list(i_validar.IdTransaccionSession);
            if (i_validar.lst_det.Count == 0)
            {
                msg = "No ha ingresado registros en el detalle de la factura";
                return false;
            }
            if (i_validar.lst_det.Where(q => q.vt_cantidad == 0).Count() > 0)
            {
                msg = "Existen registros con cantidad 0 en el detalle de la factura";
                return false;
            }
            if (i_validar.lst_det.Where(q => q.IdProducto == 0).Count() > 0)
            {
                msg = "Existen registros sin producto en el detalle de la factura";
                return false;
            }
            if (i_validar.lst_det.Sum(q=>q.vt_total) == 0)
            {
                msg = "La factura no tiene valor, por favor revise";
                return false;
            }

            i_validar.lst_cuota = List_cuotas.get_list(i_validar.IdTransaccionSession);
            i_validar.IdBodega = (int)bus_punto_venta.get_info(i_validar.IdEmpresa, i_validar.IdSucursal, Convert.ToInt32(i_validar.IdPuntoVta)).IdBodega;
            i_validar.IdPeriodo = Convert.ToInt32(i_validar.vt_fecha.ToString("yyyyMM"));
            i_validar.vt_mes = i_validar.vt_fecha.Month;
            i_validar.vt_anio = i_validar.vt_fecha.Year;
            i_validar.IdCaja = 1;
            i_validar.IdUsuario = SessionFixed.IdUsuario;
            i_validar.IdUsuarioUltModi = SessionFixed.IdUsuario;

            if (i_validar.IdCbteVta == 0)
            {
                var talonario = bus_talonario.get_info(i_validar.IdEmpresa, i_validar.vt_tipoDoc, i_validar.vt_serie1, i_validar.vt_serie2, i_validar.vt_NumFactura);
                if (talonario == null)
                {
                    msg = "No existe un talonario creado con la numeración: "+i_validar.vt_serie1+"-"+i_validar.vt_serie2+"-"+i_validar.vt_NumFactura;
                    return false;
                }
                if (talonario.Usado == true)
                {
                    msg = "El talonario: " + i_validar.vt_serie1 + "-" + i_validar.vt_serie2 + "-" + i_validar.vt_NumFactura+" se encuentra utilizado.";
                    return false;
                }
                if (bus_factura.factura_existe(i_validar.IdEmpresa,i_validar.vt_serie1,i_validar.vt_serie2,i_validar.vt_NumFactura))
                {
                    msg = "Existe una factura registrada con el número: " + i_validar.vt_serie1 + "-" + i_validar.vt_serie2 + "-" + i_validar.vt_NumFactura + ".";
                    return false;
                }                
            }

            if (!bus_cliente.ValidarCupoCreditoCliente(i_validar.IdEmpresa, i_validar.IdSucursal, i_validar.IdBodega, i_validar.IdCbteVta, "FACT", i_validar.IdCliente, i_validar.lst_det.Sum(q => q.vt_total), ref MsgValidaciones))
            {
                var info_usuarios = bus_usuario.get_info(string.IsNullOrEmpty(i_validar.IdUsuarioAut) ? "" : i_validar.IdUsuarioAut);
                if (info_usuarios != null && info_usuarios.es_super_admin && !string.IsNullOrEmpty(i_validar.contrasena_admin) && !string.IsNullOrEmpty(info_usuarios.contrasena_admin) && i_validar.contrasena_admin.Trim().ToLower() == info_usuarios.contrasena_admin.Trim().ToLower())
                {
                    tbl_TransaccionesAutorizadas_info info_trasnsaccion_aut = new tbl_TransaccionesAutorizadas_info
                    {
                        IdEmpresa = i_validar.IdEmpresa,
                        IdUsuarioAut = i_validar.IdUsuarioAut,
                        IdUsuarioLog = SessionFixed.IdUsuario,
                        Observacion = (i_validar.IdCbteVta == 0 ? "**NUEVO**" : "**MODIFICAR**") + "Desbloqueo de facturación para cupo de crédito excedido para FACT #" + i_validar.vt_NumFactura,
                    };
                    bus_transaccionesAut.guardarDB(info_trasnsaccion_aut);
                }
                else
                {
                    i_validar.PedirDesbloqueo = true;
                    msg = null;
                    return false;
                }
            }

            if (bus_factura.ValidarCarteraVencida(i_validar.IdEmpresa,i_validar.IdCliente,ref MsgValidaciones))
            {
                var info_usuario = bus_usuario.get_info(string.IsNullOrEmpty(i_validar.IdUsuarioAut) ? "" : i_validar.IdUsuarioAut);

                if (info_usuario != null && info_usuario.es_super_admin && !string.IsNullOrEmpty(i_validar.contrasena_admin) && !string.IsNullOrEmpty(info_usuario.contrasena_admin) && i_validar.contrasena_admin.Trim().ToLower() == info_usuario.contrasena_admin.Trim().ToLower())
                {
                    tbl_TransaccionesAutorizadas_info info_trasnsaccion_aut = new tbl_TransaccionesAutorizadas_info
                    {
                        IdEmpresa = i_validar.IdEmpresa,
                        IdUsuarioAut = i_validar.IdUsuarioAut,
                        IdUsuarioLog = SessionFixed.IdUsuario,
                        Observacion = (i_validar.IdCbteVta == 0 ? "**NUEVO**" : "**MODIFICAR**") + "Desbloqueo de facturación para cartera vencida para FACT #" + i_validar.vt_NumFactura,
                    };
                    bus_transaccionesAut.guardarDB(info_trasnsaccion_aut);
                }
                else
                {
                    i_validar.PedirDesbloqueo = true;
                    msg = null;
                    return false;
                }
            }

            var lst_validar = i_validar.lst_det.Select(q => new in_Producto_Stock_Info
            {
                IdEmpresa = i_validar.IdEmpresa,
                IdSucursal = i_validar.IdSucursal,
                IdBodega = i_validar.IdBodega,
                IdProducto = q.IdProducto,
                pr_descripcion = q.pr_descripcion,
                Cantidad = q.vt_cantidad,
                tp_manejaInven = q.tp_manejaInven,
                CantidadAnterior = q.CantidadAnterior,
                SeDestribuye = (bool)q.se_distribuye
            }).ToList();
            if (!bus_producto.validar_stock(lst_validar, ref msg))
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Json
        public JsonResult Desbloquear(string Contrasenia = "")
        {
            string EstadoDesbloqueo = "";

            var param = bus_param.get_info(Convert.ToInt32(SessionFixed.IdEmpresa));
            if(param != null)
            {
                if (Contrasenia.ToLower() == param.clave_desbloqueo_precios.ToLower())
                {
                    EstadoDesbloqueo = "DESBLOQUEADO";
                }
            }

            return Json(EstadoDesbloqueo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidarCliente(int IdEmpresa = 0, decimal IdCliente = 0, decimal IdTransaccionSession = 0)
        {
            string mensaje = string.Empty;
            string mensaje_cupo = string.Empty;

            if (IdCliente != 0)
            {
                bus_factura.ValidarCarteraVencida(IdEmpresa, IdCliente, ref mensaje);

                bus_cliente.ValidarCupoCreditoCliente(IdEmpresa, 0, 0, 0, "FACT", IdCliente, List_det.get_list(IdTransaccionSession).Sum(q => q.vt_total), ref mensaje_cupo);
            }            

            if (string.IsNullOrEmpty(mensaje))
                mensaje = mensaje_cupo;
            else
                mensaje += !string.IsNullOrEmpty(mensaje_cupo) ? (", Además " + mensaje_cupo) : "";

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLineaDetalle(int Secuencia = 0, decimal IdTransaccionSession = 0)
        {
            fa_factura_det_Info lineaF = new fa_factura_det_Info();

            var linea = List_det.get_list(IdTransaccionSession).Where(q => q.Secuencia == Secuencia).FirstOrDefault();
            if(linea != null)
                lineaF = linea;
            return Json(linea, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarSeleccionLote(decimal IdProducto = 0)
        {
            string mensaje = "";
            var producto = List_producto.get_list().Where(q=>q.IdProducto == IdProducto).FirstOrDefault();
            if (producto != null && producto.OrdenVcto != 1)
            {
                mensaje = "Ha escogido un producto con fecha de vencimiento diferente al recomendado";
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarGrid(decimal IdTransaccionSession = 0)
        {
            string mensaje = "";
            var lista = List_det.get_list(IdTransaccionSession);
            if (lista.Where(q=>q.vt_Precio == 0).Count() > 0)
            {
                mensaje = "Existen items con precio 0 en el detalle";
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ModificarLinea(int Secuencia = 0, decimal IdTransaccionSession = 0, double Precio = 0, double PorDescuento = 0, bool AplicarTodaFactura = false)
        {            
            var lista = List_det.get_list(IdTransaccionSession);
            if (AplicarTodaFactura)
            {
                foreach (var linea in lista)
                {
                    if (linea.Secuencia == Secuencia)
                        linea.vt_Precio = Precio;
                                        
                    linea.vt_PorDescUnitario = PorDescuento;
                    List_det.UpdateRow(linea, IdTransaccionSession);
                }
            }
            else
            {
                var linea = lista.Where(q => q.Secuencia == Secuencia).FirstOrDefault();
                if (linea != null)
                {
                    linea.vt_Precio = Precio;
                    linea.vt_PorDescUnitario = PorDescuento;
                    List_det.UpdateRow(linea, IdTransaccionSession);
                }
            }
            
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarLineaProducto(int Secuencia = 0, decimal IdTransaccionSession = 0, decimal IdProducto = 0)
        {
            var linea = List_det.get_list(IdTransaccionSession).Where(q => q.Secuencia == Secuencia).FirstOrDefault();
            if (linea != null)
            {
                var producto = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), IdProducto);
                if (producto != null)
                {
                    linea.IdProducto = IdProducto;
                    linea.pr_descripcion = producto.pr_descripcion_combo;
                }                    
                List_det.UpdateRow(linea, IdTransaccionSession);
            }
            return Json(linea, JsonRequestBehavior.AllowGet);
        }

        public JsonResult cargar_contactos(decimal IdCliente = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var resultado = bus_contacto.get_list(IdEmpresa, IdCliente);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargarPuntosDeVenta(int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var resultado = bus_punto_venta.get_list(IdEmpresa, IdSucursal, false);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BuscarProducto(int IdSucursal = 0, int IdPuntoVta = 0, int Secuencia = 0,decimal IdTransaccionSession = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var linea = List_det.get_list(IdTransaccionSession).Where(q => q.Secuencia == Secuencia).FirstOrDefault();
            
            var resultado = bus_producto.get_info(IdEmpresa, linea == null ? 0 : linea.IdProducto);
            if (resultado == null)
                resultado = new in_Producto_Info();

            var punto_venta = bus_punto_venta.get_info(IdEmpresa, IdSucursal, IdPuntoVta);
            if (punto_venta != null)
            {
                if (resultado.IdProducto_padre > 0)
                    List_producto.set_list(bus_producto.get_list_stock_lotes(IdEmpresa, IdSucursal, Convert.ToInt32(punto_venta.IdBodega), Convert.ToDecimal(resultado.IdProducto_padre)));
            }
            else
                List_producto.set_list(new List<in_Producto_Info>());
            
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLotesPorProducto(int IdSucursal = 0, int IdPuntoVta = 0, decimal IdProducto = 0, decimal IdCliente = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var resultado = bus_producto.get_info(IdEmpresa, IdProducto);
            if (resultado == null)
                resultado = new in_Producto_Info();

            var cliente = bus_cliente.get_info(IdEmpresa, IdCliente);
            if (cliente != null && cliente.EsClienteExportador)
            {
                resultado.IdCod_Impuesto_Iva = "IVA0";
            }

            var punto_venta = bus_punto_venta.get_info(IdEmpresa, IdSucursal, IdPuntoVta);
            if (punto_venta != null)
            {
                if (resultado.IdProducto_padre > 0)
                    List_producto.set_list(bus_producto.get_list_stock_lotes(IdEmpresa, IdSucursal, Convert.ToInt32(punto_venta.IdBodega), Convert.ToDecimal(resultado.IdProducto_padre)));
            }
            else
                List_producto.set_list(new List<in_Producto_Info>());
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_info_cliente(decimal IdCliente = 0, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
            fa_cliente_Info resultado = bus_cliente.get_info(IdEmpresa, IdCliente);
            if (resultado == null)
            {
                resultado = new fa_cliente_Info
                {
                    info_persona = new tb_persona_Info()
                };
            }
            else
            {
                var vendedor = bus_v_x_c.get_info(IdEmpresa, IdCliente, IdSucursal);
                if (vendedor != null)
                    resultado.IdVendedor = vendedor.IdVendedor;
                else
                    resultado.IdVendedor = 1;
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_info_termino_pago(string IdTerminoPago = "")
        {
            var resultado = bus_termino_pago.get_info(IdTerminoPago);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUltimoDocumento(int IdSucursal = 0, int IdPuntoVta = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            tb_sis_Documento_Tipo_Talonario_Info resultado;
            var punto_venta = bus_punto_venta.get_info(IdEmpresa, IdSucursal, IdPuntoVta);
            if (punto_venta != null)
            {
                tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
                var bodega = bus_bodega.get_info(IdEmpresa, IdSucursal, Convert.ToInt32(punto_venta.IdBodega));
                var sucursal = bus_sucursal.get_info(IdEmpresa, IdSucursal);
                resultado = bus_talonario.get_info_ultimo_no_usado(IdEmpresa, sucursal.Su_CodigoEstablecimiento, bodega.cod_punto_emision, "FACT");
            }
            else
                resultado = new tb_sis_Documento_Tipo_Talonario_Info();
            if (resultado == null)
                resultado = new tb_sis_Documento_Tipo_Talonario_Info();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProformasPorFacturar(int IdSucursal = 0, decimal IdCliente = 0)
        {
            bool resultado = true;

            set_list_proformas(bus_det.get_list_proformas_x_facturar(Convert.ToInt32(SessionFixed.IdEmpresa), IdSucursal, IdCliente));

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public void CargarCuotas(DateTime? FechaPrimerPago, string IdTerminoPago = "", double ValorPrimerPago = 0, decimal IdTransaccionSession = 0)
        {
            List<fa_cuotas_x_doc_Info> lst_cuotas = new List<fa_cuotas_x_doc_Info>();
            if (FechaPrimerPago != null)
            {
                var lst_distribucion = bus_termino_pago_distribucion.get_list(IdTerminoPago);
                int Secuencia = 1;
                int NumCuotas = lst_distribucion.Count;
                double totalAux = Math.Round(List_det.get_list(IdTransaccionSession).Sum(q => q.vt_total) - ValorPrimerPago, 2, MidpointRounding.AwayFromZero);
                DateTime FechaPagosAcum = Convert.ToDateTime(FechaPrimerPago);
                foreach (var item in lst_distribucion)
                {
                    if (Secuencia == 1)
                    {
                        lst_cuotas.Add(new fa_cuotas_x_doc_Info
                        {
                            secuencia = Secuencia,
                            num_cuota = Secuencia++,
                            valor_a_cobrar = Math.Round(totalAux * (item.Por_distribucion / 100), 2, MidpointRounding.AwayFromZero),
                            fecha_vcto_cuota = FechaPagosAcum
                        });
                        lst_distribucion.ForEach(q => q.Num_Dias_Vcto  =- item.Num_Dias_Vcto);
                    }
                    else
                    if (Secuencia == NumCuotas)
                    {
                        lst_cuotas.Add(new fa_cuotas_x_doc_Info
                        {
                            secuencia = Secuencia,
                            num_cuota = Secuencia++,
                            valor_a_cobrar = Math.Round(totalAux - lst_cuotas.Sum(q => q.valor_a_cobrar), 2, MidpointRounding.AwayFromZero),
                            fecha_vcto_cuota = FechaPagosAcum = (item.Num_Dias_Vcto == 30 ? FechaPagosAcum.AddMonths(1) : FechaPagosAcum.AddDays(item.Num_Dias_Vcto))
                        });
                    }
                    else
                        lst_cuotas.Add(new fa_cuotas_x_doc_Info
                        {
                            secuencia = Secuencia,
                            num_cuota = Secuencia++,
                            valor_a_cobrar = Math.Round(totalAux * (item.Por_distribucion / 100), 2, MidpointRounding.AwayFromZero),
                            fecha_vcto_cuota = FechaPagosAcum = (item.Num_Dias_Vcto == 30 ? FechaPagosAcum.AddMonths(1) : FechaPagosAcum.AddDays(item.Num_Dias_Vcto))
                        });
                }
            }
            List_cuotas.set_list(lst_cuotas,IdTransaccionSession);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_factura_Info model = new fa_factura_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                vt_fecha = DateTime.Now,
                vt_fech_venc = DateTime.Now,
                lst_det = new List<fa_factura_det_Info>(),
                lst_cuota = new List<fa_cuotas_x_doc_Info>(),
                vt_tipoDoc = "FACT",
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            List_cuotas.set_list(model.lst_cuota, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(fa_factura_Info model)
        {
            if (!validar(model, ref mensaje))
            {
                List_det.set_list(List_det.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = mensaje;
                cargar_combos(model);
                return View(model);
            }
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            if (!bus_factura.guardarDB(model))
            {
                List_det.set_list(List_det.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos(model);
                return View(model);
            };
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0 , int IdSucursal = 0, int IdBodega = 0, decimal IdCbteVta = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_factura_Info model = bus_factura.get_info(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            if(model == null)
                return RedirectToAction("Index");
            if (model.esta_impresa == null ? false : Convert.ToBoolean(model.esta_impresa))
                return RedirectToAction("Index");
            
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_cuota = bus_cuotas.get_list(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            model.lst_det = bus_det.get_list(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            List_det.set_list(model.lst_det,model.IdTransaccionSession);            
            List_cuotas.set_list(model.lst_cuota, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(fa_factura_Info model)
        {
            if (!validar(model, ref mensaje))
            {
                List_det.set_list(List_det.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = mensaje;
                cargar_combos(model);
                return View(model);
            }
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            if (!bus_factura.modificarDB(model))
            {
                List_det.set_list(List_det.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = "No se ha podido modificar el registro";
                cargar_combos(model);
                return View(model);
            };
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0 , int IdSucursal = 0, int IdBodega = 0, decimal IdCbteVta = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_factura_Info model = bus_factura.get_info(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_cuota = bus_cuotas.get_list(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            model.lst_det = bus_det.get_list(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            List_cuotas.set_list(model.lst_cuota, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_factura_Info model)
        {
            if (!bus_factura.ValidarDocumentoAnulacion(model.IdEmpresa,model.IdSucursal,model.IdBodega,model.IdCbteVta,model.vt_tipoDoc,ref mensaje))
            {
                List_det.set_list(List_det.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = mensaje;
                cargar_combos(model);
                return View(model);
            }
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario.ToString();
            if (!bus_factura.anularDB(model))
            {
                List_det.set_list(List_det.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos(model);
                return View(model);
            };
            return RedirectToAction("Index");
        }
        #endregion

        #region Cuotas
        public ActionResult GridViewPartial_factura_cuotas()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_cuotas.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_factura_cuotas", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdateCuota([ModelBinder(typeof(DevExpressEditorsBinder))] fa_cuotas_x_doc_Info info_det)
        {
            List_cuotas.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_cuotas.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_factura_cuotas", model);
        }

        public ActionResult EditingDeleteCuota(int Secuencia)
        {
            List_cuotas.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_cuotas.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_factura_cuotas", model);
        }
        #endregion

        #region funciones del detalle

        public ActionResult GridViewPartial_LoteFactura()
        {
            var model = List_producto.get_list();
            return PartialView("_GridViewPartial_LoteFactura", model);
        }

        private void cargar_combos_detalle()
        {
            var lst_impuesto = bus_impuesto.get_list("IVA", false);
            ViewBag.lst_impuesto = lst_impuesto;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_factura_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            SessionFixed.IdEntidad = !string.IsNullOrEmpty(Request.Params["IdCliente"]) ? Request.Params["IdCliente"].ToString() : "-1";
            return PartialView("_GridViewPartial_factura_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] fa_factura_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            decimal IdCliente = Convert.ToDecimal(SessionFixed.IdEntidad);
            if (info_det != null && info_det.IdProducto != 0)
            {
                var producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                if (producto != null)
                {
                    info_det.pr_descripcion = producto.pr_descripcion_combo;
                    info_det.se_distribuye = producto.se_distribuye;
                    info_det.tp_manejaInven = bus_producto_tipo.get_info(IdEmpresa, producto.IdProductoTipo).tp_ManejaInven;
                    var cliente = bus_cliente.get_info(IdEmpresa, IdCliente);
                    if (cliente != null)
                    {
                        int nivel_precio = (cliente.NivelPrecio == null || cliente.NivelPrecio == 0) ? 1 : Convert.ToInt32(cliente.NivelPrecio);
                        switch (nivel_precio)
                        {
                            case 1:
                                info_det.vt_Precio = producto.precio_1;
                                break;
                            case 2:
                                if (producto.signo_2 == "+")
                                {
                                    info_det.vt_Precio = producto.precio_2 == 0 ? producto.precio_1 : producto.precio_2;
                                    info_det.vt_PorDescUnitario = 0;
                                }
                                else
                                {
                                    info_det.vt_Precio = producto.precio_1;
                                    info_det.vt_PorDescUnitario = producto.porcentaje_2;
                                }
                                break;
                            case 3:
                                if (producto.signo_3 == "+")
                                {
                                    info_det.vt_Precio = producto.precio_3 == 0 ? producto.precio_1 : producto.precio_3;
                                    info_det.vt_PorDescUnitario = 0;
                                }
                                else
                                {
                                    info_det.vt_Precio = producto.precio_1;
                                    info_det.vt_PorDescUnitario = producto.porcentaje_3;
                                }
                                break;
                            case 4:
                                if (producto.signo_4 == "+")
                                {
                                    info_det.vt_Precio = producto.precio_4 == 0 ? producto.precio_1 : producto.precio_4;
                                    info_det.vt_PorDescUnitario = 0;
                                }
                                else
                                {
                                    info_det.vt_Precio = producto.precio_1;
                                    info_det.vt_PorDescUnitario = producto.porcentaje_4;
                                }

                                break;
                            case 5:
                                if (producto.signo_5 == "+")
                                {
                                    info_det.vt_Precio = producto.precio_5 == 0 ? producto.precio_1 : producto.precio_5;
                                    info_det.vt_PorDescUnitario = 0;
                                }
                                else
                                {
                                    info_det.vt_Precio = producto.precio_1;
                                    info_det.vt_PorDescUnitario = producto.porcentaje_5;
                                }
                                break;
                        }
                    }
                }
            }
            List_det.AddRow(info_det,Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_factura_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] fa_factura_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            decimal IdCliente = Convert.ToDecimal(SessionFixed.IdEntidad);
            if (info_det != null && info_det.IdProducto != 0)
            {
                var producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                if (producto != null)
                {
                    info_det.pr_descripcion = producto.pr_descripcion_combo;
                    info_det.tp_manejaInven = bus_producto_tipo.get_info(IdEmpresa, producto.IdProductoTipo).tp_ManejaInven;
                    info_det.se_distribuye = producto.se_distribuye;
                    var cliente = bus_cliente.get_info(IdEmpresa, IdCliente);
                    if (cliente != null)
                    {
                        int nivel_precio = (cliente.NivelPrecio == null || cliente.NivelPrecio == 0) ? 1 : Convert.ToInt32(cliente.NivelPrecio);
                        switch (nivel_precio)
                        {
                            case 1:
                                info_det.vt_Precio = producto.precio_1;
                                break;
                            case 2:
                                if (producto.signo_2 == "+")
                                {
                                    info_det.vt_Precio = producto.precio_2 == 0 ? producto.precio_1 : producto.precio_2;
                                    info_det.vt_PorDescUnitario = 0;
                                }
                                else
                                {
                                    info_det.vt_Precio = producto.precio_1;
                                    info_det.vt_PorDescUnitario = producto.porcentaje_2;
                                }
                                break;
                            case 3:
                                if (producto.signo_3 == "+")
                                {
                                    info_det.vt_Precio = producto.precio_3 == 0 ? producto.precio_1 : producto.precio_3;
                                    info_det.vt_PorDescUnitario = 0;
                                }
                                else
                                {
                                    info_det.vt_Precio = producto.precio_1;
                                    info_det.vt_PorDescUnitario = producto.porcentaje_3;
                                }
                                break;
                            case 4:
                                if (producto.signo_4 == "+")
                                {
                                    info_det.vt_Precio = producto.precio_4 == 0 ? producto.precio_1 : producto.precio_4;
                                    info_det.vt_PorDescUnitario = 0;
                                }
                                else
                                {
                                    info_det.vt_Precio = producto.precio_1;
                                    info_det.vt_PorDescUnitario = producto.porcentaje_4;
                                }

                                break;
                            case 5:
                                if (producto.signo_5 == "+")
                                {
                                    info_det.vt_Precio = producto.precio_5 == 0 ? producto.precio_1 : producto.precio_5;
                                    info_det.vt_PorDescUnitario = 0;
                                }
                                else
                                {
                                    info_det.vt_Precio = producto.precio_1;
                                    info_det.vt_PorDescUnitario = producto.porcentaje_5;
                                }
                                break;
                        }
                    }
                }
            }
            List_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_factura_det", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            List_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_factura_det", model);
        }
        #endregion

        #region Detalle de proforma
        [ValidateInput(false)]
        public ActionResult GridViewPartial_PFactura_det()
        {
            var model = get_list_proformas();
            return PartialView("_GridViewPartial_PFactura_det", model);
        }
        public void AddProformas(string IDs = "", decimal IdTransaccionSession =0)
        {
            if (!string.IsNullOrEmpty(IDs))
            {
                string[] array = IDs.Split(',');
                var lst = get_list_proformas();
                var lst_det_f = List_det.get_list(IdTransaccionSession);
                foreach (var item in array)
                {
                    var pf = lst.Where(q => q.secuencial == item).FirstOrDefault();
                    if (pf != null)
                        if (lst_det_f.Where(q => q.IdEmpresa_pf == pf.IdEmpresa_pf && q.IdSucursal_pf == pf.IdSucursal_pf && q.IdProforma == pf.IdProforma && q.Secuencia_pf == pf.Secuencia_pf).Count() == 0)
                        {
                            pf.Secuencia = lst_det_f.Count == 0 ? 1 : lst_det_f.Max(q => q.Secuencia) + 1;
                            lst_det_f.Add(pf);
                        }
                }
                List_det.set_list(lst_det_f,IdTransaccionSession);
            }
        }
        public List<fa_factura_det_Info> get_list_proformas()
        {
            if (Session["fa_factura_det_proforma_Info"] == null)
            {
                List<fa_factura_det_Info> list = new List<fa_factura_det_Info>();

                Session["fa_factura_det_proforma_Info"] = list;
            }
            return (List<fa_factura_det_Info>)Session["fa_factura_det_proforma_Info"];
        }

        public void set_list_proformas(List<fa_factura_det_Info> list)
        {
            Session["fa_factura_det_proforma_Info"] = list;
        }
        #endregion
    }

    public class fa_factura_det_List
    {
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        string variable = "fa_factura_det_Info";
        public List<fa_factura_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_factura_det_Info> list = new List<fa_factura_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_factura_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }
        public void set_list(List<fa_factura_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(fa_factura_det_Info info_det, decimal IdTransaccionSession)
        {
            List<fa_factura_det_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.IdProducto = info_det.IdProducto;
            info_det.pr_descripcion = info_det.pr_descripcion;
            info_det.vt_DescUnitario = info_det.vt_Precio * (info_det.vt_PorDescUnitario / 100);
            info_det.vt_PrecioFinal = info_det.vt_Precio - info_det.vt_DescUnitario;
            info_det.vt_Subtotal =info_det.vt_cantidad * info_det.vt_PrecioFinal;
            var impuesto = bus_impuesto.get_info(info_det.IdCod_Impuesto_Iva);
            if (impuesto != null)
                info_det.vt_por_iva = impuesto.porcentaje;
            info_det.vt_iva = info_det.vt_Subtotal * (info_det.vt_por_iva / 100);
            info_det.vt_total = info_det.vt_Subtotal + info_det.vt_iva;
            
            list.Add(info_det);
        }

        public void UpdateRow(fa_factura_det_Info info_det, decimal IdTransaccionSession)
        {
            fa_factura_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdProducto = info_det.IdProducto;
            edited_info.pr_descripcion = info_det.pr_descripcion;
            edited_info.vt_cantidad = info_det.vt_cantidad;
            edited_info.vt_PorDescUnitario = info_det.vt_PorDescUnitario;
            edited_info.vt_Precio = info_det.vt_Precio;
            edited_info.vt_DescUnitario = info_det.vt_Precio * (info_det.vt_PorDescUnitario / 100);
            edited_info.vt_PrecioFinal = info_det.vt_Precio - edited_info.vt_DescUnitario;
            edited_info.vt_Subtotal = info_det.vt_cantidad * edited_info.vt_PrecioFinal;
            edited_info.tp_manejaInven = info_det.tp_manejaInven;
            edited_info.se_distribuye = info_det.se_distribuye;
            if (!string.IsNullOrEmpty(info_det.IdCod_Impuesto_Iva) && info_det.IdCod_Impuesto_Iva != edited_info.IdCod_Impuesto_Iva)
            {
                var impuesto = bus_impuesto.get_info(info_det.IdCod_Impuesto_Iva);
                if (impuesto != null)
                    edited_info.vt_por_iva = impuesto.porcentaje;
            }
            edited_info.vt_iva = edited_info.vt_Subtotal * (edited_info.vt_por_iva / 100);
            edited_info.vt_total = edited_info.vt_Subtotal + edited_info.vt_iva;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<fa_factura_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

    public class fa_cuotas_x_doc_List
    {
        string variable = "fa_cuotas_x_doc_Info";
        public List<fa_cuotas_x_doc_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_cuotas_x_doc_Info> list = new List<fa_cuotas_x_doc_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_cuotas_x_doc_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_cuotas_x_doc_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(fa_cuotas_x_doc_Info info_det, decimal IdTransaccionSession)
        {
            List<fa_cuotas_x_doc_Info> list = get_list(IdTransaccionSession);
            info_det.secuencia = list.Count == 0 ? 1 : list.Max(q => q.secuencia) + 1;
            list.Add(info_det);
        }

        public void UpdateRow(fa_cuotas_x_doc_Info info_det, decimal IdTransaccionSession)
        {
            fa_cuotas_x_doc_Info edited_info = get_list(IdTransaccionSession).Where(m => m.secuencia == info_det.secuencia).First();
        }

        public void DeleteRow(int secuencia, decimal IdTransaccionSession)
        {
            List<fa_cuotas_x_doc_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.secuencia == secuencia).First());
        }
    }
}