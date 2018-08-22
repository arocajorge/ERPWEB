﻿using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    public class GrupoController : Controller
    {
        in_grupo_Bus bus_grupo = new in_grupo_Bus();
            in_categorias_Bus bus_categoria = new in_categorias_Bus();
            in_linea_Bus bus_linea = new in_linea_Bus();
        public ActionResult Index(int IdEmpresa = 0 , string IdCategoria = "",int IdLinea = 0)
        {
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdCategoria = IdCategoria;
            ViewBag.IdLinea = IdLinea;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_grupo(int IdEmpresa= 0 , string IdCategoria = "", int IdLinea = 0)
        {
            ViewBag.IdCategoria = IdCategoria;
            ViewBag.IdLinea = IdLinea;
            var model = bus_grupo.get_list(IdEmpresa,IdCategoria, IdLinea, true);
            return PartialView("_GridViewPartial_grupo", model);
        }
        private void cargar_combos(int IdEmpresa , string IdCategoria)
        {
            var lst_categoria = bus_categoria.get_list(IdEmpresa, false);
            ViewBag.lst_categorias = lst_categoria;


            var lst_linea = bus_linea.get_list(IdEmpresa, IdCategoria, false);
            ViewBag.lst_lineas = lst_linea;


        }
        
        public ActionResult Nuevo(int IdEmpresa = 0 , string IdCategoria ="", int IdLinea = 0)
        {
            in_grupo_Info model = new in_grupo_Info
            {
                 IdEmpresa = IdEmpresa,
                IdCategoria = IdCategoria,
                IdLinea = IdLinea
            };
            cargar_combos(IdEmpresa, IdCategoria);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(in_grupo_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            if (!bus_grupo.guardarDB(model))
            {
                ViewBag.IdCategoria = model.IdCategoria;
                ViewBag.IdLinea = model.IdLinea;
                cargar_combos(model.IdEmpresa, model.IdCategoria);
                return View(model);
            }
            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdCategoria = model.IdCategoria, IdLinea = model.IdLinea });
        }
        public ActionResult Modificar(int IdEmpresa = 0 , string IdCategoria="", int IdLinea = 0, int IdGrupo = 0)
        {
            in_grupo_Info model = bus_grupo.get_info(IdEmpresa, IdCategoria, IdLinea, IdGrupo);
            if (model == null)
            {
                ViewBag.IdCategoria = IdCategoria;
                ViewBag.IdLinea = IdLinea;
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdCategoria = IdCategoria, IdLinea = IdLinea });
            }
            cargar_combos(IdEmpresa, IdCategoria);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(in_grupo_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario.ToString();
            if (!bus_grupo.modificarDB(model))
            {
                ViewBag.IdCategoria = model.IdCategoria;
                ViewBag.IdLinea = model.IdLinea;
                cargar_combos(model.IdEmpresa, model.IdCategoria);
                return View(model);
            }
            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdCategoria = model.IdCategoria, IdLinea = model.IdLinea });
        }
        public ActionResult Anular(int IdEmpresa = 0 , string IdCategoria = "", int IdLinea = 0, int IdGrupo = 0)
        {
            in_grupo_Info model = bus_grupo.get_info(IdEmpresa, IdCategoria, IdLinea, IdGrupo);
            if (model == null)
            {
                ViewBag.IdCategoria = IdCategoria;
                ViewBag.IdLinea = IdLinea;
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdCategoria = IdCategoria, IdLinea = IdLinea });
            }
            cargar_combos(IdEmpresa, IdCategoria);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(in_grupo_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario.ToString();
            if (!bus_grupo.anularDB(model))
            {
                ViewBag.IdCategoria = model.IdCategoria;
                ViewBag.IdLinea = model.IdLinea;
                cargar_combos(model.IdEmpresa, model.IdCategoria);
                return View(model);
            }
            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdCategoria = model.IdCategoria, IdLinea = model.IdLinea });
        }
    }
}