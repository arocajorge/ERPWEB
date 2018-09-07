﻿using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Web.Helps;
using System;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class TipoDocumentoTalonarioController : Controller
    {
        #region Variables
        tb_sis_Documento_Tipo_Talonario_Bus bus_talonario = new tb_sis_Documento_Tipo_Talonario_Bus();
            tb_sis_Documento_Tipo_Bus bus_tipodoc = new tb_sis_Documento_Tipo_Bus();
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        #endregion
        #region Index / Metodos
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tipodocumentotal()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_talonario.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_tipodocumentotal", model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_talonario = bus_tipodoc.get_list(false);
            ViewBag.lst_talonario = lst_talonario;
            
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;
        }

        #endregion
        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0 )
        {
            tb_sis_Documento_Tipo_Talonario_Info model = new tb_sis_Documento_Tipo_Talonario_Info
            {
                IdEmpresa = IdEmpresa,
                FechaCaducidad = DateTime.Now
            };
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(tb_sis_Documento_Tipo_Talonario_Info model)
        {
            decimal documento_inicial = Convert.ToDecimal(model.NumDocumento);
            decimal documento_final = Convert.ToDecimal(model.Documentofinal);
            for (decimal i = documento_inicial; i < documento_final; i++)
            {
                tb_sis_Documento_Tipo_Talonario_Info info = new tb_sis_Documento_Tipo_Talonario_Info
                {
                    IdEmpresa = model.IdEmpresa,
                    CodDocumentoTipo = model.CodDocumentoTipo,
                    Establecimiento = model.Establecimiento,
                    PuntoEmision = model.PuntoEmision,
                    NumDocumento = i.ToString("000000000"),
                   es_Documento_Electronico = model.es_Documento_Electronico,
                   FechaCaducidad = model.FechaCaducidad,
                   IdSucursal = model.IdSucursal,
                   NumAutorizacion = model.NumAutorizacion,
                   Usado = model.Usado,                    
                };
                if (!bus_talonario.guardarDB(info))
                {
                    cargar_combos(model.IdEmpresa);
                    return View(model);
                }
            }           

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0 , string CodDocumentoTipo = "", string Establecimiento = "", string PuntoEmision = "", string NumDocumento = "")
        {
            tb_sis_Documento_Tipo_Talonario_Info model = bus_talonario.get_info(IdEmpresa, CodDocumentoTipo, Establecimiento, PuntoEmision, NumDocumento);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(tb_sis_Documento_Tipo_Talonario_Info model)
        {
            if (!bus_talonario.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");

        }

        public ActionResult Anular(int IdEmpresa = 0 , string CodDocumentoTipo = "", string Establecimiento = "", string PuntoEmision = "", string NumDocumento = "")
        {
            tb_sis_Documento_Tipo_Talonario_Info model = bus_talonario.get_info(IdEmpresa, CodDocumentoTipo, Establecimiento, PuntoEmision, NumDocumento);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(tb_sis_Documento_Tipo_Talonario_Info model)
        {
            if (!bus_talonario.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
        #region Json
        public JsonResult get_NumeroDocumentoInicial (int IdEmpresa = 0 , string CodDocumentoTipo="", string Establecimiento="", string PuntoEmision = "")
        {
            var NumeroDocumento = bus_talonario.get_NumeroDocumentoInicial(IdEmpresa, CodDocumentoTipo, Establecimiento, PuntoEmision);

            return Json(NumeroDocumento, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}