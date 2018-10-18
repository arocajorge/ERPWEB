﻿using Core.Erp.Bus.Facturacion;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
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
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    [SessionTimeout]
    public class NotaDeDebitoFacturacionController : Controller
    {
        #region Variables
        fa_notaCreDeb_Bus bus_nota = new fa_notaCreDeb_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        fa_cliente_contactos_Bus bus_contacto = new fa_cliente_contactos_Bus();
        fa_PuntoVta_Bus bus_punto_venta = new fa_PuntoVta_Bus();
        in_Producto_List List_producto = new in_Producto_List();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        fa_cliente_x_fa_Vendedor_x_sucursal_Bus bus_v_x_c = new fa_cliente_x_fa_Vendedor_x_sucursal_Bus();
        fa_TerminoPago_Bus bus_termino_pago = new fa_TerminoPago_Bus();
        fa_TerminoPago_Distribucion_Bus bus_termino_pago_distribucion = new fa_TerminoPago_Distribucion_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_sis_Documento_Tipo_Talonario_Bus bus_talonario = new tb_sis_Documento_Tipo_Talonario_Bus();
        fa_notaCreDeb_det_List List_det = new fa_notaCreDeb_det_List();
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
        fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
        string mensaje = string.Empty;
        fa_notaCreDeb_det_Bus bus_det = new fa_notaCreDeb_det_Bus();
        fa_TipoNota_Bus bus_tipo_nota = new fa_TipoNota_Bus();
        fa_notaCreDeb_x_fa_factura_NotaDeb_Bus bus_cruce = new fa_notaCreDeb_x_fa_factura_NotaDeb_Bus();
        fa_notaCreDeb_x_fa_factura_NotaDeb_List List_cruce = new fa_notaCreDeb_x_fa_factura_NotaDeb_List();
        fa_TipoNota_x_Empresa_x_Sucursal_Bus bus_tipo_nota_x_sucursal = new fa_TipoNota_x_Empresa_x_Sucursal_Bus();
        fa_TipoNota_x_Empresa_x_Sucursal_Bus bus_nota_x_empresa_sucursal = new fa_TipoNota_x_Empresa_x_Sucursal_Bus();

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
        public ActionResult GridViewPartial_NotaDebitoFacturacion(DateTime? Fecha_ini, DateTime? Fecha_fin)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            var model = bus_nota.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin, "D");
            return PartialView("_GridViewPartial_NotaDebitoFacturacion", model);
        }
        #endregion

        #region Metodos ComboBox bajo demanda cliente
        public ActionResult CmbCliente_NotaDebito()
        {
            decimal model = new decimal();
            return PartialView("_CmbCliente_NotaDebitoFacturacion", model);
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
            return PartialView("_CmbProducto_NotaDebitoFacturacion", value);
        }
        public ActionResult CmbProducto_NotaDebito()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_NotaDebitoFacturacion", model);
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

        #region Json
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
        public JsonResult GetLotesPorProducto(int IdSucursal = 0, int IdPuntoVta = 0, decimal IdProducto = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var resultado = bus_producto.get_info(IdEmpresa, IdProducto);
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
                resultado = bus_talonario.get_info_ultimo_no_usado(IdEmpresa, sucursal.Su_CodigoEstablecimiento, bodega.cod_punto_emision, "NTDB");
            }
            else
                resultado = new tb_sis_Documento_Tipo_Talonario_Info();
            if (resultado == null)
                resultado = new tb_sis_Documento_Tipo_Talonario_Info();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDocumentosPorCobrar(int IdSucursal = 0, decimal IdCliente = 0, decimal IdTransaccionSession = 0)
        {
            bool resultado = true;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var List = List_cruce.get_list(IdTransaccionSession).Where(q => q.seleccionado == true).ToList();
            var ListPorCruzar = bus_cruce.get_list_cartera(IdEmpresa, IdSucursal, IdCliente, false);

            foreach (var item in List)
            {
                ListPorCruzar.Remove(ListPorCruzar.Where(q => q.secuencial == item.secuencial).FirstOrDefault());
            }

            List.AddRange(ListPorCruzar);
            List_cruce.set_list(List, IdTransaccionSession);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult VaciarListas(decimal IdTransaccionSession = 0)
        {
            bool resultado = true;
            List_cruce.set_list(new List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info>(), IdTransaccionSession);
            List_det.set_list(new List<fa_notaCreDeb_det_Info>(), IdTransaccionSession);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Grillas de cruce
        public ActionResult GridViewPartial_CruceND_x_cruzar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_cruce.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == false).ToList();
            return PartialView("_GridViewPartial_CruceND_x_cruzar", model);
        }

        public ActionResult GridViewPartial_CruceND()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_cruce.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == true).ToList();
            return PartialView("_GridViewPartial_CruceND", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNewFacturas(string IDs = "",decimal IdTransaccionSession =0)
        {
            if (IDs != "")
            {
                string[] array = IDs.Split(',');
                foreach (var item in array)
                {
                    List_cruce.DeleteRow(item, IdTransaccionSession);
                }
            }
            var list = List_cruce.get_list(IdTransaccionSession).Where(q => q.seleccionado == true).ToList();
            var lst_det = new List<fa_notaCreDeb_det_Info>();
            foreach (var item in list)
            {
                lst_det.AddRange(bus_det.get_list(item.IdEmpresa_fac_nd_doc_mod, item.IdSucursal_fac_nd_doc_mod, item.IdBodega_fac_nd_doc_mod, item.IdCbteVta_fac_nd_doc_mod, item.vt_tipoDoc));
            }
            List_det.set_list(lst_det, IdTransaccionSession);
            var model = list;
            return PartialView("_GridViewPartial_CruceND", model);
        }

        public ActionResult EditingUpdateFactura([ModelBinder(typeof(DevExpressEditorsBinder))] fa_notaCreDeb_x_fa_factura_NotaDeb_Info info_det)
        {
            List_cruce.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_cruce.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == true).ToList();
            return PartialView("_GridViewPartial_CruceND", model);
        }
        public ActionResult EditingDeleteFactura(string secuencial)
        {
            List_cruce.DeleteRow(secuencial, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_cruce.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == true).ToList();
            return PartialView("_GridViewPartial_CruceND", model);
        }

        #endregion

        #region funciones del detalle

        public ActionResult GridViewPartial_LoteDebitoFacturacion()
        {
            var model = List_producto.get_list();
            return PartialView("_GridViewPartial_LoteNotaDebitoFacturacion", model);
        }

        private void cargar_combos_detalle()
        {
            var lst_impuesto = bus_impuesto.get_list("IVA", false);
            ViewBag.lst_impuesto = lst_impuesto;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_notaDebito_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_NotaDebitoFacturacion_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] fa_notaCreDeb_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (ModelState.IsValid)            
                List_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_NotaDebitoFacturacion_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] fa_notaCreDeb_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (ModelState.IsValid)
                List_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_NotaDebitoFacturacion_det", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            List_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_NotaDebitoFacturacion_det", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(fa_notaCreDeb_Info model)
        {
            var lst_sucursal = bus_sucursal.get_list(model.IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_punto_venta = bus_punto_venta.get_list(model.IdEmpresa, model.IdSucursal, false);
            ViewBag.lst_punto_venta = lst_punto_venta;

            var lst_contacto = bus_contacto.get_list(model.IdEmpresa, model.IdCliente);
            ViewBag.lst_contacto = lst_contacto;

            Dictionary<string, string> lst_naturaleza = new Dictionary<string, string>();
            lst_naturaleza.Add("INT", "Interno");
            lst_naturaleza.Add("SRI", "Autorizado por el SRI");
            ViewBag.lst_naturaleza = lst_naturaleza;

            var lst_tipo_nota = bus_tipo_nota.get_list("D", false);
            ViewBag.lst_tipo_nota = lst_tipo_nota;
        }
        private bool validar(fa_notaCreDeb_Info i_validar, ref string msg)
        {
            i_validar.lst_det = List_det.get_list(i_validar.IdTransaccionSession);
            i_validar.lst_cruce = List_cruce.get_list(i_validar.IdTransaccionSession).Where(q => q.seleccionado == true).ToList();
            if (i_validar.lst_det.Count == 0)
            {
                msg = "No ha ingresado registros en el detalle";
                return false;
            }
            if (i_validar.lst_det.Where(q => q.sc_cantidad == 0).Count() > 0)
            {
                msg = "Existen registros con cantidad 0 en el detalle";
                return false;
            }
            if (i_validar.lst_det.Where(q => q.IdProducto == 0).Count() > 0)
            {
                msg = "Existen registros sin producto en el detalle";
                return false;
            }

            i_validar.lst_cruce.ForEach(q => q.Valor_Aplicado = 0);

            i_validar.IdBodega = (int)bus_punto_venta.get_info(i_validar.IdEmpresa, i_validar.IdSucursal, Convert.ToInt32(i_validar.IdPuntoVta)).IdBodega;
            i_validar.IdUsuario = SessionFixed.IdUsuario;
            i_validar.IdUsuarioUltMod = SessionFixed.IdUsuario;
            var tipo_nota = bus_tipo_nota_x_sucursal.get_info(i_validar.IdEmpresa, i_validar.IdTipoNota, i_validar.IdSucursal);
            if (tipo_nota != null)
                i_validar.IdCtaCble_TipoNota = tipo_nota.IdCtaCble;

            if (i_validar.IdNota == 0 && i_validar.NaturalezaNota == "SRI")
            {
                var talonario = bus_talonario.get_info(i_validar.IdEmpresa, i_validar.CodDocumentoTipo, i_validar.Serie1, i_validar.Serie2, i_validar.NumNota_Impresa);
                if (talonario == null)
                {
                    msg = "No existe un talonario creado con la numeración: " + i_validar.Serie1 + "-" + i_validar.Serie2 + "-" + i_validar.NumNota_Impresa;
                    return false;
                }
                if (talonario.Usado == true)
                {
                    msg = "El talonario: " + i_validar.Serie1 + "-" + i_validar.Serie2 + "-" + i_validar.NumNota_Impresa + " se encuentra utilizado.";
                    return false;
                }
                if (bus_nota.DocumentoExiste(i_validar.IdEmpresa, i_validar.CodDocumentoTipo, i_validar.Serie1, i_validar.Serie2, i_validar.NumNota_Impresa))
                {
                    msg = "Existe una nota de débito con el talonario: " + i_validar.Serie1 + "-" + i_validar.Serie2 + "-" + i_validar.NumNota_Impresa + " utilizado.";
                    return false;
                }
                if (talonario.es_Documento_Electronico == false)
                {
                    i_validar.NumAutorizacion = talonario.NumAutorizacion;
                }
            }

            if (i_validar.NaturalezaNota != "SRI")
            {
                i_validar.Serie1 = null;
                i_validar.Serie2 = null;
                i_validar.NumNota_Impresa = null;
            }

            return true;
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
            fa_notaCreDeb_Info model = new fa_notaCreDeb_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                no_fecha = DateTime.Now,
                no_fecha_venc = DateTime.Now,
                lst_det = new List<fa_notaCreDeb_det_Info>(),
                lst_cruce = new List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info>(),
                CodDocumentoTipo = "NTDB",
                CreDeb = "D",
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            List_cruce.set_list(model.lst_cruce, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(fa_notaCreDeb_Info model)
        {
            var nota = bus_nota_x_empresa_sucursal.get_info(model.IdEmpresa, model.IdTipoNota, model.IdSucursal);
            if (nota != null)
            {
                if (nota.IdCtaCble == null | nota.IdCtaCble == "")
                {
                    ViewBag.mensaje = "No existe cuenta contable para el tipo de nota de credito";
                    cargar_combos(model);
                    return View(model);
                }
            }
            if (!validar(model, ref mensaje))
            {
                List_det.set_list(List_det.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = mensaje;
                cargar_combos(model);
                return View(model);
            }
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            if (!bus_nota.guardarDB(model))
            {
                List_det.set_list(List_det.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos(model);
                return View(model);
            };
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdSucursal = 0, int IdBodega = 0, decimal IdNota = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_notaCreDeb_Info model = bus_nota.get_info(IdEmpresa, IdSucursal, IdBodega, IdNota);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_det.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            model.lst_cruce = bus_cruce.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            List_cruce.set_list(model.lst_cruce, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(fa_notaCreDeb_Info model)
        {
            var nota = bus_nota_x_empresa_sucursal.get_info(model.IdEmpresa, model.IdTipoNota, model.IdSucursal);
            if (nota != null)
            {
                if (nota.IdCtaCble == null | nota.IdCtaCble == "")
                {
                    ViewBag.mensaje = "No existe cuenta contable para el tipo de nota de credito";
                    cargar_combos(model);
                    return View(model);
                }
            }
            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model);
                return View(model);
            }
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            if (!bus_nota.modificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                cargar_combos(model);
                return View(model);
            };
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0 , int IdSucursal = 0, int IdBodega = 0, decimal IdNota = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_notaCreDeb_Info model = bus_nota.get_info(IdEmpresa, IdSucursal, IdBodega, IdNota);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_det.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            model.lst_cruce = bus_cruce.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            List_cruce.set_list(model.lst_cruce, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_notaCreDeb_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario.ToString();
            if (!bus_nota.anularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos(model);
                return View(model);
            };
            return RedirectToAction("Index");
        }
        #endregion
    }
}