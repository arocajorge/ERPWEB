﻿using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    public class GrupoPorTipoGastoController : Controller
    {
        #region Variables
        ct_grupo_x_Tipo_Gasto_List Lista_GrupoPorTipoGasto = new ct_grupo_x_Tipo_Gasto_List();
        ct_grupo_x_Tipo_Gasto_Bus bus_GrupoPorTipoGasto = new ct_grupo_x_Tipo_Gasto_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda

        public ActionResult CmbCuenta_TipoGastoPadre()
        {
            ct_grupo_x_Tipo_Gasto_Info model = new ct_grupo_x_Tipo_Gasto_Info();
            return PartialView("_CmbCuenta_TipoGastoPadre", model);
        }
        public List<ct_grupo_x_Tipo_Gasto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_GrupoPorTipoGasto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public ct_grupo_x_Tipo_Gasto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_GrupoPorTipoGasto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var model = new ct_grupo_x_Tipo_Gasto_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<ct_grupo_x_Tipo_Gasto_Info> lista = bus_GrupoPorTipoGasto.get_list(model.IdEmpresa, true);
            Lista_GrupoPorTipoGasto.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_GrupoPorTipoGasto()
        {
            try
            {
                SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

                List<ct_grupo_x_Tipo_Gasto_Info> model = Lista_GrupoPorTipoGasto.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                return PartialView("_GridViewPartial_GrupoPorTipoGasto", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Acciones
        [HttpPost]
        public ActionResult Nuevo(ct_grupo_x_Tipo_Gasto_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!bus_GrupoPorTipoGasto.guardarDB(info))
                        return View(info);
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
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            try
            {
                ct_grupo_x_Tipo_Gasto_Info info = new ct_grupo_x_Tipo_Gasto_Info
                {
                    IdEmpresa = IdEmpresa,
                    nivel = 1,
                    orden = 1
                };

                return View(info);

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ct_grupo_x_Tipo_Gasto_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!bus_GrupoPorTipoGasto.modificarDB(info))
                        return View(info);
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

        public ActionResult Modificar(int IdEmpresa = 0, int IdTipo_Gasto = 0)
        {
            try
            {
                return View(bus_GrupoPorTipoGasto.get_info(IdEmpresa, IdTipo_Gasto));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ct_grupo_x_Tipo_Gasto_Info info)
        {
            try
            {
                if (!bus_GrupoPorTipoGasto.anularDB(info))
                    return View(info);
                else
                    return RedirectToAction("Index");


            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdEmpresa = 0, int IdTipo_Gasto = 0)
        {
            try
            {
                return View(bus_GrupoPorTipoGasto.get_info(IdEmpresa, IdTipo_Gasto));

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        public JsonResult get_info_nuevo(int IdEmpresa = 0, int IdTipoGasto_padre = 0)
        {
            var resultado = bus_GrupoPorTipoGasto.get_info_nuevo(IdEmpresa, IdTipoGasto_padre);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

    }

    public class ct_grupo_x_Tipo_Gasto_List
    {
        string Variable = "ct_grupo_x_Tipo_Gasto_Info";
        public List<ct_grupo_x_Tipo_Gasto_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ct_grupo_x_Tipo_Gasto_Info> list = new List<ct_grupo_x_Tipo_Gasto_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ct_grupo_x_Tipo_Gasto_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ct_grupo_x_Tipo_Gasto_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}