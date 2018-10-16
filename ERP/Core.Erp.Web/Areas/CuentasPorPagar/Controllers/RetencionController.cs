﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Bus.Contabilidad;
using DevExpress.Web.Mvc;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    [SessionTimeout]
    public class RetencionController : Controller
    {
          #region variables
            cp_retencion_Bus bus_retencion = new cp_retencion_Bus();
            cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
            ct_cbtecble_det_List_re List_ct_cbtecble_det_List = new ct_cbtecble_det_List_re();
             cp_retencion_det_lst List_cp_retencion_det = new cp_retencion_det_lst();

        List<cp_retencion_det_Info> lst_detalle_ret = new List<cp_retencion_det_Info>();
            List<cp_retencion_Info> lst_retenciones = new List<cp_retencion_Info>();
            cp_parametros_Info info_param_op = new cp_parametros_Info();
            cp_parametros_Bus bus_parametros = new cp_parametros_Bus();
            List<cp_codigo_SRI_Info> lst_codigo_retencion = new List<cp_codigo_SRI_Info>();
            cp_codigo_SRI_Bus bus_codigo_ret = new cp_codigo_SRI_Bus();
            int IdEmpresa = 0;
        #endregion

          #region vistas
           public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);
        }

        public ActionResult GridViewPartial_retenciones(DateTime? Fecha_ini, DateTime? Fecha_fin)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            List<cp_retencion_Info> model = new List<cp_retencion_Info>();
            model = bus_retencion.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_retenciones", model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_retencion_det()
        {
            try
            {
                cargar_combos_detalle();
                SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
                var model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                return PartialView("_GridViewPartial_retencion_det", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult GridViewPartial_retencio_dc()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_retencio_dc", model);
        }
        #endregion
           
          #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0 , int IdTipoCbte_Ogiro = 0, decimal IdCbteCble_Ogiro = 0)

        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cp_retencion_Info model = new cp_retencion_Info
            {
                IdEmpresa = IdEmpresa
            };
            
            

            Session["info_param_op"] = bus_parametros.get_info(IdEmpresa);
            model = bus_retencion.get_info_factura(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
            model.fecha = model.fecha;
            if (model.co_valoriva > 0)
                Session["co_valoriva"] = model.co_valoriva;
            cargar_combos(IdEmpresa);
            cargar_combos_detalle();
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            List_cp_retencion_det.set_list(new List<cp_retencion_det_Info>(), model.IdTransaccionSession);
            List_ct_cbtecble_det_List.set_list(new List<ct_cbtecble_det_Info>(), model.IdTransaccionSession);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(cp_retencion_Info model)
        {
            bus_retencion = new cp_retencion_Bus();
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            model.detalle = List_cp_retencion_det.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            model.info_comprobante.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            info_param_op = Session["info_param_op"] as cp_parametros_Info;
            model.info_comprobante.IdTipoCbte = (int)info_param_op.pa_IdTipoCbte_x_Retencion;

            string mensaje = bus_retencion.validar(model);
            if (mensaje != "")
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                cargar_combos_detalle();
                return View(model);
            }
            else
            {
                lst_codigo_retencion = Session["lst_codigo_retencion"] as List<cp_codigo_SRI_Info>;
                model.detalle.ForEach(item =>
                {
                    cp_codigo_SRI_Info info_ = lst_codigo_retencion.Where(v => v.codigoSRI == item.re_Codigo_impuesto).FirstOrDefault();
                    item.IdCodigo_SRI = info_.IdCodigo_SRI;
                    if (info_.IdTipoSRI == "COD_RET_IVA")
                    {
                        model.re_Tiene_RFuente = "S";
                        item.re_tipoRet = "IVA";
                    }
                    if (info_.IdTipoSRI == "COD_RET_FUE")
                    {
                        model.re_Tiene_RTiva = "S";
                        item.re_tipoRet = "RTF";
                    }
                });

                if (bus_retencion.guardarDB(model))
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.mensaje = mensaje;
                    cargar_combos(model.IdEmpresa);
                    cargar_combos_detalle();
                    return View(model);
                }
            }
        }
        public ActionResult Modificar(int IdEmpresa = 0 , int IdRetencion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cargar_combos(IdEmpresa);
            cargar_combos_detalle();
            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            Session["info_param_op"] = bus_parametros.get_info(IdEmpresa);
            cp_retencion_Info model = new cp_retencion_Info();
            model = bus_retencion.get_info(IdEmpresa, IdRetencion);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            List_ct_cbtecble_det_List.set_list(model.info_comprobante.lst_ct_cbtecble_det, model.IdTransaccionSession);
            List_cp_retencion_det.set_list(model.detalle, model.IdTransaccionSession);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(cp_retencion_Info model)
        {

            bus_retencion = new cp_retencion_Bus();
            model.IdUsuario = Session["IdUsuario"].ToString();
            model.detalle = List_cp_retencion_det.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            model.info_comprobante.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            info_param_op = Session["info_param_op"] as cp_parametros_Info;
            model.info_comprobante.IdTipoCbte = (int)info_param_op.pa_IdTipoCbte_x_Retencion;

            string mensaje = bus_retencion.validar(model);
            if (mensaje != "")
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                cargar_combos_detalle();
                return View(model);
            }
            else
            {
                lst_codigo_retencion = Session["lst_codigo_retencion"] as List<cp_codigo_SRI_Info>;
                model.detalle.ForEach(item =>
                {
                    cp_codigo_SRI_Info info_ = lst_codigo_retencion.Where(v => v.codigoSRI == item.re_Codigo_impuesto).FirstOrDefault();
                    item.IdCodigo_SRI = info_.IdCodigo_SRI;
                    if (info_.IdTipoSRI == "COD_RET_IVA")
                    {
                        model.re_Tiene_RFuente = "S";
                        item.re_tipoRet = "IVA";
                    }
                    if (info_.IdTipoSRI == "COD_RET_FUE")
                    {
                        model.re_Tiene_RTiva = "S";
                        item.re_tipoRet = "RTF";
                    }
                });

                if (bus_retencion.modificarDB(model))
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.mensaje = mensaje;
                    cargar_combos(model.IdEmpresa);
                    cargar_combos_detalle();
                    return View(model);
                }
            }
        }
        public ActionResult Anular(int IdEmpresa = 0 ,int IdRetencion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cargar_combos(IdEmpresa);
            cargar_combos_detalle();
            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            Session["info_param_op"] = bus_parametros.get_info(IdEmpresa);
            cp_retencion_Info model = new cp_retencion_Info();
            model = bus_retencion.get_info(IdEmpresa, IdRetencion);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            List_ct_cbtecble_det_List.set_list(model.info_comprobante.lst_ct_cbtecble_det, model.IdTransaccionSession);
            List_cp_retencion_det.set_list(model.detalle, model.IdTransaccionSession);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(cp_retencion_Info model)
        {

            bus_retencion = new cp_retencion_Bus();
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            model.detalle = List_cp_retencion_det.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            model.info_comprobante.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            info_param_op = Session["info_param_op"] as cp_parametros_Info;
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.info_comprobante.IdTipoCbte = (int)info_param_op.pa_IdTipoCbte_x_Retencion;

            string mensaje = bus_retencion.validar(model);
            if (mensaje != "")
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                cargar_combos_detalle();
                return View(model);
            }
            else
            {
                lst_codigo_retencion = Session["lst_codigo_retencion"] as List<cp_codigo_SRI_Info>;
                model.detalle.ForEach(item =>
                {
                    cp_codigo_SRI_Info info_ = lst_codigo_retencion.Where(v => v.codigoSRI == item.re_Codigo_impuesto).FirstOrDefault();
                    item.IdCodigo_SRI = info_.IdCodigo_SRI;
                    if (info_.IdTipoSRI == "COD_RET_IVA")
                    {
                        model.re_Tiene_RFuente = "S";
                        item.re_tipoRet = "IVA";
                    }
                    if (info_.IdTipoSRI == "COD_RET_FUE")
                    {
                        model.re_Tiene_RTiva = "S";
                        item.re_tipoRet = "RTF";
                    }
                });

                if (bus_retencion.anularDB(model))
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.mensaje = mensaje;
                    cargar_combos(model.IdEmpresa);
                    cargar_combos_detalle();
                    return View(model);
                }
            }
        }

        #endregion

        #region Detalle diario
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                List_ct_cbtecble_det_List.AddRow(info_det,Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_retencio_dc", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                List_ct_cbtecble_det_List.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_retencio_dc", model);
        }

        public ActionResult EditingDelete(int secuencia)
        {
            List_ct_cbtecble_det_List.DeleteRow(secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_retencio_dc", model);
        }
        #endregion

        #region Acciones collbak retenciones
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew_ret([ModelBinder(typeof(DevExpressEditorsBinder))]  cp_retencion_det_Info info_det)
        {
            cp_codigo_SRI_Info info_codifo_sri = new cp_codigo_SRI_Info();
            List<cp_retencion_det_Info> model = new List<cp_retencion_det_Info>();
            lst_codigo_retencion = Session["lst_codigo_retencion"] as List<cp_codigo_SRI_Info>;
            info_codifo_sri = lst_codigo_retencion.Where(v => v.codigoSRI == info_det.re_Codigo_impuesto).FirstOrDefault();
            info_det.re_Porcen_retencion = info_codifo_sri.co_porRetencion;
            if (info_codifo_sri.IdTipoSRI == "COD_RET_IVA")
            {
                if (info_det.re_baseRetencion != 0)
                {                    
                    info_det.re_valor_retencion = (info_det.re_baseRetencion * info_codifo_sri.co_porRetencion) / 100;
                    info_det.IdCtacble = info_codifo_sri.info_codigo_ctacble.IdCtaCble;

                    // calculando valores retencion
                    List_cp_retencion_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                    model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

                }
            }
            else
            {
                if (info_codifo_sri.co_porRetencion != 0 & info_det.re_baseRetencion != null & info_det.re_baseRetencion != 0)
                {
                    info_det.re_valor_retencion = (info_det.re_baseRetencion * info_codifo_sri.co_porRetencion) / 100;
                    info_det.IdCtacble = info_codifo_sri.info_codigo_ctacble.IdCtaCble;


                    // calculando valores retencion
                    List_cp_retencion_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                    model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));


                }


            }



            cargar_combos_detalle();
            model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_retencion_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_ret([ModelBinder(typeof(DevExpressEditorsBinder))] cp_retencion_det_Info info_det)
        {
            cp_codigo_SRI_Info info_codifo_sri = new cp_codigo_SRI_Info();
            List<cp_retencion_det_Info> model = new List<cp_retencion_det_Info>();
            lst_codigo_retencion = Session["lst_codigo_retencion"] as List<cp_codigo_SRI_Info>;
            info_codifo_sri = lst_codigo_retencion.Where(v => v.codigoSRI == info_det.re_Codigo_impuesto).FirstOrDefault();
            info_det.re_Porcen_retencion = info_codifo_sri.co_porRetencion;
            if (info_codifo_sri.IdTipoSRI == "COD_RET_IVA")
            {
                if (info_det.re_baseRetencion != 0)
                {
                    info_det.re_baseRetencion = info_det.re_baseRetencion;
                    info_det.re_valor_retencion = (info_det.re_baseRetencion * info_codifo_sri.co_porRetencion) / 100;
                    info_det.IdCtacble = info_codifo_sri.info_codigo_ctacble.IdCtaCble;
                    List_cp_retencion_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                    model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                }
            }
            else
            {
                if (info_codifo_sri.co_porRetencion != 0 & info_det.re_baseRetencion != null & info_det.re_baseRetencion != 0)
                {
                    info_det.re_valor_retencion = (info_det.re_baseRetencion * info_codifo_sri.co_porRetencion) / 100;
                    info_det.IdCtacble = info_codifo_sri.info_codigo_ctacble.IdCtaCble;
                    info_det.re_Codigo_impuesto = info_det.re_Codigo_impuesto;
                    List_cp_retencion_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                    model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                }
            }

            cargar_combos_detalle();
            model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_retencion_det", model);
        }

        public ActionResult EditingDelete_ret(int Idsecuencia)
        {
            List_cp_retencion_det.DeleteRow(Idsecuencia);
            List<cp_retencion_det_Info> model = new List<cp_retencion_det_Info>();
            model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_retencion_det", model);
        }

        #endregion

         #region json

        public JsonResult armar_diario(decimal IdProveedor=0, decimal IdTransaccionSession = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);

            var datos = bus_proveedor.get_info(IdEmpresa, IdProveedor);
            if (datos == null)
                datos = new cp_proveedor_Info();
            var detalle_ret = List_cp_retencion_det.get_list(IdTransaccionSession);
            var param_op = bus_parametros.get_info(IdEmpresa);
            List_ct_cbtecble_det_List.delete_detail_New_details(param_op,detalle_ret, IdTransaccionSession, datos.IdCtaCble_CXP);

            return Json("", JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Cargar combos
        private void cargar_combos(int IdEmpresa)
        {
            var lst_proveedores = bus_proveedor.get_list(IdEmpresa, false);
            ViewBag.lst_proveedores = lst_proveedores;
        
        }
        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ct_plancta_Bus bus_cuenta = new ct_plancta_Bus();
            var lst_cuentas = bus_cuenta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cuentas = lst_cuentas;
            lst_codigo_retencion = bus_codigo_ret.get_list_cod_ret(false, IdEmpresa);
            ViewBag.lst_codigo_retencion = lst_codigo_retencion;
            Session["lst_codigo_retencion"] = lst_codigo_retencion;
        }
        #endregion
    }

    public class ct_cbtecble_det_List_re
    {
        string variable = "ct_cbtecble_det_Info";
        public List<ct_cbtecble_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ct_cbtecble_det_Info> list = new List<ct_cbtecble_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ct_cbtecble_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ct_cbtecble_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ct_cbtecble_det_Info info_det, decimal IdTransaccionSession)
        {
            List<ct_cbtecble_det_Info> list = get_list(IdTransaccionSession);
            info_det.secuencia = list.Count == 0 ? 1 : list.Max(q => q.secuencia) + 1;
            info_det.dc_Valor = info_det.dc_Valor_debe > 0 ? info_det.dc_Valor_debe : info_det.dc_Valor_haber * -1;
            list.Add(info_det);
        }

        public void UpdateRow(ct_cbtecble_det_Info info_det, decimal IdTransaccionSession)
        {
            ct_cbtecble_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.secuencia == info_det.secuencia).First();
            edited_info.IdCtaCble = info_det.IdCtaCble;
            edited_info.dc_para_conciliar = info_det.dc_para_conciliar;
            edited_info.dc_Valor = info_det.dc_Valor_debe > 0 ? info_det.dc_Valor_debe : info_det.dc_Valor_haber * -1;
            edited_info.dc_Valor_debe = info_det.dc_Valor_debe;
            edited_info.dc_Valor_haber = info_det.dc_Valor_haber;
        }

        public void DeleteRow(int secuencia, decimal IdTransaccionSession)
        {
            List<ct_cbtecble_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.secuencia == secuencia).First());
        }

        public void delete_detail_New_details(cp_parametros_Info info_param_op, List<cp_retencion_det_Info> detalle_retencion, decimal IdTransaccionSession, string IdCtaCble = "")
        {
            try
            {
                int sec = 2;

                set_list(new List<ct_cbtecble_det_Info>(), IdTransaccionSession);

                ct_cbtecble_det_Info cbtecble_haber_Info = new ct_cbtecble_det_Info();
                cbtecble_haber_Info.secuencia = 1;
                cbtecble_haber_Info.IdEmpresa = info_param_op.IdEmpresa;
                cbtecble_haber_Info.IdTipoCbte = (int)info_param_op.pa_IdTipoCbte_x_Retencion;
                cbtecble_haber_Info.IdCtaCble = IdCtaCble;
                cbtecble_haber_Info.dc_Valor_debe = Math.Round(detalle_retencion.Sum(v => Convert.ToDouble(v.re_valor_retencion)),2,MidpointRounding.AwayFromZero);
                cbtecble_haber_Info.dc_Valor = Math.Round(detalle_retencion.Sum(v => Convert.ToDouble(v.re_valor_retencion)),2,MidpointRounding.AwayFromZero);
                cbtecble_haber_Info.dc_Observacion = "";
                AddRow(cbtecble_haber_Info, IdTransaccionSession);

                foreach (var item in detalle_retencion)
                {
                    ct_cbtecble_det_Info cbtecble_debe_Info = new ct_cbtecble_det_Info();
                    cbtecble_debe_Info.secuencia = sec;
                    cbtecble_debe_Info.IdEmpresa = info_param_op.IdEmpresa;
                    cbtecble_debe_Info.IdTipoCbte = (int)info_param_op.pa_IdTipoCbte_x_Retencion;
                    cbtecble_debe_Info.IdCtaCble = item.IdCtacble;
                    cbtecble_debe_Info.dc_Valor_haber = Math.Round((double)item.re_valor_retencion,2,MidpointRounding.AwayFromZero);
                    cbtecble_debe_Info.dc_Valor = Math.Round((double)item.re_valor_retencion,2,MidpointRounding.AwayFromZero) *-1;
                    cbtecble_debe_Info.dc_Observacion = "";
                    sec++;
                    AddRow(cbtecble_debe_Info, IdTransaccionSession);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class cp_retencion_det_lst
    {
        string variable = " cp_retencion_det_Info";
        public List<cp_retencion_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable+IdTransaccionSession.ToString()] == null)
            {
                List<cp_retencion_det_Info> list = new List<cp_retencion_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_retencion_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_retencion_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cp_retencion_det_Info info_det, decimal IdTransaccionSession)
        {
            List<cp_retencion_det_Info> list = get_list(IdTransaccionSession);
            info_det.Idsecuencia = list.Count() + 1;
            info_det.re_valor_retencion = Math.Round((double)info_det.re_valor_retencion, 2, MidpointRounding.AwayFromZero);
            list.Add(info_det);
        }

        public void UpdateRow(cp_retencion_det_Info info_det, decimal IdTransaccionSession)
        {
            cp_retencion_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Idsecuencia == info_det.Idsecuencia).First();
            edited_info.re_Codigo_impuesto = info_det.re_Codigo_impuesto;
            edited_info.IdCodigo_SRI = info_det.IdCodigo_SRI;
            edited_info.re_baseRetencion = info_det.re_baseRetencion;
            edited_info.re_Porcen_retencion = info_det.re_Porcen_retencion;
            edited_info.re_valor_retencion = Math.Round((double)info_det.re_valor_retencion, 2, MidpointRounding.AwayFromZero);
        }

        public void DeleteRow(int secuencia)
        {
            List<cp_retencion_det_Info> list = get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            list.Remove(list.Where(m => m.Idsecuencia == secuencia).First());
        }

   

    }

}