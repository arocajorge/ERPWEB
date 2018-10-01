﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using DevExpress.Web.Mvc;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using DevExpress.Web;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class ActaFiniquitoController : Controller
    {
        #region variables
        ro_Acta_Finiquito_Bus bus_acta_finiquito = new ro_Acta_Finiquito_Bus();
        ro_Acta_Finiquito_Detalle_Bus bus_detalle = new ro_Acta_Finiquito_Detalle_Bus();
        ro_Acta_Finiquito_Detalle_lst lst_detalle = new ro_Acta_Finiquito_Detalle_lst();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
        ro_Acta_Finiquito_Info info = new ro_Acta_Finiquito_Info();
        int IdEmpresa = 0;
        #endregion

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

        public ActionResult CmbEmpleado_acta()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_acta", model);
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
        public ActionResult GridViewPartial_liquidacion_empleado()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            List<ro_Acta_Finiquito_Info> model = bus_acta_finiquito.get_list(IdEmpresa);
            return PartialView("_GridViewPartial_liquidacion_empleado", model);
        }
        private void cargar_combos()
        {
            IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ViewBag.lst_empleado = bus_empleado.get_list_combo_liquidar(IdEmpresa);
            ViewBag.lst_tipo_contrato = bus_catalogo.get_list_x_tipo(2);
            ViewBag.lst_tipo_terminacion = bus_catalogo.get_list_x_tipo(24);

        }
        public ActionResult Nuevo()
        {
            ro_Acta_Finiquito_Info model = new ro_Acta_Finiquito_Info
            {
                IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]),
                FechaIngreso = DateTime.Now,
                FechaSalida =DateTime.Now,
               IdCausaTerminacion= "CTL_02"

            };
            model.lst_detalle = new List<ro_Acta_Finiquito_Detalle_Info>();
            lst_detalle.set_list(model.lst_detalle);
            cargar_combos();
            cargar_combos_detalle();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ro_Acta_Finiquito_Info model)
        {
            model.lst_detalle = lst_detalle.get_list();
            if (model.lst_detalle == null || model.lst_detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la novedad";
                cargar_combos();
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.IdUsuario = Session["IdUsuario"].ToString();
            if (!bus_acta_finiquito.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(decimal IdActaFiniquito)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_Acta_Finiquito_Info model = bus_acta_finiquito.get_info(IdEmpresa, IdActaFiniquito);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_detalle = bus_detalle.get_list(IdEmpresa, IdActaFiniquito);
            lst_detalle.set_list(model.lst_detalle);
            cargar_combos_detalle();
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ro_Acta_Finiquito_Info model)
        {
            model.lst_detalle = lst_detalle.get_list();
            if (model.lst_detalle == null || model.lst_detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la planificación";
                cargar_combos();
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.IdUsuarioUltMod = Session["IdUsuario"].ToString();
            if (!bus_acta_finiquito.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");

        }
        public ActionResult Anular( decimal IdActaFiniquito)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_Acta_Finiquito_Info model = bus_acta_finiquito.get_info(IdEmpresa, IdActaFiniquito);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_detalle = bus_detalle.get_list(IdEmpresa, IdActaFiniquito);
            lst_detalle.set_list(model.lst_detalle);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ro_Acta_Finiquito_Info model)
        {
            model.lst_detalle = lst_detalle.get_list();

            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.IdUsuarioUltAnu = Session["IdUsuario"].ToString();
            if (!bus_acta_finiquito.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_liquidacion_empleado_det()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_Acta_Finiquito_Info model = new ro_Acta_Finiquito_Info();
            model.lst_detalle = lst_detalle.get_list();
            if (model.lst_detalle.Count == 0)
                model.lst_detalle = lst_detalle.get_list();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_empleado_det", model);
        }
        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ViewBag.lst_rubro = bus_rubro.get_list(IdEmpresa, false);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ro_Acta_Finiquito_Detalle_Info info_det)
        {

            List<ro_rubro_tipo_Info> lista = Session["lst_rubro"] as List<ro_rubro_tipo_Info>;
            if (lista != null && lista.Count > 0)
                if (lista.Where(v => v.IdRubro == info_det.IdRubro).FirstOrDefault().ru_tipo == "E")
                    info_det.Valor = info_det.Valor * -1;

            if (ModelState.IsValid)
                lst_detalle.AddRow(info_det);
            ro_Acta_Finiquito_Info model = new ro_Acta_Finiquito_Info();
            model.lst_detalle = lst_detalle.get_list();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_empleado_det", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_Acta_Finiquito_Detalle_Info info_det)
        {
            List<ro_rubro_tipo_Info> lista = Session["lst_rubro"] as List<ro_rubro_tipo_Info>;
            if (lista != null && lista.Count > 0)
                if (lista.Where(v => v.IdRubro == info_det.IdRubro).FirstOrDefault().ru_tipo == "E")
                    info_det.Valor = info_det.Valor * -1;

            if (ModelState.IsValid)
                lst_detalle.UpdateRow(info_det);
            ro_Acta_Finiquito_Info model = new ro_Acta_Finiquito_Info();
            model.lst_detalle = lst_detalle.get_list();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_empleado_det", model);
        }
        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] ro_Acta_Finiquito_Detalle_Info info_det)
        {
            lst_detalle.DeleteRow(info_det.IdSecuencia);
            ro_Acta_Finiquito_Info model = new ro_Acta_Finiquito_Info();
            model.lst_detalle = lst_detalle.get_list();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_empleado_det", model);
        }
        public ActionResult Procesar(DateTime? FechaIngreso , DateTime? FechaSalida, decimal IdEmpleado=0, 
        double UltimaRemuneracion=0,  string idterminacion="", bool EsMujerEmbarazada=false, bool EsDirigenteSindical = false,
        bool EsPorDiscapacidad = false,bool EsPorEnfermedadNoProfesional=false)
        {

                      

            if (FechaIngreso == null)
                FechaIngreso = DateTime.Now;
            if (FechaSalida == null)
                FechaSalida = DateTime.Now;
            IdEmpresa = Convert.ToInt32(Session["IdEmpresa"].ToString());
            info.IdEmpleado = IdEmpleado;
            info.IdEmpresa = IdEmpresa;
            info.UltimaRemuneracion = UltimaRemuneracion;
            info.FechaIngreso =Convert.ToDateTime( FechaIngreso);
            info.FechaSalida =Convert.ToDateTime( FechaSalida);
            info.IdCausaTerminacion = idterminacion;
            info.EsMujerEmbarazada = EsMujerEmbarazada;
            info.EsDirigenteSindical = EsDirigenteSindical;
            info.EsPorDiscapacidad = EsPorDiscapacidad;
            info.EsPorEnfermedadNoProfesional = EsPorEnfermedadNoProfesional;
            info = bus_acta_finiquito.ObtenerIndemnizacion(info);
            lst_detalle.set_list(info.lst_detalle);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult Liquidar( decimal IdActaFiniquito)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_Acta_Finiquito_Info model = bus_acta_finiquito.get_info(IdEmpresa, IdActaFiniquito);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_detalle = bus_detalle.get_list(IdEmpresa, IdActaFiniquito);
            lst_detalle.set_list(model.lst_detalle);
            cargar_combos_detalle();
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Liquidar(ro_Acta_Finiquito_Info model)
        {
            model.lst_detalle = lst_detalle.get_list();
            if (model.lst_detalle == null || model.lst_detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la planificación";
                cargar_combos();
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.IdUsuarioUltMod = Session["IdUsuario"].ToString();
            if (!bus_acta_finiquito.Liquidar(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");

        }

    }


}

    public class ro_Acta_Finiquito_Detalle_lst
    {
        public List<ro_Acta_Finiquito_Detalle_Info> get_list()
        {
            if (HttpContext.Current.Session["lst_detalle"] == null)
            {
                List<ro_Acta_Finiquito_Detalle_Info> list = new List<ro_Acta_Finiquito_Detalle_Info>();

                HttpContext.Current.Session["lst_detalle"] = list;
            }
            return (List<ro_Acta_Finiquito_Detalle_Info>)HttpContext.Current.Session["lst_detalle"];
        }

        public void set_list(List<ro_Acta_Finiquito_Detalle_Info> list)
        {
            HttpContext.Current.Session["lst_detalle"] = list;
        }

        public void AddRow(ro_Acta_Finiquito_Detalle_Info info_det)
        {
            List<ro_Acta_Finiquito_Detalle_Info> list = get_list();
            info_det.IdSecuencia = list.Count == 0 ? 1 : list.Max(q => q.IdSecuencia) + 1;
            list.Add(info_det);
        }

        public void UpdateRow(ro_Acta_Finiquito_Detalle_Info info_det)
        {
            ro_Acta_Finiquito_Detalle_Info edited_info = get_list().Where(m => m.IdSecuencia == info_det.IdSecuencia).First();
            edited_info.IdActaFiniquito = info_det.IdActaFiniquito;
            edited_info.IdRubro = info_det.IdRubro;
            edited_info.Valor = info_det.Valor;
            edited_info.Observacion = info_det.Observacion;
        }

        public void DeleteRow(int Secuencia)
        {
            List<ro_Acta_Finiquito_Detalle_Info> list = get_list();
            list.Remove(list.Where(m => m.IdSecuencia == Secuencia).First());
        }
    }

