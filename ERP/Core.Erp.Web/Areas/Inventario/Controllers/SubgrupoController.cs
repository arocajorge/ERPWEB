﻿
using Core.Erp.Bus.Inventario;
using Core.Erp.Bus.SeguridadAcceso;
using Core.Erp.Info.Inventario;
using Core.Erp.Info.SeguridadAcceso;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class SubgrupoController : Controller
    {
        #region Variables
        in_subgrupo_Bus bus_subgrupo = new in_subgrupo_Bus();
        in_categorias_Bus bus_categoria = new in_categorias_Bus();
        in_linea_Bus bus_linea = new in_linea_Bus();
        in_grupo_Bus bus_grupo = new in_grupo_Bus();
        in_subgrupo_List Lista_SubGrupo = new in_subgrupo_List();
        seg_Menu_x_Empresa_x_Usuario_Bus bus_permisos = new seg_Menu_x_Empresa_x_Usuario_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";

        #endregion
        #region Index/Metodos
        public ActionResult Index(int IdEmpresa = 0 , string IdCategoria = "", int IdLinea = 0, int IdGrupo = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "Inventario", "Categoria", "Index");
            ViewBag.Nuevo = info.Nuevo;
            #endregion

            in_subgrupo_Info model = new in_subgrupo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdCategoria = IdCategoria,
                IdLinea = IdLinea,
                IdGrupo = IdGrupo,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
            };

            var lst = bus_subgrupo.get_list(model.IdEmpresa,model.IdCategoria, model.IdLinea, model.IdGrupo, true);
            Lista_SubGrupo.set_list(lst, model.IdTransaccionSession);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_subgrupo(int IdEmpresa = 0 , string IdCategoria = "", int IdLinea = 0, int IdGrupo = 0, bool Nuevo=false)
        {
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdCategoria = IdCategoria;
            ViewBag.IdLinea = IdLinea;
            ViewBag.IdGrupo = IdGrupo;
            ViewBag.Nuevo = Nuevo;
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_SubGrupo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_subgrupo", model);
        }
        private void cargar_combos(int IdEmpresa, string IdCategoria, int IdLinea)
        {
            var lst_categoria = bus_categoria.get_list(IdEmpresa, true);
            ViewBag.lst_categorias = lst_categoria;


            var lst_linea = bus_linea.get_list(IdEmpresa, IdCategoria, true);
            ViewBag.lst_lineas = lst_linea;

            var lst_grupo = bus_grupo.get_list(IdEmpresa, IdCategoria, IdLinea, true);
            ViewBag.lst_grupos = lst_grupo;
        }
        #endregion
        #region Acciones

        public ActionResult Nuevo(int IdEmpresa = 0 , string IdCategoria = "", int IdLinea = 0, int IdGrupo = 0)
        {
            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "Inventario", "Categoria", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion

            in_subgrupo_Info model = new in_subgrupo_Info
            {
                IdEmpresa= IdEmpresa,
                IdCategoria = IdCategoria,
                IdLinea = IdLinea,
                IdGrupo = IdGrupo
            };
            ViewBag.IdCategoria = IdCategoria;
            ViewBag.IdLinea = IdLinea;
            ViewBag.IdGrupo = IdGrupo;
            cargar_combos(IdEmpresa, IdCategoria, IdLinea);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(in_subgrupo_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            if(!bus_subgrupo.guardarDB(model))
            {
                ViewBag.IdCategoria = model.IdCategoria;
                ViewBag.IdLinea = model.IdLinea;
                ViewBag.IdGrupo = model.IdGrupo;
                cargar_combos(model.IdEmpresa, model.IdCategoria, model.IdLinea);
                return View(model);
            }
            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdCategoria = model.IdCategoria, IdLinea = model.IdLinea, IdGrupo = model.IdGrupo });
        }

        public ActionResult Consultar(int IdEmpresa = 0, string IdCategoria = "", int IdLinea = 0, int IdGrupo = 0, int IdSubgrupo = 0, bool Exito=false)
        {
            in_subgrupo_Info model = bus_subgrupo.get_info(IdEmpresa, IdCategoria, IdLinea, IdGrupo, IdSubgrupo);
            if (model == null)
            {
                ViewBag.IdCategoria = IdCategoria;
                ViewBag.IdLinea = IdLinea;
                ViewBag.IdGrupo = IdGrupo;
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdCategoria = IdCategoria, IdLinea = IdLinea, IdGrupo = IdGrupo });
            }

            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "Inventario", "Categoria", "Index");
            if (model.Estado == "I")
            {
                info.Modificar = false;
                info.Anular = false;
            }
            model.Nuevo = (info.Nuevo == true ? 1 : 0);
            model.Modificar = (info.Modificar == true ? 1 : 0);
            model.Anular = (info.Anular == true ? 1 : 0);
            #endregion

            cargar_combos(IdEmpresa, IdCategoria, IdLinea);
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            return View(model);
        }

        public ActionResult Modificar(int IdEmpresa = 0 , string IdCategoria = "", int IdLinea = 0, int IdGrupo = 0, int IdSubgrupo = 0)
        {
            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "Inventario", "Categoria", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion
            in_subgrupo_Info model = bus_subgrupo.get_info(IdEmpresa, IdCategoria, IdLinea, IdGrupo, IdSubgrupo);
            if(model==null)
            {
                ViewBag.IdCategoria = IdCategoria;
                ViewBag.IdLinea = IdLinea;
                ViewBag.IdGrupo = IdGrupo;
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdCategoria = IdCategoria, IdLinea = IdLinea, IdGrupo = IdGrupo});
            }
            cargar_combos(IdEmpresa, IdCategoria, IdLinea);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(in_subgrupo_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario.ToString();
            if (!bus_subgrupo.modificarDB(model))
            {
                ViewBag.IdCategoria = model.IdCategoria;
                ViewBag.IdLinea = model.IdLinea;
                ViewBag.IdGrupo = model.IdGrupo;
                cargar_combos(model.IdEmpresa, model.IdCategoria, model.IdLinea);
                return View(model);
            }
            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdCategoria = model.IdCategoria, IdLinea = model.IdLinea, IdGrupo = model.IdGrupo });
        }

        public ActionResult Anular(int IdEmpresa = 0 , string IdCategoria = "", int IdLinea = 0, int IdGrupo = 0, int IdSubgrupo = 0)
        {
            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "Inventario", "Categoria", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
            in_subgrupo_Info model = bus_subgrupo.get_info(IdEmpresa, IdCategoria, IdLinea, IdGrupo, IdSubgrupo);
            if (model == null)
            {
                ViewBag.IdCategoria = IdCategoria;
                ViewBag.IdLinea = IdLinea;
                ViewBag.IdGrupo = IdGrupo;
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdCategoria = IdCategoria, IdLinea = IdLinea, IdGrupo = IdGrupo });
            }
            cargar_combos(IdEmpresa, IdCategoria, IdLinea);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(in_subgrupo_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario.ToString();
            if (!bus_subgrupo.anularDB(model))
            {
                ViewBag.IdCategoria = model.IdCategoria;
                ViewBag.IdLinea = model.IdLinea;
                ViewBag.IdGrupo = model.IdGrupo;
                cargar_combos(model.IdEmpresa, model.IdCategoria, model.IdLinea);
                return View(model);
            }
            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdCategoria = model.IdCategoria, IdLinea = model.IdLinea, IdGrupo = model.IdGrupo });
        }
        #endregion
    }
    public class in_subgrupo_List
    {
        string Variable = "in_subgrupo_Info";
        public List<in_subgrupo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_subgrupo_Info> list = new List<in_subgrupo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_subgrupo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_subgrupo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

}