﻿using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using DevExpress.Web;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class LiquidacionVacacionesController : Controller
    {
        ro_Historico_Liquidacion_Vacaciones_Bus bus_liquidacion = new ro_Historico_Liquidacion_Vacaciones_Bus();
        List<ro_Historico_Liquidacion_Vacaciones_Info> lst_vacaciones = new List<ro_Historico_Liquidacion_Vacaciones_Info>();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        List<ro_Historico_Liquidacion_Vacaciones_Det_Info> lst_detalle = new List<ro_Historico_Liquidacion_Vacaciones_Det_Info>();
        ro_Historico_Liquidacion_Vacaciones_Det_Info_lst lst_detalle_lst = new ro_Historico_Liquidacion_Vacaciones_Det_Info_lst();
        ro_Historico_Liquidacion_Vacaciones_Info info_liquidacion = new ro_Historico_Liquidacion_Vacaciones_Info();
        int IdEmpresa = 0;

        #region Metodos ComboBox bajo demanda

        tb_persona_Bus bus_persona = new tb_persona_Bus();

        public tb_persona_Bus Bus_persona
        {
            get
            {
                return bus_persona;
            }

            set
            {
                bus_persona = value;
            }
        }

        public ActionResult CmbEmpleado_vaca()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_vaca", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return Bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return Bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_vacaciones_liquidadas()
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                List<ro_Historico_Liquidacion_Vacaciones_Info> model = bus_liquidacion.get_list(IdEmpresa);
                return PartialView("_GridViewPartial_vacaciones_liquidadas", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_vacaciones_liquidadas_det()
        {
            try
            {
                lst_detalle = Session["detalle"] as List<ro_Historico_Liquidacion_Vacaciones_Det_Info>;
                return PartialView("_GridViewPartial_vacaciones_liquidadas_det", lst_detalle);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_Historico_Liquidacion_Vacaciones_Info info)
        {
            try
            {
                bus_liquidacion = new ro_Historico_Liquidacion_Vacaciones_Bus();
                if (ModelState.IsValid)
                {
                    string mensaje = "";
                    info.detalle = Session["detalle"] as List<ro_Historico_Liquidacion_Vacaciones_Det_Info>;
                    if (info.detalle != null)
                    {
                        foreach (var item in info.detalle)
                        {
                            if (item.Valor_Cancelar == 0)
                            {
                                mensaje = "Existen periodos con valores cero a cancelar";
                            }
                        }
                    }
                    if (mensaje != "")
                    {
                        ViewBag.mensaje = mensaje;
                        cargar_combo();
                        return View(info);
                    }
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_liquidacion.guardarDB(info))
                    {
                        cargar_combo();
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index");
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Nuevo(decimal IdEmpleado=0, decimal IdSolicitud = 0)
        {
            try
            {
                ro_Historico_Liquidacion_Vacaciones_Info info = new ro_Historico_Liquidacion_Vacaciones_Info
                {
                  
                };
                IdEmpresa = GetIdEmpresa();
                info = bus_liquidacion.obtener_valores(IdEmpresa, IdEmpleado, IdSolicitud);
                Session["detalle"] = info.detalle;
                cargar_combo();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_Historico_Liquidacion_Vacaciones_Info info)
        {

            try
            {
                bus_liquidacion = new ro_Historico_Liquidacion_Vacaciones_Bus();
                if (ModelState.IsValid)
                {
                    string mensaje = "";
                    info.detalle = Session["detalle"] as List<ro_Historico_Liquidacion_Vacaciones_Det_Info>;
                    if(info.detalle!=null)
                    {
                        foreach (var item in info.detalle)
                        {
                            if (item.Valor_Cancelar == 0)
                            {
                                mensaje = "Existen periodos con valores cero a cancelar";
                            }
                        }
                    }
                    if (mensaje != "")
                    {
                        ViewBag.mensaje = mensaje;
                        cargar_combo();
                        return View(info);
                    }
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_liquidacion.modificarDB(info))
                    {
                        cargar_combo();
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index");
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Modificar(decimal IdEmpleado = 0, decimal IdLiquidacion = 0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                info_liquidacion = bus_liquidacion.get_info(IdEmpresa, IdEmpleado, IdLiquidacion);
                Session["detalle"] = info_liquidacion.detalle;
                cargar_combo();
                return View(info_liquidacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_Historico_Liquidacion_Vacaciones_Info info)
        {
            try
            {
                bus_liquidacion = new ro_Historico_Liquidacion_Vacaciones_Bus();
                IdEmpresa = GetIdEmpresa();
                info.IdEmpresa = IdEmpresa;
                if (!bus_liquidacion.anularDB(info))
                    return View(info);
                else
                    return RedirectToAction("Index");


            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(decimal IdEmpleado = 0, decimal IdLiquidacion = 0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                info_liquidacion = bus_liquidacion.get_info(IdEmpresa, IdEmpleado, IdLiquidacion);
                Session["detalle"] = info_liquidacion.detalle;
                cargar_combo();
                return View(info_liquidacion);

            }
            catch (Exception)
            {

                throw;
            }
        }
        private int GetIdEmpresa()
        {
            try
            {
                if (Session["IdEmpresa"] != null)
                    return Convert.ToInt32(Session["IdEmpresa"]);
                else
                    return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cargar_combo()
        {
            IdEmpresa = GetIdEmpresa();
            ViewBag.lst_empleado = bus_empleado.get_list_combo(IdEmpresa);
            ViewBag.lst_vacaciones = lst_vacaciones;
        }
        public JsonResult get_list_vacaciones(decimal IdEmpleado)
        {
            IdEmpresa = GetIdEmpresa();
          //  lst_vacaciones = bus_vacaciones.GrabarBD(IdEmpresa, IdEmpleado);
            Session["lst_vacaciones"] = lst_vacaciones;

            return Json(lst_vacaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_Historico_Liquidacion_Vacaciones_Det_Info info_det)
        {
            if (ModelState.IsValid)
                lst_detalle_lst.UpdateRow(info_det);
            ro_Historico_Liquidacion_Vacaciones_Info model = new ro_Historico_Liquidacion_Vacaciones_Info();
            model.detalle = lst_detalle_lst.get_list() as List<ro_Historico_Liquidacion_Vacaciones_Det_Info>;
            return PartialView("_GridViewPartial_vacaciones_liquidadas_det", model.detalle);
        }

        }



    public class ro_Historico_Liquidacion_Vacaciones_Det_Info_lst
    {
        public List<ro_Historico_Liquidacion_Vacaciones_Det_Info> get_list()
        {
            if (HttpContext.Current.Session["detalle"] == null)
            {
                List<ro_Historico_Liquidacion_Vacaciones_Det_Info> list = new List<ro_Historico_Liquidacion_Vacaciones_Det_Info>();

                HttpContext.Current.Session["detalle"] = list;
            }
            return (List<ro_Historico_Liquidacion_Vacaciones_Det_Info>)HttpContext.Current.Session["detalle"];
        }

        public void set_list(List<ro_Historico_Liquidacion_Vacaciones_Det_Info> list)
        {
            HttpContext.Current.Session["detalle"] = list;
        }


        public void UpdateRow(ro_Historico_Liquidacion_Vacaciones_Det_Info info_det)
        {
            ro_Historico_Liquidacion_Vacaciones_Det_Info edited_info = get_list().Where(m => m.Sec == info_det.Sec).First();
            edited_info.IdLiquidacion = info_det.IdLiquidacion;
            edited_info.Total_Remuneracion = info_det.Total_Remuneracion;
            edited_info.Total_Vacaciones = info_det.Total_Vacaciones;
            edited_info.Valor_Cancelar = info_det.Valor_Cancelar;

        }

    }
}

