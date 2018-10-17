﻿using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Areas.Inventario.Controllers;
using Core.Erp.Web.Helps;
using Core.Erp.Web.Reportes.Inventario;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Reportes.Controllers
{
    [SessionTimeout]
    public class InventarioReportesController : Controller
    {
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        in_Producto_List List_decimal = new in_Producto_List();

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbProducto_Inventario()
        {
            SessionFixed.IdProducto_padre_dist = (!string.IsNullOrEmpty(Request.Params["IdProductoPadre"])) ? Request.Params["IdProductoPadre"].ToString() : "-1";

            cl_filtros_inventario_Info model = new cl_filtros_inventario_Info();
            return PartialView("_CmbProducto_Inventario", model);
        }
        public List<in_Producto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.SOLOHIJOS, cl_enumeradores.eModulo.INV, (string.IsNullOrEmpty(SessionFixed.IdProducto_padre_dist) ? -1 : decimal.Parse(SessionFixed.IdProducto_padre_dist)));
        }
        public in_Producto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region ProductoPadre
        public ActionResult CmbProductoPadre_Inventario()
        {
            cl_filtros_inventario_Info model = new cl_filtros_inventario_Info();
            return PartialView("_CmbProductoPadre_Inventario", model);
        }

        public List<in_Producto_Info> get_list_ProductoPadre_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.SOLOPADRES, cl_enumeradores.eModulo.INV, 0);
        }
        public in_Producto_Info get_info_producto_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region json

        public JsonResult cargar_bodega(int IdEmpresa = 0, int IdSucursal = 0)
        {
            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            var resultado = bus_bodega.get_list(IdEmpresa, IdSucursal, false);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult cargar_lineas(int IdEmpresa = 0, string IdCategoria = "")
        {
            in_linea_Bus bus_linea = new in_linea_Bus();
            var resultado = bus_linea.get_list(IdEmpresa, IdCategoria, false);
            resultado.Add(new in_linea_Info
            {
                IdEmpresa = IdEmpresa,
                IdCategoria = IdCategoria,
                IdLinea = 0,
                nom_linea = "Todos"
            });
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult cargar_grupos(int IdEmpresa = 0, string IdCategoria = "", int IdLinea = 0)
        {
            in_grupo_Bus bus_grupo = new in_grupo_Bus();
            var resultado = bus_grupo.get_list(IdEmpresa, IdCategoria, IdLinea, false);
            resultado.Add(new in_grupo_Info
            {
                IdEmpresa = IdEmpresa,
                IdCategoria = IdCategoria,
                IdLinea = IdLinea,
                IdGrupo = 0,
                nom_grupo = "Todos"
            });
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult cargar_subgrupos(int IdEmpresa = 0, string IdCategoria = "", int IdLinea = 0, int IdGrupo = 0)
        {
            in_subgrupo_Bus bus_subgrupo = new in_subgrupo_Bus();
            var resultado = bus_subgrupo.get_list(IdEmpresa, IdCategoria, IdLinea, IdGrupo, false);
            resultado.Add(new in_subgrupo_Info
            {
                IdEmpresa = IdEmpresa,
                IdCategoria = IdCategoria,
                IdLinea = IdLinea,
                IdGrupo = IdGrupo,
                IdSubgrupo = 0,
                nom_subgrupo = "Todos"
            });
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GRids
        [ValidateInput(false)]
        public ActionResult GridViewPartial_producto_lst()
        {
            var model = List_decimal.get_list();
            return PartialView("_GridViewPartial_producto_lst", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] in_Producto_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (info_det != null)
                if (info_det.IdProducto != 0)
                {
                    info_det = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                }
            List_decimal.AddRow(info_det);
            var model = List_decimal.get_list();
            return PartialView("_GridViewPartial_producto_lst", model);
        }
        public ActionResult EditingDelete(decimal IdProducto)
        {
            List_decimal.DeleteRow(IdProducto);
            var model = List_decimal.get_list();
            return PartialView("_GridViewPartial_producto_lst", model);
        }

        #endregion
        public ActionResult INV_001(int IdSucursal = 0, int IdMovi_inven_tipo = 0, decimal IdNumMovi = 0)
        {
            INV_001_Rpt model = new INV_001_Rpt();
            model.p_IdEmpresa.Value = Convert.ToInt32(Session["IdEmpresa"]);
            model.p_IdSucursal.Value = IdSucursal;
            model.p_IdMovi_inven_tipo.Value = IdMovi_inven_tipo;
            model.p_IdNumMovi.Value = IdNumMovi;
            model.usuario = Session["IdUsuario"].ToString();
            model.empresa = Session["nom_empresa"].ToString();
            model.RequestParameters = false;
            return View(model);
        }

        public ActionResult INV_002(int IdSucursal = 0, int IdMovi_inven_tipo = 0, decimal IdNumMovi = 0)
        {
            INV_002_Rpt model = new INV_002_Rpt();
            model.p_IdEmpresa.Value = Convert.ToInt32(Session["IdEmpresa"]);
            model.p_IdSucursal.Value = IdSucursal;
            model.p_IdMovi_inven_tipo.Value = IdMovi_inven_tipo;
            model.p_IdNumMovi.Value = IdNumMovi;
            model.usuario = Session["IdUsuario"].ToString();
            model.empresa = Session["nom_empresa"].ToString();
            if (IdNumMovi == 0)
                model.RequestParameters = false;
            return View(model);
        }

        public ActionResult INV_003()
        {

            cl_filtros_inventario_Info model = new cl_filtros_inventario_Info {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdCategoria = "",
                IdMarca = 0,
                IdProducto = 0
            };

            cargar_combos(model);
            INV_003_Rpt report = new INV_003_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdBodega.Value = model.IdBodega;
            report.p_IdProducto.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
            report.p_IdCategoria.Value = model.IdCategoria;
            report.p_IdLinea.Value = model.IdLinea;
            report.p_IdGrupo.Value = model.IdGrupo;
            report.p_IdSubgrupo.Value = model.IdSubGrupo;
            report.p_fecha_corte.Value = model.fecha_fin;
            report.p_mostrar_stock_0.Value = model.mostrar_saldos_en_0;
            report.p_IdMarca.Value = model.IdMarca;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();

            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult INV_003(cl_filtros_inventario_Info model)
        {
            INV_003_Rpt report = new INV_003_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdBodega.Value = model.IdBodega;
            report.p_IdProducto.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
            report.p_IdCategoria.Value = model.IdCategoria;
            report.p_IdLinea.Value = model.IdLinea;
            report.p_IdGrupo.Value = model.IdGrupo;
            report.p_IdSubgrupo.Value = model.IdSubGrupo;
            report.p_fecha_corte.Value = model.fecha_fin;
            report.p_mostrar_stock_0.Value = model.mostrar_saldos_en_0;
            report.p_IdMarca.Value = model.IdMarca;
            cargar_combos(model);

            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();

            ViewBag.Report = report;
            return View(model);
        }
        public ActionResult INV_004()
        {

            cl_filtros_inventario_Info model = new cl_filtros_inventario_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdMarca = 0,
                IdProducto = 0
            };

            cargar_combos(model);
            INV_004_Rpt report = new INV_004_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdBodega.Value = model.IdBodega;
            report.p_IdProducto.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
            report.p_IdMarca.Value = model.IdMarca;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();

            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult INV_004(cl_filtros_inventario_Info model)
        {
            INV_004_Rpt report = new INV_004_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdBodega.Value = model.IdBodega;
            report.p_IdProducto.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
            report.p_IdMarca.Value = model.IdMarca;
            cargar_combos(model);

            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();

            ViewBag.Report = report;
            return View(model);
        }

        private void cargar_combos(cl_filtros_inventario_Info model)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            int IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            var lst_bodega = bus_bodega.get_list(IdEmpresa, IdSucursal, false);
            ViewBag.lst_bodega = lst_bodega;

            in_Producto_Bus bus_producto = new in_Producto_Bus();
            var lst_producto = bus_producto.get_list(IdEmpresa, false);
            ViewBag.lst_producto = lst_producto;

            in_categorias_Bus bus_categoria = new in_categorias_Bus();
            var lst_categoria = bus_categoria.get_list(IdEmpresa, false);
            lst_categoria.Add(new in_categorias_Info
            {
                IdEmpresa = model.IdEmpresa,
                IdCategoria = "",
                ca_Categoria = "Todos"
            });
            ViewBag.lst_categoria = lst_categoria;

            in_linea_Bus bus_linea = new in_linea_Bus();
            var lst_linea = bus_linea.get_list(IdEmpresa, model.IdCategoria, false);
            lst_linea.Add(new in_linea_Info
            {
                IdEmpresa = model.IdEmpresa,
                IdLinea = 0,
                nom_linea = "Todos"
            });
            ViewBag.lst_linea = lst_linea;

            in_grupo_Bus bus_grupo = new in_grupo_Bus();
            var lst_grupo = bus_grupo.get_list(IdEmpresa, model.IdCategoria, model.IdLinea, false);
            lst_grupo.Add(new in_grupo_Info
            {
                IdEmpresa = model.IdEmpresa,
                IdGrupo = 0,
                nom_grupo = "Todos"
            });
            ViewBag.lst_grupo = lst_grupo;

            in_subgrupo_Bus bus_subgrupo = new in_subgrupo_Bus();
            var lst_subgrupo = bus_subgrupo.get_list(IdEmpresa, model.IdCategoria, model.IdLinea, model.IdGrupo, false);
            lst_subgrupo.Add(new in_subgrupo_Info
            {
                IdEmpresa = model.IdEmpresa,
                IdSubgrupo = 0,
                nom_subgrupo = "Todos"
            });
            ViewBag.lst_subgrupo = lst_subgrupo;

            in_Marca_Bus bus_marca = new in_Marca_Bus();
            var lst_marca = bus_marca.get_list(IdEmpresa, false);
            lst_marca.Add(new Info.Inventario.in_Marca_Info
            {
                IdMarca = 0,
                Descripcion = "Todas"
            });
            ViewBag.lst_marca = lst_marca;
        }

        public ActionResult INV_005(bool mostrar_detallado = false)
        {
            cl_filtros_inventario_Info model = new cl_filtros_inventario_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            cargar_combos(model);
            if (mostrar_detallado)
            {
                INV_005_detalle_Rpt model_detalle = new INV_005_detalle_Rpt();
                model_detalle.p_IdEmpresa.Value = model.IdEmpresa;
                model_detalle.p_IdSucursal.Value = model.IdSucursal;
                model_detalle.p_IdBodega.Value = model.IdBodega;
                model_detalle.P_IdProductoPadre.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
                model_detalle.p_IdProducto.Value = model.IdProducto == null ? 0 : model.IdProducto;
                model_detalle.p_IdUsuario.Value = SessionFixed.IdUsuario;
                model_detalle.p_fecha_ini.Value = model.fecha_ini;
                model_detalle.p_fecha_fin.Value = model.fecha_fin;
                model_detalle.p_mostrar_detallado.Value = model.mostrar_detallado;
                model_detalle.p_no_mostrar_valores_en_0.Value = model.no_mostrar_valores_en_0;

                model_detalle.usuario = SessionFixed.IdUsuario.ToString();
                model_detalle.empresa = SessionFixed.NomEmpresa.ToString();
                ViewBag.report = model_detalle;
            }
            else
            {
                INV_005_resumen_Rpt model_resumen = new INV_005_resumen_Rpt();
                model_resumen.p_IdEmpresa.Value = model.IdEmpresa;
                model_resumen.p_IdSucursal.Value = model.IdSucursal;
                model_resumen.p_IdBodega.Value = model.IdBodega;
                model_resumen.P_IdProductoPadre.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
                model_resumen.p_IdProducto.Value = model.IdProducto == null ? 0 : model.IdProducto;
                model_resumen.p_IdUsuario.Value = SessionFixed.IdUsuario;
                model_resumen.p_fecha_ini.Value = model.fecha_ini;
                model_resumen.p_fecha_fin.Value = model.fecha_fin;
                model_resumen.p_mostrar_detallado.Value = model.mostrar_detallado;
                model_resumen.p_no_mostrar_valores_en_0.Value = model.no_mostrar_valores_en_0;

                model_resumen.usuario = SessionFixed.IdUsuario.ToString();
                model_resumen.empresa = SessionFixed.NomEmpresa.ToString();
                ViewBag.report = model_resumen;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult INV_005(cl_filtros_inventario_Info model)
        {
            if (model.mostrar_detallado)
            {
                INV_005_detalle_Rpt report = new INV_005_detalle_Rpt();
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdSucursal.Value = model.IdSucursal;
                report.p_IdBodega.Value = model.IdBodega;
                report.P_IdProductoPadre.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
                report.p_IdProducto.Value = model.IdProducto;
                report.p_IdUsuario.Value = SessionFixed.IdUsuario;
                report.p_fecha_ini.Value = model.fecha_ini;
                report.p_fecha_fin.Value = model.fecha_fin;
                report.p_mostrar_detallado.Value = model.mostrar_detallado;
                report.p_no_mostrar_valores_en_0.Value = model.no_mostrar_valores_en_0;
                cargar_combos(model);

                report.usuario = SessionFixed.IdUsuario.ToString();
                report.empresa = SessionFixed.NomEmpresa.ToString();
                ViewBag.Report = report;
            }
            else
            {
                INV_005_resumen_Rpt report = new INV_005_resumen_Rpt();
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdSucursal.Value = model.IdSucursal;
                report.p_IdBodega.Value = model.IdBodega;
                report.P_IdProductoPadre.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
                report.p_IdProducto.Value = model.IdProducto;
                report.p_IdUsuario.Value = SessionFixed.IdUsuario;
                report.p_fecha_ini.Value = model.fecha_ini;
                report.p_fecha_fin.Value = model.fecha_fin;
                report.p_mostrar_detallado.Value = model.mostrar_detallado;
                report.p_no_mostrar_valores_en_0.Value = model.no_mostrar_valores_en_0;
                cargar_combos(model);

                report.usuario = SessionFixed.IdUsuario.ToString();
                report.empresa = SessionFixed.NomEmpresa.ToString();
                ViewBag.Report = report;
            }
            return View(model);
        }

        public ActionResult INV_006()
        {
            cl_filtros_inventario_Info model = new cl_filtros_inventario_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdProducto = 0,
                IdProductoPadre = 0
            };

            cargar_combos(model);
            if (model.mostrar_detallado)
            {
                INV_006_detalle_Rpt model_detalle = new INV_006_detalle_Rpt();
                model_detalle.p_IdEmpresa.Value = model.IdEmpresa;
                model_detalle.p_IdSucursal.Value = model.IdSucursal;
                model_detalle.p_IdBodega.Value = model.IdBodega;
                model_detalle.P_IdProductoPadre.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
                model_detalle.p_IdProducto.Value = model.IdProducto;
                model_detalle.p_IdUsuario.Value = SessionFixed.IdUsuario;
                model_detalle.p_fecha_ini.Value = model.fecha_ini;
                model_detalle.p_fecha_fin.Value = model.fecha_fin;
                model_detalle.p_mostrar_detallado.Value = model.mostrar_detallado;
                model_detalle.p_no_mostrar_valores_en_0.Value = model.no_mostrar_valores_en_0;
                model_detalle.usuario = SessionFixed.IdUsuario;
                model_detalle.empresa = SessionFixed.NomEmpresa;

                ViewBag.report = model_detalle;
            }
            else
            {
                INV_006_resumen_Rpt model_resumen = new INV_006_resumen_Rpt();
                model_resumen.p_IdEmpresa.Value = model.IdEmpresa;
                model_resumen.p_IdSucursal.Value = model.IdSucursal;
                model_resumen.p_IdBodega.Value = model.IdBodega;
                model_resumen.P_IdProductoPadre.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
                model_resumen.p_IdProducto.Value = model.IdProducto;
                model_resumen.p_IdUsuario.Value = SessionFixed.IdUsuario;
                model_resumen.p_fecha_ini.Value = model.fecha_ini;
                model_resumen.p_fecha_fin.Value = model.fecha_fin;
                model_resumen.p_mostrar_detallado.Value = model.mostrar_detallado;
                model_resumen.p_no_mostrar_valores_en_0.Value = model.no_mostrar_valores_en_0;
                
                model_resumen.usuario = SessionFixed.IdUsuario;
                model_resumen.empresa = SessionFixed.NomEmpresa;

                ViewBag.report = model_resumen;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult INV_006(cl_filtros_inventario_Info model)
        {
            if (model.mostrar_detallado)
            {
                INV_006_detalle_Rpt report = new INV_006_detalle_Rpt();
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdSucursal.Value = model.IdSucursal;
                report.p_IdBodega.Value = model.IdBodega;
                report.P_IdProductoPadre.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
                report.p_IdProducto.Value = model.IdProducto == null ? 0 : model.IdProducto;
                report.p_IdUsuario.Value = SessionFixed.IdUsuario;
                report.p_fecha_ini.Value = model.fecha_ini;
                report.p_fecha_fin.Value = model.fecha_fin;
                report.p_mostrar_detallado.Value = model.mostrar_detallado;
                report.p_no_mostrar_valores_en_0.Value = model.no_mostrar_valores_en_0;
                cargar_combos(model);

                report.usuario = SessionFixed.IdUsuario;
                report.empresa = SessionFixed.NomEmpresa;
                
                    report.RequestParameters = false;
                ViewBag.Report = report;
            }
            else
            {
                INV_006_resumen_Rpt report = new INV_006_resumen_Rpt();
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdSucursal.Value = model.IdSucursal;
                report.p_IdBodega.Value = model.IdBodega;
                report.P_IdProductoPadre.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
                report.p_IdProducto.Value = model.IdProducto == null ? 0 : model.IdProducto;
                report.p_IdUsuario.Value = SessionFixed.IdUsuario;
                report.p_fecha_ini.Value = model.fecha_ini;
                report.p_fecha_fin.Value = model.fecha_fin;
                report.p_mostrar_detallado.Value = model.mostrar_detallado;
                report.p_no_mostrar_valores_en_0.Value = model.no_mostrar_valores_en_0;
                cargar_combos(model);

                report.usuario = SessionFixed.IdUsuario;
                report.empresa = SessionFixed.NomEmpresa;
                report.RequestParameters = false;
                ViewBag.Report = report;
            }
            return View(model);
        }

        public ActionResult INV_007(int IdSucursalOrigen = 0, int IdBodegaOrigen = 0, decimal IdTransferencia = 0)
        {
            INV_007_Rpt model = new INV_007_Rpt();
            model.p_IdEmpresa.Value = Convert.ToInt32(Session["IdEmpresa"]);
            model.p_IdSucursalOrigen.Value = IdSucursalOrigen;
            model.p_IdBodegaOrigen.Value = IdBodegaOrigen;
            model.p_IdTransferencia.Value = IdTransferencia;
            model.usuario = Session["IdUsuario"].ToString();
            model.empresa = Session["nom_empresa"].ToString();
            if (IdTransferencia == 0)
                model.RequestParameters = false;
            return View(model);
        }

        public ActionResult INV_008()
        {
            cl_filtros_inventario_Info model = new cl_filtros_inventario_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            List_decimal.set_list(new List<in_Producto_Info>());
            cargar_combos(model);
            INV_008_Rpt report = new INV_008_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdBodega.Value = model.IdBodega;
            report.p_mostrar_saldos_en_0.Value = model.mostrar_saldos_en_0;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }

        [HttpPost]
        public ActionResult INV_008(cl_filtros_inventario_Info model)
        {
            INV_008_Rpt report = new INV_008_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdBodega.Value = model.IdBodega;
            report.p_mostrar_saldos_en_0.Value = model.mostrar_saldos_en_0;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            report.lst_producto = List_decimal.get_list();
            cargar_combos(model);
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult INV_009()
        {
            cl_filtros_inventario_Info model = new cl_filtros_inventario_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            List_decimal.set_list(new List<in_Producto_Info>());
            cargar_combos(model);
            INV_009_Rpt report = new INV_009_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdBodega.Value = model.IdBodega;
            report.p_IdMarca.Value = model.IdMarca;
            report.p_IdProductoPadre.Value = model.IdProductoPadre == null ? 0 : Convert.ToDecimal(model.IdProductoPadre);
            report.p_fechaCorte.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }

        [HttpPost]
        public ActionResult INV_009(cl_filtros_inventario_Info model)
        {
            INV_009_Rpt report = new INV_009_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdBodega.Value = model.IdBodega;
            report.p_IdMarca.Value = model.IdMarca;
            report.p_IdProductoPadre.Value = model.IdProductoPadre == null ? 0 : Convert.ToDecimal(model.IdProductoPadre);
            report.p_fechaCorte.Value = model.fecha_fin;
            cargar_combos(model);
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult INV_010()
        {

            cl_filtros_inventario_Info model = new cl_filtros_inventario_Info {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdCategoria = "",
                fecha_ini = new DateTime(DateTime.Now.Year,1,1),
                fecha_fin = new DateTime(DateTime.Now.Year,12,31)
            };

            cargar_combos(model);
            INV_010_Rpt report = new INV_010_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdMarca.Value = model.IdMarca;
            report.p_IdUsuario.Value = SessionFixed.IdUsuario;
            report.p_IdProducto.Value = model.IdProductoPadre;
            report.p_IdCategoria.Value = model.IdCategoria;
            report.p_IdLinea.Value = model.IdLinea;
            report.p_IdGrupo.Value = model.IdGrupo;
            report.p_IdSubGrupo.Value = model.IdSubGrupo;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.p_mostrarSinMovimiento.Value = model.mostrarSinMovimiento;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult INV_010(cl_filtros_inventario_Info model)
        {
            INV_010_Rpt report = new INV_010_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdMarca.Value = model.IdMarca;
            report.p_IdUsuario.Value = SessionFixed.IdUsuario;
            report.p_IdProducto.Value = model.IdProductoPadre;
            report.p_IdCategoria.Value = model.IdCategoria;
            report.p_IdLinea.Value = model.IdLinea;
            report.p_IdGrupo.Value = model.IdGrupo;
            report.p_IdSubGrupo.Value = model.IdSubGrupo;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.p_mostrarSinMovimiento.Value = model.mostrarSinMovimiento;
            cargar_combos(model);

            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();

            ViewBag.Report = report;
            return View(model);
        }


        public ActionResult INV_011(int IdSucursal = 0, int IdMovi_inven_tipo = 0, decimal IdNumMovi = 0)
        {
            INV_011_Rpt model = new INV_011_Rpt();
            model.p_IdEmpresa.Value = Convert.ToInt32(Session["IdEmpresa"]);
            model.p_IdSucursal.Value = IdSucursal;
            model.p_IdMovi_inven_tipo.Value = IdMovi_inven_tipo;
            model.p_IdNumMovi.Value = IdNumMovi;
            model.usuario = Session["IdUsuario"].ToString();
            model.empresa = Session["nom_empresa"].ToString();
            if (IdNumMovi == 0)
                model.RequestParameters = false;
            return View(model);
        }

        public ActionResult INV_012()
        {

            cl_filtros_inventario_Info model = new cl_filtros_inventario_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdMarca = 0,
                IdProducto = 0,
                dIAS = 40
            };

            cargar_combos(model);
            INV_012_Rpt report = new INV_012_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdBodega.Value = model.IdBodega;
            report.p_IdProducto.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
            report.p_IdMarca.Value = model.IdMarca;
            report.p_fechaIni.Value = model.fecha_fin;
            report.p_dIAS.Value = model.dIAS;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();

            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult INV_012(cl_filtros_inventario_Info model)
        {
            INV_012_Rpt report = new INV_012_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdBodega.Value = model.IdBodega;
            report.p_IdProducto.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;
            report.p_IdMarca.Value = model.IdMarca;
            report.p_fechaIni.Value = model.fecha_fin;
            report.p_dIAS.Value = model.dIAS;
            cargar_combos(model);

            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();

            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult INV_013()
        {
            cl_filtros_inventario_Info model = new cl_filtros_inventario_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdProducto = 0,
            };

            INV_013_Rpt report = new INV_013_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdProducto.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;

            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult INV_013(cl_filtros_inventario_Info model)
        {
            INV_013_Rpt report = new INV_013_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdProducto.Value = model.IdProductoPadre == null ? 0 : model.IdProductoPadre;

            ViewBag.Report = report;
            return View(model);
        }

    }

}