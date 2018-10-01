﻿using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Caja;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class EgresoInventarioController : Controller
    {
        #region Variables
        in_Ing_Egr_Inven_Bus bus_ing_inv = new in_Ing_Egr_Inven_Bus();
        in_Ing_Egr_Inven_det_Bus bus_det_ing_inv = new in_Ing_Egr_Inven_det_Bus();
        in_Ing_Egr_Inven_det_List List_in_Ing_Egr_Inven_det = new in_Ing_Egr_Inven_det_List();
        in_parametro_Bus bus_in_param = new in_parametro_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        string mensaje = string.Empty;
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbProducto_EgresoInventario()
      {
            decimal model = new decimal();
            return PartialView("_CmbProducto_EgresoInventario", model);
        }
        public List<in_Producto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa),cl_enumeradores.eTipoBusquedaProducto.PORMODULO,cl_enumeradores.eModulo.INV,0);
        }
        public in_Producto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region vistas
        [ValidateInput(false)]
        public ActionResult GridViewPartial_egr_inv_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_in_Ing_Egr_Inven_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_egr_inv_det", model);
        }
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
        public ActionResult GridViewPartial_egreso_inventario(DateTime? fecha_ini, DateTime? fecha_fin)
        {
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : fecha_ini;
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : fecha_fin;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_ing_inv.get_list(IdEmpresa, "-", true, ViewBag.fecha_ini, ViewBag.fecha_fin);
            return PartialView("_GridViewPartial_egreso_inventario", model);
        }

        #endregion

        #region Acciones

        public ActionResult Nuevo(int IdEmpresa = 0 )
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            in_parametro_Info i_param = bus_in_param.get_info(IdEmpresa);
            if (i_param == null)
                return RedirectToAction("Index");
            in_Ing_Egr_Inven_Info model = new in_Ing_Egr_Inven_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdEmpresa = IdEmpresa,
                cm_fecha = DateTime.Now,
                signo = "-",
                IdMovi_inven_tipo = i_param.P_IdMovi_inven_tipo_default_egr == null ? 0 : Convert.ToInt32(i_param.P_IdMovi_inven_tipo_default_egr)
            };
            List_in_Ing_Egr_Inven_det.set_list(new List<in_Ing_Egr_Inven_det_Info>(), model.IdTransaccionSession);
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(in_Ing_Egr_Inven_Info model)
        {            
            model.lst_in_Ing_Egr_Inven_det = List_in_Ing_Egr_Inven_det.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdUsuario = Session["IdUsuario"].ToString();
            if (!bus_ing_inv.guardarDB(model,"-"))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdSucursal = 0, int IdMovi_inven_tipo = 0, decimal IdNumMovi = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            bus_ing_inv = new in_Ing_Egr_Inven_Bus();
            bus_det_ing_inv = new in_Ing_Egr_Inven_det_Bus();
            in_Ing_Egr_Inven_Info model = bus_ing_inv.get_info(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_in_Ing_Egr_Inven_det = bus_det_ing_inv.get_list(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            List_in_Ing_Egr_Inven_det.set_list(model.lst_in_Ing_Egr_Inven_det,model.IdTransaccionSession);
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(in_Ing_Egr_Inven_Info model)
        {
            model.lst_in_Ing_Egr_Inven_det = List_in_Ing_Egr_Inven_det.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdUsuarioUltModi = Session["IdUsuario"].ToString();
            if (!bus_ing_inv.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0 , int IdSucursal = 0, int IdMovi_inven_tipo = 0, decimal IdNumMovi = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            bus_ing_inv = new in_Ing_Egr_Inven_Bus();
            bus_det_ing_inv = new in_Ing_Egr_Inven_det_Bus();
            in_Ing_Egr_Inven_Info model = bus_ing_inv.get_info(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_in_Ing_Egr_Inven_det = bus_det_ing_inv.get_list(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            List_in_Ing_Egr_Inven_det.set_list(model.lst_in_Ing_Egr_Inven_det, model.IdTransaccionSession);
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(in_Ing_Egr_Inven_Info model)
        {
            model.lst_in_Ing_Egr_Inven_det = List_in_Ing_Egr_Inven_det.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdusuarioUltAnu = Session["IdUsuario"].ToString();
            if (!bus_ing_inv.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
         
        #region cargar combo y validaciones
        private void cargar_combos_detalle()
        {
            in_UnidadMedida_Bus bus_unidad = new in_UnidadMedida_Bus();
            var lst_unidad = bus_unidad.get_list(false);
            ViewBag.lst_unidad = lst_unidad;
        }
        private bool validar(in_Ing_Egr_Inven_Info i_validar, ref string msg)
        {
            if (i_validar.lst_in_Ing_Egr_Inven_det.Count == 0)
            {
                mensaje = "Debe ingresar al menos un producto";
                return false;
            }
            return true;
        }
        private void cargar_combos(int IdEmpresa)
        {
            in_movi_inven_tipo_Bus bus_tipo = new in_movi_inven_tipo_Bus();
            var lst_tipo = bus_tipo.get_list(IdEmpresa, false);
            ViewBag.lst_tipo = lst_tipo;

            in_Motivo_Inven_Bus bus_motivo = new in_Motivo_Inven_Bus();
            var lst_motivo = bus_motivo.get_list(IdEmpresa, false);
            ViewBag.lst_motivo = lst_motivo;

            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            var lst_bodega = bus_bodega.get_list(IdEmpresa, false);
            ViewBag.lst_bodega = lst_bodega;
        }
        #endregion

        #region funciones del detalle
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] in_Ing_Egr_Inven_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (info_det != null)
                if (info_det.IdProducto != 0)
                {
                    in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                    if (info_producto != null)
                    {
                        info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                        info_det.IdUnidadMedida = info_producto.IdUnidadMedida;
                        info_det.IdUnidadMedida_sinConversion = info_producto.IdUnidadMedida;
                    }
                }
            if(info_det.dm_cantidad_sinConversion>0 && info_det.IdProducto!=0)
            List_in_Ing_Egr_Inven_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_in_Ing_Egr_Inven_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_egr_inv_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] in_Ing_Egr_Inven_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (info_det != null)
                if (info_det.IdProducto != 0)
                {
                    in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                    if (info_producto != null)
                    {
                        info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                        info_det.IdUnidadMedida = info_producto.IdUnidadMedida;
                        info_det.IdUnidadMedida_sinConversion = info_producto.IdUnidadMedida;
                    }
                }
            if (info_det.dm_cantidad_sinConversion > 0)
                List_in_Ing_Egr_Inven_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_in_Ing_Egr_Inven_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_egr_inv_det", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            List_in_Ing_Egr_Inven_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_in_Ing_Egr_Inven_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_egr_inv_det", model);
        }
        #endregion
    }

}
