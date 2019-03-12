﻿using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Helps;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Info.RRHH.RDEP;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class RdepController : Controller
    {
        #region variables
        //Rdep_Info_lis Lis_Rdep_Info_lis = new Rdep_Info_lis();
        //Rdep_Bus bus_rpde = new Rdep_Bus();
        ro_rdep_List Lista_ro_rdep = new ro_rdep_List();
        List<ro_rdep_Info> ro_rdep_Lista = new List<ro_rdep_Info>();
        FilesHelper_Bus FilesHelper_B = new FilesHelper_Bus();
        tb_sucursal_Bus bus_Sucursal = new tb_sucursal_Bus();
        ct_anio_fiscal_Bus bus_anio = new ct_anio_fiscal_Bus();
        ro_rdep_Bus bus_ro_rpde = new ro_rdep_Bus();
        ro_nomina_tipo_Bus bus_tipo_nomina = new ro_nomina_tipo_Bus();

        #endregion

        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_rdep()
        {
            Rdep_Info model = new Rdep_Info();
            return PartialView("_CmbEmpleado_rdep", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {
            try
            {
                var lst_Sucursal = bus_Sucursal.get_list(IdEmpresa, false);
                ViewBag.lst_Sucursal = lst_Sucursal;

                var lst_Anio = bus_anio.get_list(false);
                ViewBag.lst_Anio = lst_Anio;

                var lst_nomina_tipo = bus_tipo_nomina.get_list(IdEmpresa, false);
                ViewBag.lst_nomina_tipo = lst_nomina_tipo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region vistas
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal),
                IdAnio = (DateTime.Now.Year),
                IdNomina = 1,
            };

            cargar_combos(model.IdEmpresa);

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            model.IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            cargar_combos(model.IdEmpresa);
            //ro_rdep_Lista = bus_ro_rpde.GetList(model.IdEmpresa, model.IdSucursal, model.IdNomina, model.IdAnio);
            //Lista_ro_rdep.set_list(ro_rdep_Lista, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            
            return View(model);
        }

        public ActionResult GridViewPartial_rdep(int IdSucursal, int IdNomina_Tipo, int pe_anio)
        {
            List<ro_rdep_Info> model = new List<ro_rdep_Info>();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.IdEmpresa = IdEmpresa == 0 ? 0 : Convert.ToInt32(IdEmpresa);
            ViewBag.IdSucursal = IdSucursal == 0 ? 0 : Convert.ToInt32(IdSucursal);
            ViewBag.IdNomina_Tipo = IdNomina_Tipo == 0 ? 0 : Convert.ToInt32(IdNomina_Tipo);
            ViewBag.pe_anio = pe_anio == 0 ? 0 : Convert.ToInt32(pe_anio);

            model = bus_ro_rpde.GetList(IdEmpresa, IdSucursal, IdNomina_Tipo, pe_anio);
            return PartialView("_GridViewPartial_rdep", model);
        }
        /*
        [HttpPost]
        public ActionResult Index(Rdep_Info model)
        {
            string nombre_file ="RDEP";
            
            string xml = "";
            rdep rdep = new rdep();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            int IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            rdep = bus_rpde.get_list(IdEmpresa,Convert.ToInt32( model.pe_anio), model.IdEmpleado);
            var ms = new MemoryStream();
            var xw = XmlWriter.Create(ms);
            var serializer = new XmlSerializer(rdep.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(xw, rdep, ns);
            xw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            using (var sr = new StreamReader(ms, Encoding.UTF8))
            {
                xml = sr.ReadToEnd();
            }
            byte[] fileBytes = ms.ToArray();
            return File(fileBytes, "application/xml", nombre_file + ".xml");

        }
        */
        
        public ActionResult GridViewPartial_rdep_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            ro_rdep_Info model = new ro_rdep_Info();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);            

            model.Lista_Rdep_Det = Lista_ro_rdep.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_rdep_det", model);
        }
        #endregion

        #region Acciones

        public ActionResult Nuevo()
        {
            ro_rdep_Info model = new ro_rdep_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal),
                pe_anio = (DateTime.Now.Year),
                IdNomina_Tipo = 1,
            };

            model.Lista_Rdep_Det = new List<ro_rdep_det_Info>();
            cargar_combos(model.IdEmpresa);
            return View(model);

        }
        [HttpPost]
        public ActionResult Nuevo(ro_rdep_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.IdUsuario = SessionFixed.IdUsuario;
                    if (!bus_ro_rpde.GenerarRDEP(info.IdEmpresa, info.IdSucursal, info.Id_Rdep, info.pe_anio, info.IdNomina_Tipo, info.IdEmpleado, info.Observacion, info.IdUsuario))
                    {
                        cargar_combos(info.IdEmpresa);
                        return View(info);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                        
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdSucursal=0, int Id_Rdep=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ro_rdep_Info model = bus_ro_rpde.GetInfo(IdEmpresa, IdSucursal, Id_Rdep);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            Lista_ro_rdep.set_list(model.Lista_Rdep_Det, Convert.ToDecimal(SessionFixed.IdTransaccionSession) );

            if (model == null)
                return RedirectToAction("Index");
           
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ro_rdep_Info model)
        {
            if (!bus_ro_rpde.ModificarBD(model))
            {
                return View(model);
            }

            cargar_combos(model.IdEmpresa);
            return RedirectToAction("Index");
        }

        #endregion
    }


    public class ro_rdep_List
    {
        string variable = "ro_rdep_det_Info";
        public List<ro_rdep_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable+IdTransaccionSession.ToString()] == null)
            {
                List<ro_rdep_det_Info> list = new List<ro_rdep_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_rdep_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_rdep_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable+IdTransaccionSession.ToString()] = list;
        }

    }
}