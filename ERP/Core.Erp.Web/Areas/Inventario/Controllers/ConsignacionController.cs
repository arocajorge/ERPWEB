﻿using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.General;
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
    public class ConsignacionController : Controller
    {
        // GET: Inventario/Consignacion
        #region Variables
        in_Consignacion_Bus bus_in_Consignacion = new in_Consignacion_Bus();
        in_ConsignacionDet_List in_ConsignacionDet_List = new in_ConsignacionDet_List();
        in_parametro_Bus bus_in_param = new in_parametro_Bus();
        string mensaje = string.Empty;
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
        #endregion

        public ConsignacionController()
        {

        }

        #region Metodos ComboBox bajo demanda
        in_ConsignacionDet_Bus bus_consignacion_det = new in_ConsignacionDet_Bus();

        #region Proveedor
        public ActionResult CmbProveedor_Consignacion()
        {
            in_Consignacion_Info model = new in_Consignacion_Info();
            return PartialView("_CmbProveedor_Consignacion", model);
        }

        public List<tb_persona_Info> get_list_bajo_demanda_proveedor(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }

        public tb_persona_Info get_info_bajo_demanda_proveedor(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        #endregion

        #region  Producto
        public ActionResult CmbProducto_Consignacion()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_Consignacion", model);
        }
        public List<in_Producto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORMODULO, cl_enumeradores.eModulo.INV, 0);
        }
        public in_Producto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #endregion

        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_combos_consulta();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            cargar_combos_consulta();
            return View(model);
        }
        #endregion        

        #region Metodos
        private void cargar_combos_consulta()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            var lst_bodega = bus_bodega.get_list(IdEmpresa, false);

            lst_sucursal.Add(new tb_sucursal_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = 0,
                Su_Descripcion = "Todos"
            });
            ViewBag.lst_sucursal = lst_sucursal;

            lst_bodega.Add(new tb_bodega_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = 0,
                bo_Descripcion = "Todos"
            });
            ViewBag.lst_bodega = lst_bodega;
        }

        private void cargar_combos(int IdEmpresa)
        {
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_bodega = bus_bodega.get_list(IdEmpresa, false);
            ViewBag.lst_bodega = lst_bodega;
        }
        #endregion

        public ActionResult GridViewPartial_Consignacion(DateTime? fecha_ini, DateTime? fecha_fin, int IdSucursal = 0)
        {
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : fecha_ini;
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : fecha_fin;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            List<in_Consignacion_Info> model = bus_in_Consignacion.GetList(IdEmpresa, IdSucursal, false, ViewBag.fecha_ini, ViewBag.fecha_fin);
            return PartialView("_GridViewPartial_Consignacion", model);
        }

        #region Acciones
        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            in_Consignacion_Info model = new in_Consignacion_Info {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal),
                Fecha = DateTime.Now.Date
            };

            in_ConsignacionDet_List.set_list(model.lst_producto_consignacion, model.IdTransaccionSession);
            cargar_combos(model.IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(in_Consignacion_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.lst_producto_consignacion = in_ConsignacionDet_List.get_list( model.IdTransaccionSession);

            if (!bus_in_Consignacion.GuardarBD(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdConsignacion=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            in_Consignacion_Info model = bus_in_Consignacion.GetInfo(IdEmpresa, IdConsignacion);

            if (model == null)
                return RedirectToAction("Index");
    
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_producto_consignacion = bus_consignacion_det.GetList(model.IdEmpresa, Convert.ToInt32(model.IdConsignacion));
            in_ConsignacionDet_List.set_list(model.lst_producto_consignacion, model.IdTransaccionSession);

            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(in_Consignacion_Info model)
        {
            model.lst_producto_consignacion = in_ConsignacionDet_List.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdUsuarioUltMod = Session["IdUsuario"].ToString();
            if (!bus_in_Consignacion.ModificarBD(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdConsignacion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            in_Consignacion_Info model = bus_in_Consignacion.GetInfo(IdEmpresa, Convert.ToInt32(IdConsignacion));
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_producto_consignacion = bus_consignacion_det.GetList(model.IdEmpresa, Convert.ToInt32(model.IdConsignacion));
            in_ConsignacionDet_List.set_list(model.lst_producto_consignacion, model.IdTransaccionSession);

            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(in_Consignacion_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario.ToString();
            if (!bus_in_Consignacion.AnularBD(model))
            {
                in_ConsignacionDet_List.set_list(model.lst_producto_consignacion, model.IdTransaccionSession);
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos(model.IdEmpresa);
                return View(model);
            };
            return RedirectToAction("Index");
        }
        #endregion

        #region Funciones del detalle
        private bool validar(in_Consignacion_Info i_validar, ref string msg)
        {
            if (i_validar.lst_producto_consignacion.Count == 0)
            {
                mensaje = "Debe ingresar al menos un producto";
                return false;
            }
            return true;
        }

        public ActionResult GridViewPartial_ConsignacionDet()
        {
            //siempre copiar
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            
            var model = in_ConsignacionDet_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();

            return PartialView("_GridViewPartial_ConsignacionDet", model);
        }

        private void cargar_combos_detalle()
        {
            in_UnidadMedida_Bus bus_unidad = new in_UnidadMedida_Bus();
            var lst_unidad = bus_unidad.get_list(false);
            ViewBag.lst_unidad = lst_unidad;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] in_ConsignacionDet_Info info_det)
        {
            if (!ModelState.IsValid)
            {
                var valid = in_ConsignacionDet_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                cargar_combos_detalle();
                return PartialView("_GridViewPartial_ConsignacionDet", valid);                 
            }
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (info_det != null)
                if (info_det.IdProducto != 0)
                {
                    in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                    if (info_producto != null)
                    {
                        info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                    }
                }
            if (info_det.Cantidad > 0)
                in_ConsignacionDet_List.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = in_ConsignacionDet_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ConsignacionDet", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] in_ConsignacionDet_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (info_det != null)
                if (info_det.IdProducto != 0)
                {
                    in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                    if (info_producto != null)
                    {
                        info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                    }
                }

                in_ConsignacionDet_List.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                var model = in_ConsignacionDet_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                cargar_combos_detalle();
                return PartialView("_GridViewPartial_ConsignacionDet", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            in_ConsignacionDet_List.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = in_ConsignacionDet_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ConsignacionDet", model);
        }

        #endregion
    }

    //siempre incluir clase para detalle
    public class in_ConsignacionDet_List
    {
        string Variable = "in_ConsignacionDet_Info";
        public List<in_ConsignacionDet_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_ConsignacionDet_Info> list = new List<in_ConsignacionDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_ConsignacionDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_ConsignacionDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(in_ConsignacionDet_Info info_det, decimal IdTransaccionSession)
        {
            List<in_ConsignacionDet_Info> list = get_list(IdTransaccionSession);
            info_det.IdConsignacion = info_det.IdConsignacion;
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.IdProducto = info_det.IdProducto;
            info_det.IdUnidadMedida = info_det.IdUnidadMedida;
            info_det.Cantidad = info_det.Cantidad;
            info_det.Costo = info_det.Costo;
            info_det.Observacion = info_det.Observacion;

            list.Add(info_det);
        }

        public void UpdateRow(in_ConsignacionDet_Info info_det, decimal IdTransaccionSession)
        {
            in_ConsignacionDet_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            info_det.IdProducto = info_det.IdProducto;
            info_det.IdUnidadMedida = info_det.IdUnidadMedida;
            info_det.Cantidad = info_det.Cantidad;
            info_det.Costo = info_det.Costo;
            info_det.Observacion = info_det.Observacion;
        }

        public void DeleteRow(int Secuencial, decimal IdTransaccionSession)
        {
            List<in_ConsignacionDet_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencial).First());
        }
    }
}