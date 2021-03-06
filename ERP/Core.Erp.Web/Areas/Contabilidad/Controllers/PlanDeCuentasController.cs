﻿using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.SeguridadAcceso;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.SeguridadAcceso;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    [SessionTimeout]
    public class PlanDeCuentasController : Controller
    {
        #region Variables
        ct_plancta_List ListaPlancta = new ct_plancta_List();
        ct_anio_fiscal_List ListaAnioFiscal = new ct_anio_fiscal_List();
        ct_anio_fiscal_Bus bus_anio_fiscal = new ct_anio_fiscal_Bus();
        seg_Menu_x_Empresa_x_Usuario_Bus bus_permisos = new seg_Menu_x_Empresa_x_Usuario_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Index
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "Contabilidad", "PlanDeCuentas", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            ct_plancta_Info model = new ct_plancta_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
            };

            var lst = bus_plancta.get_list(model.IdEmpresa, true, false);
            ListaPlancta.set_list(lst, model.IdTransaccionSession);
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_plancta(bool Nuevo = false)
        {
            //int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            //List<ct_plancta_Info> model = bus_plancta.get_list(IdEmpresa, true, false);
            ViewBag.Nuevo = Nuevo;
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaPlancta.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_plancta", model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_cuentas = bus_plancta.get_list(IdEmpresa, false, false);
            ViewBag.lst_cuentas = lst_cuentas;

            Dictionary<string, string> lst_naturaleza = new Dictionary<string, string>();
            lst_naturaleza.Add("D","Deudora");
            lst_naturaleza.Add("C", "Acreedora");
            ViewBag.lst_naturaleza = lst_naturaleza;

            ct_grupocble_Bus bus_grupo_contable = new ct_grupocble_Bus();
            var lst_grupo_contabe = bus_grupo_contable.get_list(false);
            ViewBag.lst_grupo_contabe = lst_grupo_contabe;

            ct_ClasificacionEBIT_Bus bus_clasificacion = new ct_ClasificacionEBIT_Bus();
            var lst_clasificacion_EBIT = bus_clasificacion.GetList();
            lst_clasificacion_EBIT.Add(new ct_ClasificacionEBIT_Info
            {
                IdClasificacionEBIT=0,
                ebit_Descripcion =""
            });
            ViewBag.lst_clasificacion_EBIT = lst_clasificacion_EBIT;

            ct_grupo_x_Tipo_Gasto_Bus bus_tipo_gasto = new ct_grupo_x_Tipo_Gasto_Bus();
            var lst_tipo_gasto = bus_tipo_gasto.get_list(IdEmpresa,false);
            lst_tipo_gasto.Add(new ct_grupo_x_Tipo_Gasto_Info
            {
                IdTipo_Gasto = 0,
                nom_tipo_Gasto = ""
            });
            ViewBag.lst_tipo_gasto = lst_tipo_gasto;
            
        }
        #endregion

        #region Metodos ComboBox bajo demanda

        public ActionResult CmbCuenta_PlanCta()
        {
            ct_plancta_Info model = new ct_plancta_Info();
            return PartialView("_CmbCuenta_PlanCta", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), true);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa=0)
        {
            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "Contabilidad", "PlanDeCuentas", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion

            ct_plancta_Info model = new ct_plancta_Info
            {
                IdEmpresa = IdEmpresa,
                IdTipo_Gasto = 0
            };
            model.IdClasificacionEBIT = (model.IdClasificacionEBIT == null ? 0 : model.IdClasificacionEBIT);
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ct_plancta_Info model)
        {
            if (bus_plancta.validar_existe_id(model.IdEmpresa,model.IdCtaCble))
            {
                ViewBag.mensaje = "El código de la cuenta ya se encuentra registrado";
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            if (!bus_plancta.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdCtaCble = model.IdCtaCble, Exito = true });
        }
        public ActionResult Consultar(int IdEmpresa = 0, string IdCtaCble = "", bool Exito=false)
        {
            ct_plancta_Info model = bus_plancta.get_info(IdEmpresa, IdCtaCble);
            model.IdClasificacionEBIT = (model.IdClasificacionEBIT == null ? 0 : model.IdClasificacionEBIT);
            model.IdTipo_Gasto = (model.IdTipo_Gasto == null ? 0 : model.IdTipo_Gasto);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "Contabilidad", "PlanDeCuentas", "Index");
            if (model.pc_Estado == "I")
            {
                info.Modificar = false;
                info.Anular = false;
            }
            model.Nuevo = (info.Nuevo == true ? 1 : 0);
            model.Modificar = (info.Modificar == true ? 1 : 0);
            model.Anular = (info.Anular == true ? 1 : 0);
            #endregion

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        public ActionResult Modificar(int IdEmpresa = 0, string IdCtaCble = "")
        {   
            ct_plancta_Info model = bus_plancta.get_info(IdEmpresa, IdCtaCble);
            model.IdClasificacionEBIT = (model.IdClasificacionEBIT == null ? 0 : model.IdClasificacionEBIT);
            model.IdTipo_Gasto = (model.IdTipo_Gasto == null ? 0 : model.IdTipo_Gasto);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "Contabilidad", "PlanDeCuentas", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion

            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ct_plancta_Info model)
        {
            if (!bus_plancta.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdCtaCble = model.IdCtaCble, Exito = true });
        }
        public ActionResult Anular(int IdEmpresa = 0, string IdCtaCble = "")
        {
            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "Contabilidad", "PlanDeCuentas", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion

            ct_plancta_Info model = bus_plancta.get_info(IdEmpresa, IdCtaCble);
            model.IdClasificacionEBIT = (model.IdClasificacionEBIT == null ? 0 : model.IdClasificacionEBIT);
            model.IdTipo_Gasto = (model.IdTipo_Gasto == null ? 0 : model.IdTipo_Gasto);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ct_plancta_Info model)
        {
            if (!bus_plancta.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Importacion
        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings.UploadValidationSettings, UploadControlSettings.FileUploadComplete);
            return null;
        }
        public ActionResult Importar(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ct_plancta_Info model = new ct_plancta_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(ct_plancta_Info model)
        {
            var Lista = ListaPlancta.get_list(model.IdTransaccionSession);
            foreach (var item in Lista)
            {
                bus_plancta.guardarDB(item);
            }
            var ListaAnio = ListaAnioFiscal.get_list(model.IdTransaccionSession);
            foreach (var item in ListaAnio)
            {
                bus_anio_fiscal.guardarDB(item);
            }
            return RedirectToAction("Index");
        }
        public ActionResult GridViewPartial_plancta_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaPlancta.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_plancta_importacion", model);
        }

        public ActionResult GridViewPartial_anio_fiscal_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaAnioFiscal.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_anio_fiscal_importacion", model);
        }
        public JsonResult ActualizarVariablesSession(int IdEmpresa = 0, decimal IdTransaccionSession = 0)
        {
            string retorno = string.Empty;
            SessionFixed.IdEmpresa = IdEmpresa.ToString();
            SessionFixed.IdTransaccionSession = IdTransaccionSession.ToString();
            SessionFixed.IdTransaccionSessionActual = IdTransaccionSession.ToString();
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public JsonResult get_info_nuevo(int IdEmpresa = 0, string IdCtaCble_padre = "")
        {
            var resultado = bus_plancta.get_info_nuevo(IdEmpresa, IdCtaCble_padre);
            
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }

    public class UploadControlSettings
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            #region Variables
            ct_anio_fiscal_List ListaAnioFiscal = new ct_anio_fiscal_List();
            ct_plancta_List ListaPlancta = new ct_plancta_List();
            List<ct_plancta_Info> ListaPlan = new List<ct_plancta_Info>();
            List<ct_anio_fiscal_Info> ListaAnio = new List<ct_anio_fiscal_Info>();

            int cont = 0;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            #endregion


            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                
                #region Plan de cuentas                
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        //var IdCtaCble = Convert.ToString(reader.GetValue(0));
                        //var pc_clave_corta = reader.GetValue(1) == null || string.IsNullOrEmpty(reader.GetString(1)) ? "" : reader.GetString(1);
                        //var pc_Cuenta = reader.GetString(2);
                        //var IdCtaCIdCtaCblePadreble = reader.GetValue(3) == null || string.IsNullOrEmpty(Convert.ToString(reader.GetValue(3))) ? null : Convert.ToString(reader.GetValue(3));
                        //var pc_Naturaleza = Convert.ToString(reader.GetValue(4));
                        //var IdNivelCta = Convert.ToInt32(reader.GetValue(5));
                        //var pc_EsMovimiento_bool = Convert.ToString(reader.GetValue(6)) == "SI" ? true : false;
                        //var pc_EsMovimiento = Convert.ToString(reader.GetValue(6)) == "SI" ? "S" : "N";
                        //var IdGrupoCble = Convert.ToString(reader.GetValue(7));

                        ct_plancta_Info info = new ct_plancta_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdCtaCble = Convert.ToString(reader.GetValue(0)),
                            pc_clave_corta = reader.GetValue(1) == null || string.IsNullOrEmpty(reader.GetString(1)) ? "" : reader.GetString(1),
                            pc_Cuenta = reader.GetString(2),
                            IdCtaCblePadre = reader.GetValue(3) == null || string.IsNullOrEmpty(Convert.ToString(reader.GetValue(3))) ? null : Convert.ToString(reader.GetValue(3)),
                            pc_Naturaleza = Convert.ToString(reader.GetValue(4)),
                            IdNivelCta = Convert.ToInt32(reader.GetValue(5)),
                            pc_EsMovimiento_bool = Convert.ToString(reader.GetValue(6)) == "SI" ? true : false,
                            pc_EsMovimiento = Convert.ToString(reader.GetValue(6)) == "SI" ? "S" : "N",
                            IdGrupoCble = Convert.ToString(reader.GetValue(7))
                    };
                        ListaPlan.Add(info);
                    }
                    else
                        cont++;
                }
                #endregion

                cont = 0;
                //Para avanzar a la siguiente hoja de excel
                reader.NextResult();

                #region Cuentas contables por anio
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        int Anio = Convert.ToInt32(reader.GetValue(0));
                        string IdCtaCble = reader.GetValue(1).ToString();
                        ct_anio_fiscal_Info info = new ct_anio_fiscal_Info
                        {                            
                            IdanioFiscal = Anio,
                            af_fechaIni = new DateTime(Anio, 1, 1),
                            af_fechaFin = new DateTime(Anio, 12, 31),
                            info_anio_ctautil = new ct_anio_fiscal_x_cuenta_utilidad_Info
                            {
                                IdEmpresa = IdEmpresa,
                                IdCtaCble = IdCtaCble,
                                IdanioFiscal = Anio,                                
                            },                            
                        };
                        ListaAnio.Add(info);
                    }
                    else
                        cont++;
                }
                #endregion

                ListaPlancta.set_list(ListaPlan,IdTransaccionSession);
                ListaAnioFiscal.set_list(ListaAnio, IdTransaccionSession);
            }
        }
    }
    public class ct_plancta_List
    {
        string Variable = "ct_plancta_Info";
        public List<ct_plancta_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession] == null)
            {
                List<ct_plancta_Info> list = new List<ct_plancta_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
            }
            return (List<ct_plancta_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession];
        }

        public void set_list(List<ct_plancta_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
        }
    }    
}