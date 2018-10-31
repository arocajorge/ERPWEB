﻿using Core.Erp.Info.Helps;
using Core.Erp.Info.Inventario;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_Producto_Data
    {
        public List<in_Producto_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<in_Producto_Info> Lista;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados)
                        Lista = (from t in Context.in_ProductoTipo
                                 join p in Context.in_Producto
                                 on new { t.IdEmpresa, t.IdProductoTipo } equals new { p.IdEmpresa, p.IdProductoTipo }
                                 join c in Context.in_categorias
                                 on new { p.IdEmpresa, p.IdCategoria } equals new { c.IdEmpresa, c.IdCategoria }
                                 join m in Context.in_Marca
                                 on new { p.IdEmpresa, p.IdMarca } equals new { m.IdEmpresa, m.IdMarca }
                                 join pr in Context.in_presentacion
                                 on new { p.IdEmpresa, p.IdPresentacion } equals new { pr.IdEmpresa, pr.IdPresentacion }

                                 where
                                 t.IdEmpresa == IdEmpresa
                                 && p.IdEmpresa == IdEmpresa
                                 && c.IdEmpresa == IdEmpresa
                                 && m.IdEmpresa == IdEmpresa
                                 && pr.IdEmpresa == IdEmpresa

                                 select new in_Producto_Info
                                 {
                                     IdEmpresa = p.IdEmpresa,
                                     IdProducto = p.IdProducto,
                                     pr_codigo = p.pr_codigo,
                                     pr_descripcion = p.pr_descripcion,
                                     Estado = p.Estado,
                                     lote_fecha_vcto = p.lote_fecha_vcto,
                                     lote_num_lote = p.lote_num_lote,

                                     tp_descripcion = t.tp_descripcion,
                                     nom_presentacion = pr.nom_presentacion,
                                     ma_descripcion = m.Descripcion,
                                     nom_categoria = c.ca_Categoria,
                                     pr_imagen = p.pr_imagen,

                                     EstadoBool = p.Estado == "A" ? true : false


                                 }).ToList();
                    else
                        Lista = (from t in Context.in_ProductoTipo
                                 join p in Context.in_Producto
                                 on new { t.IdEmpresa, t.IdProductoTipo } equals new { p.IdEmpresa, p.IdProductoTipo }
                                 join c in Context.in_categorias
                                 on new { p.IdEmpresa, p.IdCategoria } equals new { c.IdEmpresa, c.IdCategoria }
                                 join m in Context.in_Marca
                                 on new { p.IdEmpresa, p.IdMarca } equals new { m.IdEmpresa, m.IdMarca }
                                 join pr in Context.in_presentacion
                                 on new { p.IdEmpresa, p.IdPresentacion } equals new { pr.IdEmpresa, pr.IdPresentacion }

                                 where
                                 t.IdEmpresa == IdEmpresa
                                 && p.IdEmpresa == IdEmpresa
                                 && c.IdEmpresa == IdEmpresa
                                 && m.IdEmpresa == IdEmpresa
                                 && pr.IdEmpresa == IdEmpresa
                                  && p.Estado == "A"
                                 select new in_Producto_Info
                                 {
                                     IdEmpresa = p.IdEmpresa,
                                     IdProducto = p.IdProducto,
                                     pr_codigo = p.pr_codigo,
                                     pr_descripcion = p.pr_descripcion,
                                     Estado = p.Estado,
                                     lote_fecha_vcto = p.lote_fecha_vcto,
                                     lote_num_lote = p.lote_num_lote,

                                     tp_descripcion = t.tp_descripcion,
                                     nom_presentacion = pr.nom_presentacion,
                                     ma_descripcion = m.Descripcion,
                                     nom_categoria = c.ca_Categoria,
                                     pr_imagen = p.pr_imagen,

                                     EstadoBool = p.Estado == "A" ? true : false


                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<in_Producto_Info> get_list_para_composicion(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<in_Producto_Info> Lista;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.in_Producto
                                 join t in Context.in_ProductoTipo
                                 on new { q.IdEmpresa, q.IdProductoTipo} equals new { t.IdEmpresa,t.IdProductoTipo}
                                 where q.IdEmpresa == IdEmpresa
                                 && t.tp_ManejaInven == "S"
                                 select new in_Producto_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdProducto = q.IdProducto,
                                     pr_codigo = q.pr_codigo,
                                     pr_descripcion = q.pr_descripcion,
                                     Estado = q.Estado,
                                     lote_fecha_vcto = q.lote_fecha_vcto,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.in_Producto
                                 join t in Context.in_ProductoTipo
                                 on new { q.IdEmpresa, q.IdProductoTipo } equals new { t.IdEmpresa, t.IdProductoTipo }
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 && t.tp_ManejaInven == "S"
                                 select new in_Producto_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdProducto = q.IdProducto,
                                     pr_codigo = q.pr_codigo,
                                     pr_descripcion = q.pr_descripcion,
                                     Estado = q.Estado,
                                     lote_fecha_vcto = q.lote_fecha_vcto,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<in_Producto_Info> get_list_padres(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<in_Producto_Info> Lista;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.in_Producto
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdProducto_padre == null
                                 select new in_Producto_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdProducto = q.IdProducto,
                                     pr_codigo = q.pr_codigo,
                                     pr_descripcion = q.pr_descripcion,
                                     Estado = q.Estado,
                                     lote_fecha_vcto = q.lote_fecha_vcto
                                 }).ToList();
                    else
                        Lista = (from q in Context.in_Producto
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdProducto_padre == null
                                 && q.Estado == "A"
                                 select new in_Producto_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdProducto = q.IdProducto,
                                     pr_codigo = q.pr_codigo,
                                     pr_descripcion = q.pr_descripcion,
                                     Estado = q.Estado,
                                     lote_fecha_vcto = q.lote_fecha_vcto
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<in_Producto_Info> get_list_combo_hijo(int IdEmpresa, decimal IdProducto_padre)
        {
            try
            {
                List<in_Producto_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.vwin_producto_hijo_combo
                             where q.IdEmpresa == IdEmpresa
                             && q.lote_num_lote!= "LOTE0"
                             && q.IdProducto_padre==IdProducto_padre
                             select new in_Producto_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdProducto = q.IdProducto,
                                 pr_descripcion = q.pr_descripcion,
                                 nom_presentacion = q.nom_presentacion,
                                 nom_categoria = q.ca_Categoria,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
                                 lote_num_lote = q.lote_num_lote,
                                 IdUnidadMedida=q.IdUnidadMedida
                             }).ToList();
                }
                Lista = get_list_nombre_combo(Lista);
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<in_Producto_Info> get_list_stock_lotes(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdProducto_padre)
        {
            try
            {
                List<in_Producto_Info> Lista;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.vwin_producto_x_tb_bodega_stock_x_lote
                             where q.IdEmpresa == IdEmpresa && q.IdProducto_padre == IdProducto_padre
                             && q.stock > 0
                             orderby q.lote_fecha_vcto ascending
                             select new in_Producto_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdProducto = q.IdProducto,
                                 pr_codigo = q.pr_codigo,
                                 pr_descripcion = q.pr_descripcion,
                                 IdProducto_padre = q.IdProducto_padre,
                                 lote_fecha_fab = q.lote_fecha_fab,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
                                 lote_num_lote = q.lote_num_lote,
                                 stock = q.stock
                             }).ToList();
                }
                Lista = get_list_nombre_combo(Lista);
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public in_Producto_Info get_info(int IdEmpresa, decimal IdProducto)
        {
            try
            {
                in_Producto_Info info = new in_Producto_Info();

                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Producto Entity = Context.in_Producto.Include("in_presentacion").FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdProducto == IdProducto);
                    if (Entity == null) return null;
                    info = new in_Producto_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdProducto = Entity.IdProducto,
                        pr_codigo = Entity.pr_codigo,
                        pr_codigo2 = Entity.pr_codigo2,
                        pr_descripcion = Entity.pr_descripcion,
                        pr_descripcion_2 = Entity.pr_descripcion_2,
                        IdProductoTipo = Entity.IdProductoTipo,
                        IdMarca = Entity.IdMarca,
                        IdPresentacion = Entity.IdPresentacion,
                        IdCategoria = Entity.IdCategoria,
                        IdLinea = Entity.IdLinea,
                        IdGrupo = Entity.IdGrupo,
                        IdSubGrupo = Entity.IdSubGrupo,
                        IdUnidadMedida = Entity.IdUnidadMedida,
                        IdUnidadMedida_Consumo = Entity.IdUnidadMedida_Consumo,
                        pr_codigo_barra = Entity.pr_codigo_barra,
                        pr_observacion = Entity.pr_observacion,
                        Estado = Entity.Estado,
                        IdCod_Impuesto_Iva = Entity.IdCod_Impuesto_Iva,
                        Aparece_modu_Ventas = Entity.Aparece_modu_Ventas,
                        Aparece_modu_Compras = Entity.Aparece_modu_Compras,
                        Aparece_modu_Inventario = Entity.Aparece_modu_Inventario,
                        Aparece_modu_Activo_F = Entity.Aparece_modu_Activo_F,
                        IdProducto_padre = Entity.IdProducto_padre,
                        lote_fecha_fab = Entity.lote_fecha_fab,
                        lote_fecha_vcto = Entity.lote_fecha_vcto,
                        lote_num_lote = Entity.lote_num_lote,
                        precio_1 = Entity.precio_1 == null ? 0 : Convert.ToDouble(Entity.precio_1),
                        precio_2 = Entity.precio_2 == null ? 0 : Convert.ToDouble(Entity.precio_2),
                        signo_2 = Entity.signo_2,
                        porcentaje_2 = Entity.porcentaje_2 == null ? 0 : Convert.ToDouble(Entity.porcentaje_2),
                        precio_3 = Entity.precio_3 == null ? 0 : Convert.ToDouble(Entity.precio_3),
                        signo_3 = Entity.signo_3,
                        porcentaje_3 = Entity.porcentaje_3 == null ? 0 : Convert.ToDouble(Entity.porcentaje_3),
                        precio_4 = Entity.precio_4 == null ? 0 : Convert.ToDouble(Entity.precio_4),
                        signo_4 = Entity.signo_4,
                        porcentaje_4 = Entity.porcentaje_4 == null ? 0 : Convert.ToDouble(Entity.porcentaje_4),
                        precio_5 = Entity.precio_5 == null ? 0 : Convert.ToDouble(Entity.precio_5),
                        signo_5 = Entity.signo_5,
                        porcentaje_5 = Entity.porcentaje_5 == null ? 0 : Convert.ToDouble(Entity.porcentaje_5),
                        se_distribuye = Entity.se_distribuye == null ? false : Convert.ToBoolean(Entity.se_distribuye),
                        pr_imagen=Entity.pr_imagen
                    };
                    info.pr_descripcion_combo = info.pr_descripcion + " " + Entity.in_presentacion.nom_presentacion + " - " + info.lote_num_lote + " - " + (info.lote_fecha_vcto != null ? Convert.ToDateTime(info.lote_fecha_vcto).ToString("dd/MM/yyyy") : "");
                }
                
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_Producto
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdProducto) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_Producto_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Context.in_Producto.Add(new in_Producto
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdProducto = info.IdProducto = get_id(info.IdEmpresa),
                        pr_codigo = string.IsNullOrEmpty(info.pr_codigo) ? ("PROD" + info.IdProducto.ToString("0000000")) : info.pr_codigo,
                        pr_codigo2 = info.pr_codigo2,
                        pr_descripcion = info.pr_descripcion,
                        pr_descripcion_2 = info.pr_descripcion_2,
                        IdProductoTipo = info.IdProductoTipo,
                        IdMarca = info.IdMarca,
                        IdPresentacion = info.IdPresentacion,
                        IdCategoria = info.IdCategoria,
                        IdLinea = info.IdLinea,
                        IdGrupo = info.IdGrupo,
                        IdSubGrupo = info.IdSubGrupo,
                        IdUnidadMedida = info.IdUnidadMedida,
                        IdUnidadMedida_Consumo = info.IdUnidadMedida_Consumo,
                        pr_codigo_barra = info.pr_codigo_barra,
                        pr_observacion = info.pr_observacion,
                        Estado = info.Estado = "A",
                        IdCod_Impuesto_Iva = info.IdCod_Impuesto_Iva,
                        Aparece_modu_Ventas = info.Aparece_modu_Ventas,
                        Aparece_modu_Compras = info.Aparece_modu_Compras,
                        Aparece_modu_Inventario = info.Aparece_modu_Inventario,
                        Aparece_modu_Activo_F = info.Aparece_modu_Activo_F,
                        IdProducto_padre = info.IdProducto_padre,
                        lote_fecha_fab = info.lote_fecha_fab,
                        lote_fecha_vcto = info.lote_fecha_vcto,
                        lote_num_lote = info.lote_num_lote,
                        precio_1 = info.precio_1,
                        precio_2 = info.precio_2,
                        signo_2 = info.signo_2,
                        porcentaje_2 = info.porcentaje_2,
                        precio_3 = info.precio_3,
                        signo_3 = info.signo_3,
                        porcentaje_3 = info.porcentaje_3,
                        precio_4 = info.precio_4,
                        signo_4 = info.signo_4,
                        porcentaje_4 = info.porcentaje_4,
                        precio_5 = info.precio_5,
                        signo_5 = info.signo_5,
                        porcentaje_5 = info.porcentaje_5,
                        se_distribuye = info.se_distribuye,
                        pr_imagen = info.pr_imagen,
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    });
                    if (info.lst_producto_x_bodega != null)
                    {
                        foreach (var item in info.lst_producto_x_bodega)
                        {
                            Context.in_producto_x_tb_bodega.Add(new in_producto_x_tb_bodega
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdProducto = info.IdProducto,
                                IdSucursal = item.IdSucursal,
                                IdBodega = item.IdBodega,
                                Stock_minimo = item.Stock_minimo
                            });

                        }
                    }
                    // composision
                    if (info.lst_producto_composicion != null)
                    {
                        foreach (var item in info.lst_producto_composicion)
                        {
                            Context.in_Producto_Composicion.Add(new in_Producto_Composicion
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdProductoPadre = info.IdProducto,
                                IdProductoHijo=item.IdProductoHijo,
                                Cantidad=item.Cantidad,
                                IdUnidadMedida = item.IdUnidadMedida

                            });

                        }
                    }

                    var parametros = Context.in_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                    if (parametros.P_se_crea_lote_0_al_crear_producto_matriz == true)
                    {
                        var tipo = Context.in_ProductoTipo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdProductoTipo == info.IdProductoTipo).FirstOrDefault();
                        if (tipo == null)
                            return false;
                        if (tipo.tp_ManejaLote)
                        {
                            Context.in_Producto.Add(new in_Producto
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdProducto = info.IdProducto+1,
                                pr_codigo = string.IsNullOrEmpty(info.pr_codigo) ? ("PROD" + info.IdProducto.ToString("0000000")) : info.pr_codigo,
                                pr_codigo2 = info.pr_codigo2,
                                pr_descripcion = info.pr_descripcion,
                                pr_descripcion_2 = info.pr_descripcion_2,
                                IdProductoTipo = (int)parametros.P_IdProductoTipo_para_lote_0,
                                IdMarca = info.IdMarca,
                                IdPresentacion = info.IdPresentacion,
                                IdCategoria = info.IdCategoria,
                                IdLinea = info.IdLinea,
                                IdGrupo = info.IdGrupo,
                                IdSubGrupo = info.IdSubGrupo,
                                IdUnidadMedida = info.IdUnidadMedida,
                                IdUnidadMedida_Consumo = info.IdUnidadMedida_Consumo,
                                pr_codigo_barra = info.pr_codigo_barra,
                                pr_observacion = info.pr_observacion,
                                Estado = info.Estado = "A",
                                IdCod_Impuesto_Iva = info.IdCod_Impuesto_Iva,
                                Aparece_modu_Ventas = true,
                                Aparece_modu_Compras = true,
                                Aparece_modu_Inventario = true,
                                Aparece_modu_Activo_F = false,
                                IdProducto_padre = info.IdProducto,
                                lote_fecha_fab = null,
                                lote_fecha_vcto = DateTime.Now.AddYears(100),
                                lote_num_lote = "LOTE0",
                                precio_1 = info.precio_1,
                                precio_2 = info.precio_2,
                                signo_2 = info.signo_2,
                                porcentaje_2 = info.porcentaje_2,
                                precio_3 = info.precio_3,
                                signo_3 = info.signo_3,
                                porcentaje_3 = info.porcentaje_3,
                                precio_4 = info.precio_4,
                                signo_4 = info.signo_4,
                                porcentaje_4 = info.porcentaje_4,
                                precio_5 = info.precio_5,
                                signo_5 = info.signo_5,
                                porcentaje_5 = info.porcentaje_5,
                                se_distribuye = true,
                                pr_imagen = null,
                                IdUsuario = info.IdUsuario,
                                Fecha_Transac = DateTime.Now
                            });
                            if (info.lst_producto_x_bodega != null)
                            {
                                foreach (var item in info.lst_producto_x_bodega)
                                {
                                    Context.in_producto_x_tb_bodega.Add(new in_producto_x_tb_bodega
                                    {
                                        IdEmpresa = info.IdEmpresa,
                                        IdProducto = info.IdProducto + 1,
                                        IdSucursal = item.IdSucursal,
                                        IdBodega = item.IdBodega,
                                        Stock_minimo = item.Stock_minimo

                                    });

                                }
                            }
                            // composision
                            if (info.lst_producto_composicion != null)
                            {
                                foreach (var item in info.lst_producto_composicion)
                                {
                                    Context.in_Producto_Composicion.Add(new in_Producto_Composicion
                                    {
                                        IdEmpresa = info.IdEmpresa,
                                        IdProductoPadre = info.IdProducto,
                                        IdProductoHijo = item.IdProductoHijo,
                                        Cantidad = item.Cantidad,
                                        IdUnidadMedida=item.IdUnidadMedida

                                    });

                                }
                            }

                        }
                    }
                    
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool modificarDB(in_Producto_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Producto Entity = Context.in_Producto.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdProducto == info.IdProducto);
                    if (Entity == null) return false;
                    Entity.pr_codigo = string.IsNullOrEmpty(info.pr_codigo) ? ("PROD" + info.IdProducto.ToString("0000000")) : info.pr_codigo;
                    Entity.pr_codigo2 = info.pr_codigo2;
                    Entity.pr_descripcion = info.pr_descripcion;
                    Entity.pr_descripcion_2 = info.pr_descripcion_2;
                    Entity.IdProductoTipo = info.IdProductoTipo;
                    Entity.IdMarca = info.IdMarca;
                    Entity.IdPresentacion = info.IdPresentacion;
                    Entity.IdCategoria = info.IdCategoria;
                    Entity.IdLinea = info.IdLinea;
                    Entity.IdGrupo = info.IdGrupo;
                    Entity.IdSubGrupo = info.IdSubGrupo;
                    Entity.IdUnidadMedida = info.IdUnidadMedida;
                    Entity.IdUnidadMedida_Consumo = info.IdUnidadMedida_Consumo;
                    Entity.pr_codigo_barra = info.pr_codigo_barra;
                    Entity.pr_observacion = info.pr_observacion;
                    Entity.IdCod_Impuesto_Iva = info.IdCod_Impuesto_Iva;
                    Entity.Aparece_modu_Ventas = info.Aparece_modu_Ventas;
                    Entity.Aparece_modu_Compras = info.Aparece_modu_Compras;
                    Entity.Aparece_modu_Inventario = info.Aparece_modu_Inventario;
                    Entity.Aparece_modu_Activo_F = info.Aparece_modu_Activo_F;
                    Entity.IdProducto_padre = info.IdProducto_padre;
                    Entity.lote_fecha_fab = info.lote_fecha_fab;
                    Entity.lote_fecha_vcto = info.lote_fecha_vcto;
                    Entity.lote_num_lote = info.lote_num_lote;
                    Entity.precio_1 = info.precio_1;
                    Entity.precio_2 = info.precio_2;
                    Entity.signo_2 = info.signo_2;
                    Entity.porcentaje_2 = info.porcentaje_2;
                    Entity.precio_3 = info.precio_3;
                    Entity.signo_3 = info.signo_3;
                    Entity.porcentaje_3 = info.porcentaje_3;
                    Entity.precio_4 = info.precio_4;
                    Entity.signo_4 = info.signo_4;
                    Entity.porcentaje_4 = info.porcentaje_4;
                    Entity.precio_5 = info.precio_5;
                    Entity.signo_5 = info.signo_5;
                    Entity.porcentaje_5 = info.porcentaje_5;
                    Entity.se_distribuye = info.se_distribuye;
                    Entity.pr_imagen = info.pr_imagen;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
                    string SQL = "UPDATE in_Producto SET pr_descripcion = '" + info.pr_descripcion +
                        "', pr_descripcion_2 = '" + info.pr_descripcion_2 +
                        "', precio_1 = " + info.precio_1 +
                        ", precio_2 = " + info.precio_2 +
                        ", precio_3 = " + info.precio_3 +
                        ", precio_4 = " + info.precio_4 +
                        ", precio_5 = " + info.precio_5 +
                        ", signo_2 = '" + info.signo_2 +
                        "', signo_3 = '" + info.signo_3 +
                        "', signo_4 = '" + info.signo_4 +
                        "', signo_5 = '" + info.signo_5 +
                        "', porcentaje_2 = " + info.porcentaje_2 +
                        ", porcentaje_3 = " + info.porcentaje_3 +
                        ", porcentaje_4 = " + info.porcentaje_4 +
                        ", porcentaje_5 = " + info.porcentaje_5 +
                        ", IdCod_Impuesto_Iva = '" + info.IdCod_Impuesto_Iva +
                        "', pr_codigo = '" + info.pr_codigo +
                        "', pr_codigo2 = '" + info.pr_codigo2 +
                        "', IdPresentacion = '" + info.IdPresentacion +
                        "', IdMarca = " + info.IdMarca +
                        ", IdUnidadMedida = '" + info.IdUnidadMedida +
                        "', IdUnidadMedida_Consumo = '" + info.IdUnidadMedida_Consumo +
                        "', IdCategoria = '" + info.IdCategoria +
                        "', IdLinea = " + info.IdLinea +
                        ", IdGrupo = " + info.IdGrupo +
                        ", IdSubGrupo = " + info.IdSubGrupo +
                        ", pr_observacion = '" + info.pr_observacion +
                        "', pr_codigo_barra = '" + info.pr_codigo_barra +
                        "' where in_Producto.IdEmpresa = " + info.IdEmpresa + " AND in_Producto.IdProducto_padre = " + info.IdProducto;
                    int row = Context.Database.ExecuteSqlCommand(SQL);
                    
                    foreach (var item in info.lst_producto_x_bodega)
                    {
                        var prod_x_bos = Context.in_producto_x_tb_bodega.Where(v => v.IdEmpresa == info.IdEmpresa && v.IdSucursal == item.IdSucursal && v.IdBodega == item.IdBodega && v.IdProducto == info.IdProducto).FirstOrDefault();
                        if (prod_x_bos == null)
                        {
                            Context.in_producto_x_tb_bodega.Add(new in_producto_x_tb_bodega
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdProducto = info.IdProducto,
                                IdSucursal = item.IdSucursal,
                                IdBodega = item.IdBodega,
                                Stock_minimo = item.Stock_minimo
                            });
                        }else
                        {
                            prod_x_bos.Stock_minimo = item.Stock_minimo;
                        }
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool anularDB(in_Producto_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Producto Entity = Context.in_Producto.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdProducto == info.IdProducto);
                    if (Entity == null) return false;
                    Entity.Estado = "I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
                    Entity.pr_motivoAnulacion = info.pr_motivoAnulacion;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<in_Producto_Info> get_list_nombre_combo(List<in_Producto_Info> Lista)
        {
            int OrdenVcto = 1;
            Lista.ForEach(V => {
                V.pr_descripcion = V.pr_descripcion + " " + V.nom_presentacion + " - " + V.lote_num_lote + " - " + (V.lote_fecha_vcto != null ? Convert.ToDateTime(V.lote_fecha_vcto).ToString("dd/MM/yyyy") : " - "+ V.ca_descripcion);
                V.pr_descripcion_combo = V.pr_descripcion;
                V.OrdenVcto = OrdenVcto++;
            });
            return Lista;
        }

        #region metodo baja demanda

        public List<in_Producto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, cl_enumeradores.eTipoBusquedaProducto Busqueda, cl_enumeradores.eModulo Modulo, decimal IdProductoPadre)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<in_Producto_Info> Lista = new List<in_Producto_Info>();
            switch (Busqueda)
            {
                case cl_enumeradores.eTipoBusquedaProducto.SOLOPADRES:
                    Lista = get_list(IdEmpresa, skip, take, args.Filter, 0);
                    break;
                case cl_enumeradores.eTipoBusquedaProducto.SOLOHIJOS:
                    Lista = get_list(IdEmpresa, skip, take, args.Filter,IdProductoPadre);
                    break;
                case cl_enumeradores.eTipoBusquedaProducto.PORMODULO:
                    Lista = get_list(Modulo, IdEmpresa, skip, take, args.Filter);
                    break;
                case cl_enumeradores.eTipoBusquedaProducto.TODOS:
                    Lista = get_list(IdEmpresa, skip, take, args.Filter);
                    break;
                case cl_enumeradores.eTipoBusquedaProducto.TODOS_MENOS_PADRES:
                    Lista = get_list_todos_menos_padres(IdEmpresa, skip, take, args.Filter);

                    
                    break;
            }
            
            
            return Lista;
        }

        public in_Producto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
             return get_info_demanda(IdEmpresa, Convert.ToDecimal(args.Value));
        }

        public in_Producto_Info get_info_demanda(int IdEmpresa, decimal IdProducto)
        {
            in_Producto_Info info = new in_Producto_Info();

            using (Entities_inventario Contex = new Entities_inventario())
            {
                info = (from q in Contex.in_Producto
                                     join p in Contex.in_presentacion
                                     on new { q.IdEmpresa, q.IdPresentacion} equals new { p.IdEmpresa, p.IdPresentacion}
                                     where q.IdEmpresa == IdEmpresa && q.IdProducto == IdProducto
                                     select new in_Producto_Info
                                     {
                                         IdProducto = q.IdProducto,
                                         pr_descripcion = q.pr_descripcion,
                                         pr_descripcion_2 = q.pr_descripcion_2,
                                         pr_codigo = q.pr_codigo,
                                         lote_num_lote = q.lote_num_lote,
                                         lote_fecha_vcto = q.lote_fecha_vcto,
                                         nom_presentacion = p.nom_presentacion
                                     }).FirstOrDefault();
             
            }
            if (info != null)
            {
                info.pr_descripcion = info.pr_descripcion + " " + info.nom_presentacion + " - " + info.lote_num_lote + " - " + (info.lote_fecha_vcto != null ? Convert.ToDateTime(info.lote_fecha_vcto).ToString("dd/MM/yyyy") : "");
                info.pr_descripcion_combo = info.pr_descripcion + " " + info.nom_presentacion + " - " + info.lote_num_lote + " - " + (info.lote_fecha_vcto != null ? Convert.ToDateTime(info.lote_fecha_vcto).ToString("dd/MM/yyyy") : "");
            }
            else
                info = new in_Producto_Info();

            return info;
        }

        public List<in_Producto_Info> get_list(int IdEmpresa, int skip, int take, string filter)
        {
            try
            {
                List<in_Producto_Info> Lista = new List<in_Producto_Info>();

                Entities_inventario Context = new Entities_inventario();

                var lst = (from 
                          p in Context.in_Producto
                         join c in Context.in_categorias
                         on new { p.IdEmpresa, p.IdCategoria } equals new { c.IdEmpresa, c.IdCategoria }
                         join pr in Context.in_presentacion
                         on new { p.IdEmpresa, p.IdPresentacion } equals new { pr.IdEmpresa, pr.IdPresentacion }

                         where
                          p.IdEmpresa == IdEmpresa
                          && c.IdEmpresa == IdEmpresa
                          && pr.IdEmpresa == IdEmpresa
                          && p.Estado=="A"
                          &&(p.IdProducto.ToString() + " " + p.pr_descripcion + " " + p.lote_num_lote).Contains(filter)
                         select new {
                             p.IdEmpresa,
                             p.IdProducto,
                             p.pr_descripcion,
                             p.pr_descripcion_2,
                             p.pr_codigo,
                             p.lote_num_lote,
                             p.lote_fecha_vcto,
                             c.ca_Categoria,
                             pr.nom_presentacion
                         })
                             .OrderBy(p => p.IdProducto)
                             .Skip(skip)
                             .Take(take)
                             .ToList();


                foreach (var q in lst)
                {
                    Lista.Add(new in_Producto_Info
                    {
                        IdEmpresa=q.IdEmpresa,
                        IdProducto = q.IdProducto,
                        pr_descripcion = q.pr_descripcion,
                        pr_descripcion_2 = q.pr_descripcion_2,
                        pr_codigo = q.pr_codigo,
                        lote_num_lote = q.lote_num_lote,
                        lote_fecha_vcto = q.lote_fecha_vcto,
                        nom_categoria=q.ca_Categoria,
                        nom_presentacion=q.nom_presentacion
                    });
                }

                Context.Dispose();
                Lista = get_list_nombre_combo(Lista);
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<in_Producto_Info> get_list(int IdEmpresa, int skip, int take, string filter, decimal IdProductoPadre)
        {
            try
            {
                List<in_Producto_Info> Lista;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (IdProductoPadre != 0)
                    {
                        Lista = (from p in Context.vwin_producto_hijo_combo
                                 where p.IdEmpresa == IdEmpresa
                                 && p.IdProducto_padre == IdProductoPadre
                                 && (p.IdProducto.ToString() + " " + p.pr_descripcion + " " + p.lote_num_lote).Contains(filter)
                                 select new in_Producto_Info
                                 {
                                     IdEmpresa = p.IdEmpresa,
                                     IdProducto = p.IdProducto,
                                     pr_descripcion = p.pr_descripcion,
                                     lote_num_lote = p.lote_num_lote,
                                     lote_fecha_vcto = p.lote_fecha_vcto,
                                     nom_categoria = p.ca_Categoria,
                                     nom_presentacion = p.nom_presentacion
                                 })
                                    .OrderBy(p => p.IdProducto)
                                    .Skip(skip)
                                    .Take(take)
                                    .ToList();
                    }
                    else
                        Lista = (from p in Context.vwin_producto_padre_combo
                                 where p.IdEmpresa == IdEmpresa
                                 && (p.IdProducto.ToString() + " " + p.pr_descripcion ).Contains(filter)
                                 select new in_Producto_Info
                                 {
                                     IdEmpresa = p.IdEmpresa,
                                     IdProducto = p.IdProducto,
                                     pr_descripcion = p.pr_descripcion,
                                     nom_categoria = p.ca_Categoria,
                                     nom_presentacion = p.nom_presentacion
                                 })
                                 .OrderBy(p => p.IdProducto)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToList();
                }
                Lista = get_list_nombre_combo(Lista);
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<in_Producto_Info> get_list(cl_enumeradores.eModulo Modulo, int IdEmpresa, int skip, int take, string filter)
        {
            try
            {
                List<in_Producto_Info> Lista = new List<in_Producto_Info>();

                using (Entities_inventario Context = new Entities_inventario())
                {
                    switch (Modulo)
                    {
                        case cl_enumeradores.eModulo.INV:
                            Lista = (from p in Context.in_Producto
                                     join c in Context.in_categorias
                                     on new { p.IdEmpresa, p.IdCategoria } equals new { c.IdEmpresa, c.IdCategoria }
                                     join pr in Context.in_presentacion
                                     on new { p.IdEmpresa, p.IdPresentacion } equals new { pr.IdEmpresa, pr.IdPresentacion }
                                     where
                                      p.IdEmpresa == IdEmpresa
                                      && c.IdEmpresa == IdEmpresa
                                      && pr.IdEmpresa == IdEmpresa
                                      && p.Estado == "A"
                                      && (p.IdProducto.ToString() + " " + p.pr_descripcion + " " +p.lote_num_lote).Contains(filter)
                                      && p.Aparece_modu_Inventario == true
                                     select new in_Producto_Info
                                     {
                                         IdEmpresa = p.IdEmpresa,
                                         IdProducto = p.IdProducto,
                                         pr_descripcion = p.pr_descripcion,
                                         lote_num_lote = p.lote_num_lote,
                                         lote_fecha_vcto = p.lote_fecha_vcto,
                                         nom_categoria = c.ca_Categoria,
                                         nom_presentacion = pr.nom_presentacion
                                     })
                                    .OrderBy(p => p.IdProducto)
                                    .Skip(skip)
                                    .Take(take)
                                    .ToList();
                            break;
                        case cl_enumeradores.eModulo.FAC:
                            Lista = (from p in Context.in_Producto
                                     join c in Context.in_categorias
                                     on new { p.IdEmpresa, p.IdCategoria } equals new { c.IdEmpresa, c.IdCategoria }
                                     join pr in Context.in_presentacion
                                     on new { p.IdEmpresa, p.IdPresentacion } equals new { pr.IdEmpresa, pr.IdPresentacion }
                                     where
                                      p.IdEmpresa == IdEmpresa
                                      && c.IdEmpresa == IdEmpresa
                                      && pr.IdEmpresa == IdEmpresa
                                      && p.Estado == "A"
                                      && (p.IdProducto.ToString() + " " + p.pr_descripcion + " " + p.lote_num_lote).Contains(filter)
                                      && p.Aparece_modu_Ventas == true
                                     select new in_Producto_Info
                                     {
                                         IdEmpresa = p.IdEmpresa,
                                         IdProducto = p.IdProducto,
                                         pr_descripcion = p.pr_descripcion,
                                         lote_num_lote = p.lote_num_lote,
                                         lote_fecha_vcto = p.lote_fecha_vcto,
                                         nom_categoria = c.ca_Categoria,
                                         nom_presentacion = pr.nom_presentacion
                                     })
                                    .OrderBy(p => p.IdProducto)
                                    .Skip(skip)
                                    .Take(take)
                                    .ToList();
                            break;
                        case cl_enumeradores.eModulo.COM:
                            Lista = (from p in Context.in_Producto
                                     join c in Context.in_categorias
                                     on new { p.IdEmpresa, p.IdCategoria } equals new { c.IdEmpresa, c.IdCategoria }
                                     join pr in Context.in_presentacion
                                     on new { p.IdEmpresa, p.IdPresentacion } equals new { pr.IdEmpresa, pr.IdPresentacion }
                                     where
                                      p.IdEmpresa == IdEmpresa
                                      && c.IdEmpresa == IdEmpresa
                                      && pr.IdEmpresa == IdEmpresa
                                      && p.Estado == "A"
                                      && (p.IdProducto.ToString() + " " + p.pr_descripcion + " " + p.lote_num_lote).Contains(filter)
                                      && p.Aparece_modu_Compras == true
                                     select new in_Producto_Info
                                     {
                                         IdEmpresa = p.IdEmpresa,
                                         IdProducto = p.IdProducto,
                                         pr_descripcion = p.pr_descripcion,
                                         lote_num_lote = p.lote_num_lote,
                                         lote_fecha_vcto = p.lote_fecha_vcto,
                                         nom_categoria = c.ca_Categoria,
                                         nom_presentacion = pr.nom_presentacion
                                     })
                                    .OrderBy(p => p.IdProducto)
                                    .Skip(skip)
                                    .Take(take)
                                    .ToList();
                            break;
                        case cl_enumeradores.eModulo.ACF:
                            Lista = (from p in Context.in_Producto
                                     join c in Context.in_categorias
                                     on new { p.IdEmpresa, p.IdCategoria } equals new { c.IdEmpresa, c.IdCategoria }
                                     join pr in Context.in_presentacion
                                     on new { p.IdEmpresa, p.IdPresentacion } equals new { pr.IdEmpresa, pr.IdPresentacion }
                                     where
                                      p.IdEmpresa == IdEmpresa
                                      && c.IdEmpresa == IdEmpresa
                                      && pr.IdEmpresa == IdEmpresa
                                      && p.Estado == "A"
                                      && (p.IdProducto.ToString() + " " + p.pr_descripcion + " " + p.lote_num_lote).Contains(filter)
                                      && p.Aparece_modu_Activo_F == true
                                     select new in_Producto_Info
                                     {
                                         IdEmpresa = p.IdEmpresa,
                                         IdProducto = p.IdProducto,
                                         pr_descripcion = p.pr_descripcion,
                                         lote_num_lote = p.lote_num_lote,
                                         lote_fecha_vcto = p.lote_fecha_vcto,
                                         nom_categoria = c.ca_Categoria,
                                         nom_presentacion = pr.nom_presentacion
                                     })
                                    .OrderBy(p => p.IdProducto)
                                    .Skip(skip)
                                    .Take(take)
                                    .ToList();
                            break;
                    }
                }
                Lista = get_list_nombre_combo(Lista);
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<in_Producto_Info> get_list_todos_menos_padres(int IdEmpresa, int skip, int take, string filter)
        {
            try
            {
                List<in_Producto_Info> Lista = new List<in_Producto_Info>();

                Entities_inventario Context = new Entities_inventario();

                var lst = (from
                          p in Context.in_Producto
                           join c in Context.in_categorias
                           on new { p.IdEmpresa, p.IdCategoria } equals new { c.IdEmpresa, c.IdCategoria }
                           join pr in Context.in_presentacion
                          
                           on new { p.IdEmpresa, p.IdPresentacion } equals new { pr.IdEmpresa, pr.IdPresentacion }

                           where
                            p.IdEmpresa == IdEmpresa
                            && c.IdEmpresa == IdEmpresa
                            && pr.IdEmpresa == IdEmpresa
                            && p.Estado == "A"
                            && p.IdProducto_padre!=null
                            && (p.IdProducto.ToString() + " " + p.pr_descripcion + " " + p.lote_num_lote).Contains(filter)
                           select new
                           {
                               p.IdEmpresa,
                               p.IdProducto,
                               p.pr_descripcion,
                               p.pr_descripcion_2,
                               p.pr_codigo,
                               p.lote_num_lote,
                               p.lote_fecha_vcto,
                               c.ca_Categoria,
                               pr.nom_presentacion
                           })
                             .OrderBy(p => p.IdProducto)
                             .Skip(skip)
                             .Take(take)
                             .ToList();


                foreach (var q in lst)
                {
                    Lista.Add(new in_Producto_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdProducto = q.IdProducto,
                        pr_descripcion = q.pr_descripcion,
                        pr_descripcion_2 = q.pr_descripcion_2,
                        pr_codigo = q.pr_codigo,
                        lote_num_lote = q.lote_num_lote,
                        lote_fecha_vcto = q.lote_fecha_vcto,
                        nom_categoria = q.ca_Categoria,
                        nom_presentacion = q.nom_presentacion
                    });
                }

                Context.Dispose();
                Lista = get_list_nombre_combo(Lista);
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        public bool validar_anulacion(int IdEmpresa, decimal IdProducto, ref string mensaje)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var prod = Context.spin_Producto_validar_anulacion(IdEmpresa, IdProducto);
                    foreach (var item in prod)
                    {
                        mensaje = item;
                    }
                    if (mensaje == "OK")
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_stock(List<in_Producto_Stock_Info> Lista, ref string mensaje)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    foreach (var item in Lista)
                    {
                        if (!item.SeDestribuye)
                        {
                            var stock = (from q in Context.vwin_producto_x_tb_bodega_stock_x_lote
                                         where q.IdEmpresa == item.IdEmpresa && q.IdSucursal == item.IdSucursal
                                         && q.IdBodega == item.IdBodega && q.IdProducto == item.IdProducto
                                         select q).FirstOrDefault();
                            if (stock == null)
                            {
                                if (item.tp_manejaInven == "S")
                                {
                                    var stock1 = (from q in Context.vwin_Producto_Stock
                                                  where q.IdEmpresa == item.IdEmpresa && q.IdSucursal == item.IdSucursal
                                                 && q.IdBodega == item.IdBodega && q.IdProducto == item.IdProducto
                                                  select q).FirstOrDefault();
                                    if (stock1 == null)
                                    {
                                        mensaje = "No existe stock para el producto: " + item.pr_descripcion;
                                        return false;
                                    }
                                    else
                                    if ((stock1.Stock + item.CantidadAnterior) < item.Cantidad)
                                    {
                                        mensaje = "El stock para el producto: " + item.pr_descripcion + " es: " + stock1.Stock + " e intenta egresar: " + item.Cantidad;
                                        return false;
                                    }
                                }
                            }
                            else
                            if ((stock.stock + item.CantidadAnterior) < item.Cantidad)
                            {
                                mensaje = "El stock para el producto: " + item.pr_descripcion + " es: " + stock.stock + " e intenta egresar: " + item.Cantidad;
                                return false;
                            }
                        }                        
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
