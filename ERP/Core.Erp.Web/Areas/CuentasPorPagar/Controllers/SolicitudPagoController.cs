﻿using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Info.SeguridadAcceso;
using Core.Erp.Bus.SeguridadAcceso;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    public class SolicitudPagoController : Controller
    {
        #region Variables
        cp_SolicitudPago_Bus bus_solicitud = new cp_SolicitudPago_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
        #endregion
        #region Index

        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_solicitud_pago(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {

            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            ViewBag.IdSucursal = IdSucursal;
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            var model = bus_solicitud.GetList(IdEmpresa, IdSucursal, ViewBag.Fecha_ini, ViewBag.Fecha_fin, true);
            return PartialView("_GridViewPartial_solicitud_pago", model);
        }
        #endregion
        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbProveedor_CXP()
        {
            decimal model = new decimal();
            return PartialView("_CmbProveedor_CXP", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        #endregion
        #region Acciones
        public ActionResult Nuevo()
        {
            cp_SolicitudPago_Info model = new cp_SolicitudPago_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                Fecha = DateTime.Now,
            };
            seg_usuario_Info mod = bus_usuario.get_info(SessionFixed.IdUsuario);
            model.Solicitante = mod.Nombre;
            cargar_combos(model.IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cp_SolicitudPago_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_solicitud.GuardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0 , decimal IdSolicitud = 0)
        {
            cp_SolicitudPago_Info model = bus_solicitud.GetInfo(IdEmpresa, IdSolicitud);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cp_SolicitudPago_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_solicitud.ModificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdSolicitud = 0)
        {
            cp_SolicitudPago_Info model = bus_solicitud.GetInfo(IdEmpresa, IdSolicitud);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cp_SolicitudPago_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_solicitud.AnularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}