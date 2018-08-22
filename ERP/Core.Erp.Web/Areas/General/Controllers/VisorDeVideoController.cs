﻿using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.General.Controllers
{
    public class VisorDeVideoController : Controller
    {
        Visor_video_Bus bus_pais = new Visor_video_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_visor_video()
        {
            List<Visor_video_Info> model = new List<Visor_video_Info>();
            model = bus_pais.get_list(true);
            return PartialView("_GridViewPartial_visor_video", model);
        }

        public ActionResult Nuevo()
        {
            Visor_video_Info model = new Visor_video_Info();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(Visor_video_Info model)
        {
            if (!bus_pais.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(string Cod_video = "")
        {
            Visor_video_Info model = bus_pais.get_info(Cod_video);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(Visor_video_Info model)
        {
            if (!bus_pais.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(string Cod_video)
        {
            Visor_video_Info model = bus_pais.get_info(Cod_video);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(Visor_video_Info model)
        {
            if (!bus_pais.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}