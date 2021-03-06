﻿using Core.Erp.Data.Compras;
using Core.Erp.Data.Contabilidad;
using Core.Erp.Data.General;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.General;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_Ing_Egr_Inven_Data
    {
        ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
        in_producto_x_tb_bodega_Costo_Historico_Data data_costo = new in_producto_x_tb_bodega_Costo_Historico_Data();
        public List<in_Ing_Egr_Inven_Info> get_list(int IdEmpresa, string signo,int IdSucursal, bool mostrar_anulados, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;
                List<in_Ing_Egr_Inven_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.vwin_Ing_Egr_Inven
                                 where q.IdEmpresa == IdEmpresa
                                 && q.signo == signo 
                                 && IdSucursalIni <= q.IdSucursal
                                 && q.IdSucursal <= IdSucursalFin
                                 && fecha_ini <= q.cm_fecha && q.cm_fecha <= fecha_fin
                                 orderby new {q.IdNumMovi } descending
                                 select new in_Ing_Egr_Inven_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                     IdBodega = q.IdBodega,
                                     IdNumMovi = q.IdNumMovi,
                                     IdMotivo_Inv = q.IdMotivo_Inv,
                                     Estado = q.Estado,
                                     signo = q.signo,
                                     cm_observacion = q.cm_observacion,
                                     CodMoviInven = q.CodMoviInven,
                                     cm_fecha = q.cm_fecha,
                                     tm_descripcion = q.tm_descripcion,
                                     nom_bodega = q.bo_Descripcion,
                                     Desc_mov_inv = q.Desc_mov_inv,
                                     IdEstadoAproba = q.IdEstadoAproba,
                                     EstadoAprobacion = q.EstadoAprobacion,
                                     IdUsuarioAR = q.IdUsuarioAR,
                                     FechaAR = q.FechaAR,
                                     FechaDespacho = q.FechaDespacho,
                                     IdUsuarioDespacho = q.IdUsuarioDespacho,
                                     EstadoBool = q.Estado == "A" ? true : false

                                 }).ToList();

                    else
                        Lista = (from q in Context.vwin_Ing_Egr_Inven
                                 where q.IdEmpresa == IdEmpresa
                                 && q.signo == signo
                                 && IdSucursalIni <= q.IdSucursal
                                 && q.IdSucursal <= IdSucursalFin
                                 && fecha_ini <= q.cm_fecha && q.cm_fecha <= fecha_fin
                                 && q.Estado == "A"
                                 orderby new {q.IdNumMovi } descending
                                 select new in_Ing_Egr_Inven_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                     IdBodega = q.IdBodega,
                                     IdNumMovi = q.IdNumMovi,
                                     IdMotivo_Inv = q.IdMotivo_Inv,
                                     Estado = q.Estado,
                                     signo = q.signo,
                                     cm_observacion = q.cm_observacion,
                                     CodMoviInven = q.CodMoviInven,
                                     cm_fecha = q.cm_fecha,
                                     tm_descripcion = q.tm_descripcion,
                                     nom_bodega = q.bo_Descripcion,
                                     Desc_mov_inv = q.Desc_mov_inv,
                                     IdEstadoAproba = q.IdEstadoAproba,
                                     EstadoAprobacion = q.EstadoAprobacion,
                                     IdUsuarioAR = q.IdUsuarioAR,
                                     FechaAR = q.FechaAR,
                                     FechaDespacho = q.FechaDespacho,
                                     IdUsuarioDespacho = q.IdUsuarioDespacho,
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
        public in_Ing_Egr_Inven_Info get_info(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo, decimal IdNumMovi)
        {
            try
            {
                in_Ing_Egr_Inven_Info info = new in_Ing_Egr_Inven_Info();
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Ing_Egr_Inven Entity = Context.in_Ing_Egr_Inven.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdMovi_inven_tipo == IdMovi_inven_tipo && q.IdNumMovi == IdNumMovi);
                    if (Entity == null) return null;
                    info = new in_Ing_Egr_Inven_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdMovi_inven_tipo = Entity.IdMovi_inven_tipo,
                        IdBodega = Entity.IdBodega,
                        IdNumMovi = Entity.IdNumMovi,
                        IdMotivo_Inv = Entity.IdMotivo_Inv,
                        cm_fecha = Entity.cm_fecha,
                        cm_observacion = Entity.cm_observacion,
                        CodMoviInven = Entity.CodMoviInven,
                        Estado = Entity.Estado,
                        IdResponsable = Entity.IdResponsable,
                        signo = Entity.signo,
                        IdEstadoAproba = Entity.IdEstadoAproba,
                        IdUsuarioAR = Entity.IdUsuarioAR,
                        FechaAR = Entity.FechaAR,
                        FechaDespacho = Entity.FechaDespacho,
                        IdUsuarioDespacho = Entity.IdUsuarioDespacho
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal get_id(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo)
        {
            try
            {
                decimal ID = 1;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_Ing_Egr_Inven
                              where q.IdEmpresa == IdEmpresa
                              && q.IdSucursal == IdSucursal
                              && q.IdMovi_inven_tipo == IdMovi_inven_tipo
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdNumMovi) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(in_Ing_Egr_Inven_Info info, string signo)
        {
            try
            {
                int sec = 1;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    var TipoMovimiento = Context.in_movi_inven_tipo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMovi_inven_tipo == info.IdMovi_inven_tipo).FirstOrDefault();
                    if (TipoMovimiento == null)
                        return false;

                    in_Ing_Egr_Inven Entity = new in_Ing_Egr_Inven
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdMovi_inven_tipo = info.IdMovi_inven_tipo,
                        IdNumMovi = info.IdNumMovi = get_id(info.IdEmpresa, info.IdSucursal, info.IdMovi_inven_tipo),
                        IdBodega = info.IdBodega,
                        IdMotivo_Inv = info.IdMotivo_Inv,
                        cm_fecha = info.cm_fecha.Date,
                        cm_observacion = info.cm_observacion,
                        CodMoviInven = info.CodMoviInven,
                        Estado = info.Estado = "A",
                        IdResponsable = info.IdResponsable,
                        signo = info.signo,
                        IdEstadoAproba = TipoMovimiento.IdCatalogoAprobacion,
                        IdUsuarioAR = info.IdUsuarioAR,
                        FechaAR = info.FechaAR,
                        FechaDespacho = info.FechaDespacho,
                        IdUsuarioDespacho = info.IdUsuarioDespacho,

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.in_Ing_Egr_Inven.Add(Entity);

                    foreach (var item in info.lst_in_Ing_Egr_Inven_det)
                    {
                        if (signo == "-")
                            item.mv_costo_sinConversion = data_costo.get_ultimo_costo(info.IdEmpresa, info.IdSucursal, Convert.ToInt32(info.IdBodega), item.IdProducto, info.cm_fecha);
                        in_Ing_Egr_Inven_det entity_det = new in_Ing_Egr_Inven_det
                        {

                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdMovi_inven_tipo = info.IdMovi_inven_tipo,
                            IdNumMovi = info.IdNumMovi,
                            Secuencia = item.Secuencia = sec,
                            IdBodega = (int)info.IdBodega,
                            IdProducto = item.IdProducto,

                            dm_observacion = item.dm_observacion,
                            IdMotivo_Inv = item.IdMotivo_Inv_det == 0 ? null : (int?)item.IdMotivo_Inv_det,

                            IdEmpresa_oc = item.IdEmpresa_oc,
                            IdSucursal_oc = item.IdSucursal_oc,
                            IdOrdenCompra = item.IdOrdenCompra,
                            Secuencia_oc = item.Secuencia_oc,

                            IdEmpresa_inv = item.IdEmpresa_inv,
                            IdSucursal_inv = item.IdSucursal_inv,
                            IdBodega_inv = item.IdBodega_inv,
                            IdMovi_inven_tipo_inv = item.IdMovi_inven_tipo_inv,
                            IdNumMovi_inv = item.IdNumMovi_inv,
                            secuencia_inv = item.secuencia_inv,

                            dm_cantidad_sinConversion = Math.Abs(item.dm_cantidad_sinConversion) * (info.signo == "-" ? -1 : 1),
                            IdUnidadMedida_sinConversion = (item.IdUnidadMedida_sinConversion) == null ? "UNID" : item.IdUnidadMedida_sinConversion,
                            mv_costo_sinConversion = item.mv_costo_sinConversion,

                            IdUnidadMedida = (item.IdUnidadMedida) == null ? "UNID" : item.IdUnidadMedida,
                            dm_cantidad = Math.Abs(item.dm_cantidad_sinConversion) * (info.signo == "-" ? -1 : 1),
                            mv_costo = item.mv_costo_sinConversion,

                            IdCentroCosto = item.IdCentroCosto,
                            IdPunto_cargo = item.IdPunto_cargo,
                            IdPunto_cargo_grupo = item.IdPunto_cargo_grupo

                        };
                        Context.in_Ing_Egr_Inven_det.Add(entity_det);
                        sec++;
                    }
                    Context.SaveChanges();


                    if (TipoMovimiento.IdCatalogoAprobacion == "APRO")
                    {
                        Context.spINV_aprobacion_ing_egr(info.IdEmpresa, info.IdSucursal, info.IdBodega, info.IdMovi_inven_tipo, info.IdNumMovi);
                        Contabilizar(info.IdEmpresa, info.IdSucursal, info.IdMovi_inven_tipo, info.IdNumMovi, info.cm_observacion, info.cm_fecha);
                    }

                    #region Validar estado cierre OC
                    var lstOC = info.lst_in_Ing_Egr_Inven_det.Where(q => q.IdOrdenCompra != null).GroupBy(q => new { q.IdEmpresa_oc, q.IdSucursal_oc, q.IdOrdenCompra }).Select(q => new
                    {
                        IdEmpresa_oc = q.Key.IdEmpresa_oc,
                        IdSucursal_oc = q.Key.IdSucursal_oc,
                        IdOrdenCompra = q.Key.IdOrdenCompra
                    }).ToList();
                    com_ordencompra_local_Data ComData = new com_ordencompra_local_Data();
                    foreach (var item in lstOC)
                    {
                        if(item.IdEmpresa_oc != null && item.IdEmpresa_oc != 0)
                            ComData.ValidarEstadoCierre(item.IdEmpresa_oc ?? 0, item.IdSucursal_oc ?? 0, item.IdOrdenCompra ?? 0);
                    }
                    #endregion
                }
                
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "in_Ing_Egr_Inven_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarDB(in_Ing_Egr_Inven_Info info)
        {
            try
            {
                int sec = 1;
                
                //ReversarAprobacion(info.IdEmpresa, info.IdSucursal, info.IdMovi_inven_tipo, info.IdNumMovi);

                using (Entities_inventario Context = new Entities_inventario())
                {
                    var TipoMovimiento = Context.in_movi_inven_tipo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMovi_inven_tipo == info.IdMovi_inven_tipo).FirstOrDefault();
                    if (TipoMovimiento == null)
                        return false;

                    in_Ing_Egr_Inven Entity = Context.in_Ing_Egr_Inven.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdMovi_inven_tipo == info.IdMovi_inven_tipo && q.IdNumMovi == info.IdNumMovi);
                    if (Entity == null) return false;

                    Entity.cm_observacion = info.cm_observacion;
                    Entity.CodMoviInven = info.CodMoviInven;
                    Entity.cm_fecha = info.cm_fecha.Date;
                    Entity.IdResponsable = info.IdResponsable;
                    Entity.IdMotivo_Inv = info.IdMotivo_Inv;
                    Entity.IdBodega = info.IdBodega;
                    Entity.IdEstadoAproba = TipoMovimiento.IdCatalogoAprobacion;
                    Entity.IdUsuarioUltModi = info.IdUsuarioUltModi;
                    Entity.Fecha_UltMod = DateTime.Now;

                    var lst = Context.in_Ing_Egr_Inven_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdMovi_inven_tipo == info.IdMovi_inven_tipo && q.IdNumMovi == info.IdNumMovi);
                    Context.in_Ing_Egr_Inven_det.RemoveRange(lst);

                    foreach (var item in info.lst_in_Ing_Egr_Inven_det)
                    {
                        if (info.signo == "-")
                            item.mv_costo_sinConversion = data_costo.get_ultimo_costo(info.IdEmpresa, info.IdSucursal, Convert.ToInt32(info.IdBodega), item.IdProducto, info.cm_fecha);
                        Context.in_Ing_Egr_Inven_det.Add(new in_Ing_Egr_Inven_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdMovi_inven_tipo = info.IdMovi_inven_tipo,
                            IdNumMovi = info.IdNumMovi,
                            Secuencia = sec,
                            IdBodega = (int)info.IdBodega,
                            IdProducto = item.IdProducto,

                            dm_observacion = item.dm_observacion,
                            IdMotivo_Inv = item.IdMotivo_Inv_det == 0 ? null : (int?)item.IdMotivo_Inv_det,

                            IdEmpresa_oc = item.IdEmpresa_oc,
                            IdSucursal_oc = item.IdSucursal_oc,
                            IdOrdenCompra = item.IdOrdenCompra,
                            Secuencia_oc = item.Secuencia_oc,

                            //Deben ir null para que al aprobarse se liguen con el nuevo
                            IdEmpresa_inv = null,
                            IdSucursal_inv = null,
                            IdBodega_inv = null,
                            IdMovi_inven_tipo_inv = null,
                            IdNumMovi_inv = null,
                            secuencia_inv = null,

                            dm_cantidad_sinConversion = Math.Abs(item.dm_cantidad_sinConversion) * (info.signo == "-" ? -1 : 1),
                            dm_cantidad = Math.Abs(item.dm_cantidad_sinConversion) * (info.signo == "-" ? -1 : 1),

                            IdUnidadMedida = (item.IdUnidadMedida) == null ? "UNID" : item.IdUnidadMedida,
                            IdUnidadMedida_sinConversion = (item.IdUnidadMedida_sinConversion) == null ? "UNID" : item.IdUnidadMedida_sinConversion,

                            mv_costo_sinConversion = item.mv_costo_sinConversion,
                            mv_costo =  item.mv_costo_sinConversion,

                            IdCentroCosto = item.IdCentroCosto,
                            IdPunto_cargo = item.IdPunto_cargo,
                            IdPunto_cargo_grupo = item.IdPunto_cargo_grupo
                        });                        
                        sec++;
                    }
                    Context.SaveChanges();
                    if (TipoMovimiento.IdCatalogoAprobacion == "APRO")
                    {
                        Context.spINV_aprobacion_ing_egr(info.IdEmpresa, info.IdSucursal, info.IdBodega, info.IdMovi_inven_tipo, info.IdNumMovi);
                        Contabilizar(info.IdEmpresa, info.IdSucursal, info.IdMovi_inven_tipo, info.IdNumMovi, info.cm_observacion, info.cm_fecha);
                    }

                    #region Validar estado cierre OC
                    var lstOC = info.lst_in_Ing_Egr_Inven_det.Where(q => q.IdOrdenCompra != null).GroupBy(q => new { q.IdEmpresa_oc, q.IdSucursal_oc, q.IdOrdenCompra }).Select(q => new
                    {
                        IdEmpresa_oc = q.Key.IdEmpresa_oc,
                        IdSucursal_oc = q.Key.IdSucursal_oc,
                        IdOrdenCompra = q.Key.IdOrdenCompra
                    }).ToList();
                    com_ordencompra_local_Data ComData = new com_ordencompra_local_Data();
                    foreach (var item in lstOC)
                    {
                        if (item.IdEmpresa_oc != null && item.IdEmpresa_oc != 0)
                            ComData.ValidarEstadoCierre(item.IdEmpresa_oc ?? 0, item.IdSucursal_oc ?? 0, item.IdOrdenCompra ?? 0);
                    }
                    #endregion
                    return true;
                }
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "in_Ing_Egr_Inven_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool anularDB(in_Ing_Egr_Inven_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Ing_Egr_Inven Entity = Context.in_Ing_Egr_Inven.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdMovi_inven_tipo == info.IdMovi_inven_tipo && q.IdNumMovi == info.IdNumMovi);
                    if (Entity == null) return false;

                    if (Entity.Estado == "I")
                        return true;

                    Entity.Estado = info.Estado="I";
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdusuarioUltAnu = info.IdusuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
                    Context.SaveChanges();

                    ReversarAprobacion(info.IdEmpresa, info.IdSucursal, info.IdMovi_inven_tipo, info.IdNumMovi);

                    #region Validar estado cierre OC
                    var lstOC = info.lst_in_Ing_Egr_Inven_det.Where(q => q.IdOrdenCompra != null).GroupBy(q => new { q.IdEmpresa_oc, q.IdSucursal_oc, q.IdOrdenCompra }).Select(q => new
                    {
                        IdEmpresa_oc = q.Key.IdEmpresa_oc,
                        IdSucursal_oc = q.Key.IdSucursal_oc,
                        IdOrdenCompra = q.Key.IdOrdenCompra
                    }).ToList();
                    com_ordencompra_local_Data ComData = new com_ordencompra_local_Data();
                    foreach (var item in lstOC)
                    {
                        if (item.IdEmpresa_oc != null && item.IdEmpresa_oc != 0)
                            ComData.ValidarEstadoCierre(item.IdEmpresa_oc ?? 0, item.IdSucursal_oc ?? 0, item.IdOrdenCompra ?? 0);
                    }
                    #endregion
                }

                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }        
        public List<in_Ing_Egr_Inven_Info> get_list_por_devolver(int IdEmpresa, string signo, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                List<in_Ing_Egr_Inven_Info> Lista;
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.vwin_Ing_Egr_Inven_por_devolver
                             where q.IdEmpresa == IdEmpresa
                             && q.signo == signo
                             && Fecha_ini <= q.cm_fecha 
                             && q.cm_fecha <= Fecha_fin
                             select new in_Ing_Egr_Inven_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                 IdNumMovi = q.IdNumMovi,
                                 signo = q.signo,
                                 tm_descripcion = q.tm_descripcion,
                                 cm_observacion = q.cm_observacion,
                                 cm_fecha = q.cm_fecha,
                                 Su_Descripcion = q.Su_Descripcion
                             }).ToList();
                }

                Lista.ForEach(q => q.SecuencialID = q.IdEmpresa.ToString("00") + q.IdSucursal.ToString("00") + q.IdMovi_inven_tipo.ToString("00") + q.IdNumMovi.ToString("00000000"));

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ReversarAprobacion(int IdEmpresa, int IdSucursal, int IdMovi_inve_tipo, decimal IdNumMovi)
        {
            Entities_inventario db_i = new Entities_inventario();
            Entities_contabilidad db_ct = new Entities_contabilidad();
            try
            {
                var lst_det = db_i.in_Ing_Egr_Inven_det.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdMovi_inven_tipo == IdMovi_inve_tipo && q.IdNumMovi == IdNumMovi).ToList();
                if (lst_det.Where(q => q.IdNumMovi_inv == null).Count() == 0)
                {
                    var PK_movi = new {
                        lst_det.First().IdEmpresa_inv,
                        lst_det.First().IdSucursal_inv,
                        lst_det.First().IdBodega_inv,
                        lst_det.First().IdMovi_inven_tipo_inv,
                        lst_det.First().IdNumMovi_inv
                    };

                    #region Elimino detalle de movi inve
                    var lst_movi_d = db_i.in_movi_inve_detalle.Where(q => q.IdEmpresa == PK_movi.IdEmpresa_inv
                                    && q.IdSucursal == PK_movi.IdSucursal_inv
                                    && q.IdBodega == PK_movi.IdBodega_inv
                                    && q.IdMovi_inven_tipo == PK_movi.IdMovi_inven_tipo_inv
                                    && q.IdNumMovi == PK_movi.IdNumMovi_inv
                                    ).ToList();

                    db_i.in_movi_inve_detalle.RemoveRange(lst_movi_d);
                    #endregion

                    #region Elimino cabecera
                    var movi = db_i.in_movi_inve.Where(q => q.IdEmpresa == PK_movi.IdEmpresa_inv
                               && q.IdSucursal == PK_movi.IdSucursal_inv
                               && q.IdBodega == PK_movi.IdBodega_inv
                               && q.IdMovi_inven_tipo == PK_movi.IdMovi_inven_tipo_inv
                               && q.IdNumMovi == PK_movi.IdNumMovi_inv).FirstOrDefault();

                    db_i.in_movi_inve.Remove(movi);
                    #endregion

                    #region Obtengo relacion contable y la elimino
                    var PK_conta = db_i.in_movi_inve_x_ct_cbteCble.Where(q => q.IdEmpresa == PK_movi.IdEmpresa_inv
                                    && q.IdSucursal == PK_movi.IdSucursal_inv
                                    && q.IdBodega == PK_movi.IdBodega_inv
                                    && q.IdMovi_inven_tipo == PK_movi.IdMovi_inven_tipo_inv
                                    && q.IdNumMovi == PK_movi.IdNumMovi_inv
                                    ).FirstOrDefault();
                    
                    #endregion
                    if (PK_conta != null)
                    {
                        #region Elimino diario contable
                        var lst_rel_det = db_i.in_movi_inve_detalle_x_ct_cbtecble_det.Where(q => q.IdEmpresa_inv == PK_movi.IdEmpresa_inv
                                    && q.IdSucursal_inv == PK_movi.IdSucursal_inv
                                    && q.IdBodega_inv == PK_movi.IdBodega_inv
                                    && q.IdMovi_inven_tipo_inv == PK_movi.IdMovi_inven_tipo_inv
                                    && q.IdNumMovi_inv == PK_movi.IdNumMovi_inv).ToList();
                        db_i.in_movi_inve_detalle_x_ct_cbtecble_det.RemoveRange(lst_rel_det);

                        var lst_conta = db_ct.ct_cbtecble_det.Where(q => q.IdEmpresa == PK_conta.IdEmpresa_ct 
                                        && q.IdTipoCbte == PK_conta.IdTipoCbte 
                                        && q.IdCbteCble == PK_conta.IdCbteCble
                                        ).ToList();
                        db_ct.ct_cbtecble_det.RemoveRange(lst_conta);

                        var Conta = db_ct.ct_cbtecble.Where(q => q.IdEmpresa == PK_conta.IdEmpresa
                                    && q.IdTipoCbte == PK_conta.IdTipoCbte
                                    && q.IdCbteCble == PK_conta.IdCbteCble
                                    ).FirstOrDefault();
                        db_ct.ct_cbtecble.Remove(Conta);
                        #endregion
                        db_i.in_movi_inve_x_ct_cbteCble.Remove(PK_conta);
                    }
                }
                #region Seteo campos de aprobacion en null
                lst_det.ForEach(q =>
                {
                    q.IdEmpresa_inv = null;
                    q.IdSucursal_inv = null;
                    q.IdBodega_inv = null;
                    q.IdMovi_inven_tipo_inv = null;
                    q.IdNumMovi_inv = null;

                    q.IdEmpresa_oc = null;
                    q.IdSucursal_oc = null;
                    q.IdOrdenCompra = null;
                    q.Secuencia_oc = null;
                });
                #endregion

                db_i.SaveChanges();
                db_ct.SaveChanges();

                db_ct.Dispose();
                db_i.Dispose();
                return true;
            }
            catch (Exception)
            {
                db_ct.Dispose();
                db_i.Dispose();
                throw;
            }
        }
        public bool Contabilizar(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo, decimal IdNumMovi, string Observacion, DateTime Fecha)
        {
            Entities_inventario db_i = new Entities_inventario();
            Entities_contabilidad db_c = new Entities_contabilidad();
            try
            {
                var tipo_movi = db_i.in_movi_inven_tipo.Where(q => q.IdEmpresa == IdEmpresa && q.IdMovi_inven_tipo == IdMovi_inven_tipo).FirstOrDefault();
                if (tipo_movi == null)
                    return false;
                
                if (tipo_movi.IdTipoCbte == null)
                    return false;

                if (tipo_movi.Genera_Diario_Contable == false)
                    return false;


                #region Get list
                var lst = new List<in_Ing_Egr_Inven_det_Info>();
                using (SqlConnection connection = new SqlConnection(ConexionesERP.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT        dbo.in_Ing_Egr_Inven_det.IdEmpresa, dbo.in_Ing_Egr_Inven_det.IdSucursal, dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven_det.IdNumMovi, dbo.in_Ing_Egr_Inven_det.Secuencia, "
                                        + " dbo.in_Ing_Egr_Inven_det.IdEmpresa_inv, dbo.in_Ing_Egr_Inven_det.IdSucursal_inv, dbo.in_Ing_Egr_Inven_det.IdBodega_inv, dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo_inv, dbo.in_Ing_Egr_Inven_det.IdNumMovi_inv, "
                                        + " dbo.in_Ing_Egr_Inven_det.secuencia_inv, dbo.in_producto_x_tb_bodega.IdCtaCble_Inven AS IdCtaCtble_Inve, dbo.in_categorias.IdCtaCtble_Costo, dbo.in_Motivo_Inven.IdCtaCble AS IdCtaCble_Motivo, "
                                        + " dbo.in_parametro.P_IdCtaCble_transitoria_transf_inven, dbo.in_Ing_Egr_Inven_det.dm_cantidad* dbo.in_Ing_Egr_Inven_det.mv_costo AS Valor, dbo.in_Ing_Egr_Inven.Estado, "
                                        + " CASE WHEN dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo = in_parametro.IdMovi_inven_tipo_egresoBodegaOrigen OR"
                                        + " dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo = in_parametro.IdMovi_inven_tipo_ingresoBodegaDestino THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS EsTransferencia, dbo.in_Ing_Egr_Inven_det.IdCentroCosto, "
                                        + " in_Motivo_Inven_1.IdCtaCble AS IdCtaCble_MotivoDet, dbo.in_producto_x_tb_bodega.IdCtaCble_Costo AS IdCtaCble_CostoProducto, CASE WHEN len(in_Producto.pr_codigo)"
                                        + " = 0 THEN '' ELSE '[' + in_Producto.pr_codigo + '] ' END + dbo.in_Producto.pr_descripcion AS pr_descripcion, dbo.tb_bodega.bo_Descripcion, in_Ing_Egr_Inven.CodMoviInven"
                                        + " FROM            dbo.in_Ing_Egr_Inven_det INNER JOIN"
                                        + " dbo.in_Producto ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_Ing_Egr_Inven_det.IdProducto = dbo.in_Producto.IdProducto INNER JOIN"
                                        + " dbo.in_categorias ON dbo.in_Producto.IdEmpresa = dbo.in_categorias.IdEmpresa AND dbo.in_Producto.IdCategoria = dbo.in_categorias.IdCategoria INNER JOIN"
                                        + " dbo.tb_bodega ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.in_Ing_Egr_Inven_det.IdSucursal = dbo.tb_bodega.IdSucursal AND"
                                        + " dbo.in_Ing_Egr_Inven_det.IdBodega = dbo.tb_bodega.IdBodega INNER JOIN"
                                        + " dbo.in_Ing_Egr_Inven ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.in_Ing_Egr_Inven.IdEmpresa AND dbo.in_Ing_Egr_Inven_det.IdSucursal = dbo.in_Ing_Egr_Inven.IdSucursal AND"
                                        + " dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo = dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo AND dbo.in_Ing_Egr_Inven_det.IdNumMovi = dbo.in_Ing_Egr_Inven.IdNumMovi INNER JOIN"
                                        + " dbo.in_Motivo_Inven ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.in_Motivo_Inven.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdMotivo_Inv = dbo.in_Motivo_Inven.IdMotivo_Inv INNER JOIN"
                                        + " dbo.in_parametro ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.in_parametro.IdEmpresa LEFT OUTER JOIN"
                                        + " dbo.in_producto_x_tb_bodega ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.in_producto_x_tb_bodega.IdEmpresa AND dbo.in_Ing_Egr_Inven_det.IdSucursal = dbo.in_producto_x_tb_bodega.IdSucursal AND"
                                        + " dbo.in_Ing_Egr_Inven_det.IdBodega = dbo.in_producto_x_tb_bodega.IdBodega AND dbo.in_Ing_Egr_Inven_det.IdProducto = dbo.in_producto_x_tb_bodega.IdProducto LEFT OUTER JOIN"
                                        + " dbo.in_Motivo_Inven AS in_Motivo_Inven_1 ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = in_Motivo_Inven_1.IdEmpresa AND dbo.in_Ing_Egr_Inven_det.IdMotivo_Inv = in_Motivo_Inven_1.IdMotivo_Inv"
                                        + " WHERE(dbo.in_Ing_Egr_Inven_det.IdSucursal_inv IS NOT NULL) and dbo.in_Ing_Egr_Inven_det.IdEmpresa = " + IdEmpresa.ToString() + " and dbo.in_Ing_Egr_Inven_det.IdSucursal = " + IdSucursal.ToString() + " and dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo = " + IdMovi_inven_tipo.ToString() + " and dbo.in_Ing_Egr_Inven_det.IdNumMovi = " + IdNumMovi.ToString();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        lst.Add(new in_Ing_Egr_Inven_det_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdSucursal = Convert.ToInt32(reader["IdSucursal"]),
                            IdMovi_inven_tipo = Convert.ToInt32(reader["IdMovi_inven_tipo"]),
                            IdNumMovi = Convert.ToInt32(reader["IdNumMovi"]),
                            Secuencia = Convert.ToInt32(reader["Secuencia"]),
                            IdEmpresa_inv = string.IsNullOrEmpty(reader["IdEmpresa_inv"].ToString()) ? (int?)null : (int?)(reader["IdEmpresa_inv"]),
                            IdSucursal_inv = string.IsNullOrEmpty(reader["IdSucursal_inv"].ToString()) ? (int?)null : (int?)(reader["IdSucursal_inv"]),
                            IdBodega_inv = string.IsNullOrEmpty(reader["IdBodega_inv"].ToString())?(int?)null : (int?)(reader["IdBodega_inv"]),
                            IdMovi_inven_tipo_inv = string.IsNullOrEmpty(reader["IdMovi_inven_tipo_inv"].ToString()) ? (int?)null : (int?)(reader["IdMovi_inven_tipo_inv"]),
                            IdNumMovi_inv = string.IsNullOrEmpty(reader["IdNumMovi_inv"].ToString()) ? (decimal?)null : (decimal?)(reader["IdNumMovi_inv"]),
                            secuencia_inv = string.IsNullOrEmpty(reader["secuencia_inv"].ToString()) ? (int?)null : (int?)(reader["secuencia_inv"]),
                            IdCtaCtble_Inve = string.IsNullOrEmpty(reader["IdCtaCtble_Inve"].ToString()) ? null : reader["IdCtaCtble_Inve"].ToString(),
                            IdCtaCtble_Costo = string.IsNullOrEmpty(reader["IdCtaCtble_Costo"].ToString()) ? null : reader["IdCtaCtble_Costo"].ToString(),
                            IdCtaCble_Motivo = string.IsNullOrEmpty(reader["IdCtaCble_Motivo"].ToString()) ? null: reader["IdCtaCble_Motivo"].ToString(),
                            P_IdCtaCble_transitoria_transf_inven = string.IsNullOrEmpty(reader["P_IdCtaCble_transitoria_transf_inven"].ToString()) ? null : reader["P_IdCtaCble_transitoria_transf_inven"].ToString(),
                            mv_costo = Convert.ToDouble(reader["Valor"]), 
                            EsTransferencia = string.IsNullOrEmpty(reader["EsTransferencia"].ToString()) ? false : Convert.ToBoolean(reader["EsTransferencia"]),
                            IdCtaCble_MotivoDet = string.IsNullOrEmpty(reader["IdCtaCble_MotivoDet"].ToString()) ? null : reader["IdCtaCble_MotivoDet"].ToString(),
                            IdCentroCosto = string.IsNullOrEmpty(reader["EsTransferencia"].ToString()) ? null : reader["IdCentroCosto"].ToString(),
                            IdCtaCble_CostoProducto = string.IsNullOrEmpty(reader["IdCtaCble_CostoProducto"].ToString()) ? null : reader["IdCtaCble_CostoProducto"].ToString(),
                            pr_descripcion = reader["pr_descripcion"].ToString(),
                            bo_Descripcion = reader["bo_Descripcion"].ToString(),
                            CodMoviInven = string.IsNullOrEmpty(reader["CodMoviInven"].ToString()) ? null : Convert.ToString(reader["CodMoviInven"])
                        });
                    }
                }
                #endregion
                
                if (lst.Count == 0)
                    return false;

                var lst_g = lst.GroupBy(q=> new
                             {
                                 q.IdCtaCble_Motivo,
                                 q.IdCtaCtble_Costo,
                                 q.IdCtaCtble_Inve,
                                 q.P_IdCtaCble_transitoria_transf_inven,
                                 q.EsTransferencia,
                                 q.IdCtaCble_MotivoDet,
                                 q.IdCentroCosto,

                                 q.IdEmpresa_inv,
                                 q.IdSucursal_inv,
                                 q.IdMovi_inven_tipo_inv,
                                 q.IdBodega_inv,
                                 q.IdNumMovi_inv,
                                 q.IdCtaCble_CostoProducto,
                                 q.pr_descripcion,
                                 q.bo_Descripcion,
                                 q.CodMoviInven,
                }).Select(g=> new
                             {
                                 g.Key.IdCtaCble_Motivo,
                                 g.Key.IdCtaCtble_Costo,
                                 g.Key.IdCtaCtble_Inve,
                                 g.Key.P_IdCtaCble_transitoria_transf_inven,
                                 g.Key.EsTransferencia,
                                 g.Key.IdCtaCble_MotivoDet,
                                 g.Key.IdCentroCosto,

                                 g.Key.IdEmpresa_inv,
                                 g.Key.IdSucursal_inv,
                                 g.Key.IdMovi_inven_tipo_inv,
                                 g.Key.IdBodega_inv,
                                 g.Key.IdNumMovi_inv,
                                 g.Key.IdCtaCble_CostoProducto,
                                 g.Key.pr_descripcion,
                                 g.Key.bo_Descripcion,
                                 g.Key.CodMoviInven,
                                 Valor = g.Sum(q=>q.mv_costo)
                             }).ToList();

                string CodMoviInven = lst_g[0].CodMoviInven;
                
                List < in_movi_inve_detalle_x_ct_cbtecble_det > lst_rel = new List<in_movi_inve_detalle_x_ct_cbtecble_det>();
                List<ct_cbtecble_det_Info> lst_ct = new List<ct_cbtecble_det_Info>();
                int Secuencia = 1;
                foreach (var item in lst_g)
                {
                    //Debe
                    lst_ct.Add(new ct_cbtecble_det_Info
                    {
                        secuencia = Secuencia++,
                        IdCtaCble = tipo_movi.cm_tipo_movi == "+" ? item.IdCtaCtble_Inve 
                        : ((bool)item.EsTransferencia ? item.P_IdCtaCble_transitoria_transf_inven 
                        : (!string.IsNullOrEmpty(item.IdCtaCble_MotivoDet) ? item.IdCtaCble_MotivoDet 
                        : (string.IsNullOrEmpty(item.IdCtaCble_CostoProducto) ? item.IdCtaCtble_Costo : item.IdCtaCble_CostoProducto))),
                        IdCentroCosto = item.IdCentroCosto,
                        dc_Valor = Math.Abs(Math.Round(item.Valor, 2, MidpointRounding.AwayFromZero)),
                        dc_Observacion = item.bo_Descripcion +" "+ item.pr_descripcion
                    });
                    //Haber
                    lst_ct.Add(new ct_cbtecble_det_Info
                    {
                        secuencia = Secuencia++,
                        IdCtaCble = tipo_movi.cm_tipo_movi == "-" ? item.IdCtaCtble_Inve 
                        : ((bool)item.EsTransferencia ? item.P_IdCtaCble_transitoria_transf_inven 
                        : (!string.IsNullOrEmpty(item.IdCtaCble_MotivoDet) ? item.IdCtaCble_MotivoDet 
                        : (string.IsNullOrEmpty(item.IdCtaCble_CostoProducto) ? item.IdCtaCtble_Costo : item.IdCtaCble_CostoProducto))),
                        IdCentroCosto = item.IdCentroCosto,
                        dc_Valor = Math.Abs(Math.Round(item.Valor, 2, MidpointRounding.AwayFromZero)) * -1,
                        dc_Observacion = item.bo_Descripcion + " " + item.pr_descripcion
                    });
                }

                var diario = odata_ct.armar_info(lst_ct, IdEmpresa, IdSucursal, (int)tipo_movi.IdTipoCbte, 0, (string.IsNullOrEmpty(CodMoviInven) ? "" : ("OP "+ CodMoviInven+" ")) + tipo_movi.tm_descripcion + " #" + IdNumMovi + " " + Observacion, Fecha);

                diario.lst_ct_cbtecble_det.RemoveAll(q => q.dc_Valor == 0);
                diario.lst_ct_cbtecble_det.RemoveAll(q => string.IsNullOrEmpty(q.IdCtaCble));

                if (diario.lst_ct_cbtecble_det.Count == 0)
                    return false;

                if (diario.lst_ct_cbtecble_det.Where(q => q.dc_Valor == 0).Count() > 0)
                    return false;

                double descuadre = Math.Round(diario.lst_ct_cbtecble_det.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero);
                if (descuadre < -0.01 || 0.01 <= descuadre)
                    return false;

                if ((descuadre <= 0.01 || -0.01 <= descuadre) && descuadre != 0)
                {
                    if (descuadre > 0)
                        diario.lst_ct_cbtecble_det.Where(q => q.dc_Valor < 0).FirstOrDefault().dc_Valor -= descuadre;
                    else
                        diario.lst_ct_cbtecble_det.Where(q => q.dc_Valor > 0).FirstOrDefault().dc_Valor += (descuadre * -1);
                }

                descuadre = Math.Round(diario.lst_ct_cbtecble_det.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero);
                if (descuadre != 0)
                    return false;

                if (odata_ct.guardarDB(diario))
                {
                    var First = lst_g.First();
                    db_i.in_movi_inve_x_ct_cbteCble.Add(new in_movi_inve_x_ct_cbteCble
                    {
                        IdEmpresa = (int)First.IdEmpresa_inv,
                        IdSucursal = (int)First.IdSucursal_inv,
                        IdBodega = (int)First.IdBodega_inv,
                        IdMovi_inven_tipo = (int)First.IdMovi_inven_tipo_inv,
                        IdNumMovi = (decimal)First.IdNumMovi_inv,

                        IdEmpresa_ct = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,

                        Observacion = ""
                    });
                    db_i.SaveChanges();
                    db_c.SaveChanges();
                }

                db_c.Dispose();
                db_i.Dispose();
                
                return true;
            }
            catch (Exception ex)
            {
                db_c.Dispose();
                db_i.Dispose();
                throw;
            }
        }
        public bool ReContabilizar(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo, decimal IdNumMovi, string Observacion, DateTime Fecha)
        {
            try
            {
                EliminarContabilizacion(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);
                Contabilizar(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi, Observacion, Fecha);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool EliminarContabilizacion(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo, decimal IdNumMovi)
        {
            Entities_inventario db_i = new Entities_inventario();
            Entities_contabilidad db_ct = new Entities_contabilidad();
            try
            {
                var lst_det = db_i.in_Ing_Egr_Inven_det.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdMovi_inven_tipo == IdMovi_inven_tipo && q.IdNumMovi == IdNumMovi).ToList();
                if (lst_det.Where(q => q.IdNumMovi_inv == null).Count() == 0)
                {
                    var PK_movi = new
                    {
                        lst_det.First().IdEmpresa_inv,
                        lst_det.First().IdSucursal_inv,
                        lst_det.First().IdBodega_inv,
                        lst_det.First().IdMovi_inven_tipo_inv,
                        lst_det.First().IdNumMovi_inv
                    };

                    #region Obtengo relacion contable y la elimino
                    var PK_conta = db_i.in_movi_inve_x_ct_cbteCble.Where(q => q.IdEmpresa == PK_movi.IdEmpresa_inv
                                    && q.IdSucursal == PK_movi.IdSucursal_inv
                                    && q.IdBodega == PK_movi.IdBodega_inv
                                    && q.IdMovi_inven_tipo == PK_movi.IdMovi_inven_tipo_inv
                                    && q.IdNumMovi == PK_movi.IdNumMovi_inv
                                    ).FirstOrDefault();

                    #endregion
                    if (PK_conta != null)
                    {
                        #region Elimino diario contable
                        var lst_rel_det = db_i.in_movi_inve_detalle_x_ct_cbtecble_det.Where(q => q.IdEmpresa_inv == PK_movi.IdEmpresa_inv
                                    && q.IdSucursal_inv == PK_movi.IdSucursal_inv
                                    && q.IdBodega_inv == PK_movi.IdBodega_inv
                                    && q.IdMovi_inven_tipo_inv == PK_movi.IdMovi_inven_tipo_inv
                                    && q.IdNumMovi_inv == PK_movi.IdNumMovi_inv).ToList();
                        db_i.in_movi_inve_detalle_x_ct_cbtecble_det.RemoveRange(lst_rel_det);

                        var lst_conta = db_ct.ct_cbtecble_det.Where(q => q.IdEmpresa == PK_conta.IdEmpresa_ct
                                        && q.IdTipoCbte == PK_conta.IdTipoCbte
                                        && q.IdCbteCble == PK_conta.IdCbteCble
                                        ).ToList();
                        db_ct.ct_cbtecble_det.RemoveRange(lst_conta);

                        var Conta = db_ct.ct_cbtecble.Where(q => q.IdEmpresa == PK_conta.IdEmpresa
                                    && q.IdTipoCbte == PK_conta.IdTipoCbte
                                    && q.IdCbteCble == PK_conta.IdCbteCble
                                    ).FirstOrDefault();
                        db_ct.ct_cbtecble.Remove(Conta);
                        #endregion
                        db_i.in_movi_inve_x_ct_cbteCble.Remove(PK_conta);
                    }
                }

                db_i.SaveChanges();
                db_ct.SaveChanges();

                db_ct.Dispose();
                db_i.Dispose();

                return true;
            }
            catch (Exception)
            {
                db_ct.Dispose();
                db_i.Dispose();
                throw;
            }
        }
        public List<in_Ing_Egr_Inven_Info> BuscarMovimientos(int IdEmpresa, DateTime FechaInicio, DateTime FechaFin, string TipoMovimiento)
        {
            try
            {
                FechaInicio = FechaInicio.Date;
                FechaFin = FechaFin.Date;

                List<in_Ing_Egr_Inven_Info> Lista = new List<in_Ing_Egr_Inven_Info>();

                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.vwin_movi_inve_x_estado_contabilizacion
                               where q.IdEmpresa == IdEmpresa
                               && q.cm_fecha >= FechaInicio && q.cm_fecha <= FechaFin
                               && q.cm_tipo == TipoMovimiento
                               orderby new { q.cm_fecha } ascending
                               select new in_Ing_Egr_Inven_Info
                               {
                                   IdEmpresa = q.IdEmpresa,
                                   IdSucursal = q.IdSucursal,
                                   IdMovi_inven_tipo =  q.IdMovi_inven_tipo,
                                   IdNumMovi = q.IdNumMovi,
                                   Su_Descripcion = q.nom_sucursal,
                                   nom_bodega = q.nom_bodega,
                                   signo = q.cm_tipo,
                                   cm_fecha = q.cm_fecha,
                                   TotalModulo = q.TotalModulo,
                                   TotalContabilidad = q.TotalContabilidad,
                                   Diferencia = q.Diferencia,
                                   Estado_contabilizacion = q.Estado_contabilizacion,
                               }).ToList();

                    Lista.ForEach(q => q.CodMoviInven = (q.IdEmpresa.ToString("00") + q.IdSucursal.ToString("00") + q.IdMovi_inven_tipo.ToString("0000") + q.IdNumMovi.ToString("00000000")));

                    foreach (var item in Lista)
                    {
                        if(item.signo == "+")
                        {
                            item.signo = "INGRESO";
                        }
                        else if (item.signo == "-")
                        {
                            item.signo = "EGRESO";
                        }
                        else
                        {
                            item.signo = "";
                        }
                    }
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Aprobar(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo, decimal IdNumMovi)
        {
            try
            {
                using (Entities_inventario db = new Entities_inventario())
                {
                    #region Movimiento pre aprobado
                    var c = db.in_Ing_Egr_Inven.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdMovi_inven_tipo == IdMovi_inven_tipo && q.IdNumMovi == IdNumMovi).FirstOrDefault();
                    if (c == null)
                        return false;

                    var d = db.in_Ing_Egr_Inven_det.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdMovi_inven_tipo == IdMovi_inven_tipo && q.IdNumMovi == IdNumMovi).ToList();
                    if(d.Count == 0)
                        return false;
                    #endregion

                    #region Cabecera
                    in_movi_inve cab = new in_movi_inve
                    {
                        IdEmpresa = IdEmpresa,
                        IdSucursal = IdSucursal,
                        IdMovi_inven_tipo = IdMovi_inven_tipo,
                        IdBodega = d.First().IdBodega,
                        IdNumMovi = GetId_movi_inven(IdEmpresa,IdSucursal,d.First().IdBodega,IdMovi_inven_tipo ,IdNumMovi),
                        CodMoviInven = c.CodMoviInven,
                        cm_tipo = c.signo,
                        cm_observacion = c.cm_observacion,
                        cm_fecha = c.cm_fecha,
                        Estado = "A",
                        IdMotivo_Inv = c.IdMotivo_Inv
                    };
                    db.in_movi_inve.Add(cab);
                    db.SaveChanges();
                    #endregion

                    #region Detalle
                    int Secuencia = 1;
                    foreach (var item in d)
                    {
                        var det = db.vwin_Ing_Egr_Inven_det_conversion.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdSucursal == item.IdSucursal && q.IdMovi_inven_tipo == item.IdMovi_inven_tipo && q.IdNumMovi == item.IdNumMovi && q.Secuencia == item.Secuencia).FirstOrDefault();
                        if (det != null)
                        {
                            db.in_movi_inve_detalle.Add(new in_movi_inve_detalle
                            {
                                IdEmpresa = cab.IdEmpresa,
                                IdSucursal = cab.IdSucursal,
                                IdBodega = cab.IdBodega,
                                IdMovi_inven_tipo = cab.IdMovi_inven_tipo,
                                IdNumMovi = cab.IdNumMovi,
                                Secuencia = Secuencia,

                                mv_tipo_movi = cab.cm_tipo,
                                IdProducto = item.IdProducto,
                                dm_observacion = item.dm_observacion,

                                dm_cantidad_sinConversion = item.dm_cantidad_sinConversion,
                                IdUnidadMedida_sinConversion = item.IdUnidadMedida_sinConversion,
                                mv_costo_sinConversion = item.mv_costo_sinConversion,

                                IdUnidadMedida = det.IdUnidadMedida_Consumo,
                                dm_cantidad = det.dm_cantidad,
                                mv_costo = det.mv_costo,

                                Costeado = true,
                                IdMotivo_Inv = item.IdMotivo_Inv,                                
                            });
                            item.mv_costo = det.mv_costo;
                            item.dm_cantidad = det.dm_cantidad;
                            item.IdUnidadMedida = det.IdUnidadMedida_Consumo;

                            item.IdEmpresa_inv = cab.IdEmpresa;
                            item.IdSucursal_inv = cab.IdSucursal;
                            item.IdBodega_inv = cab.IdBodega;
                            item.IdMovi_inven_tipo_inv = cab.IdMovi_inven_tipo;
                            item.IdNumMovi_inv = cab.IdNumMovi;
                            item.secuencia_inv = Secuencia++;
                        }
                    }
                    #endregion

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal GetId_movi_inven(int IdEmpresa, int IdSucursal, int IdBodega, int IdMovi_inven_tipo, decimal IdNumMovi)
        {
            try
            {
                decimal ID = 1;

                using (Entities_inventario db = new Entities_inventario())
                {
                    var lst = db.in_movi_inve.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdMovi_inven_tipo == IdMovi_inven_tipo).ToList();
                    if (lst.Count > 0)
                        ID = lst.Max(q => q.IdNumMovi) + 1;
                }

                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<in_Ing_Egr_Inven_Info> get_list_orden_compra(int IdEmpresa, int IdSucursal, bool mostrar_anulados, int IdBodega, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 999999 : IdSucursal;
                int IdBodegaIni = IdBodega;
                int IdBodegaFin = IdBodega == 0 ? 999999 : IdBodega;
                List<in_Ing_Egr_Inven_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = Context.vwin_Ing_Egr_Inven_PorOrdenCompra.Where(q => q.IdEmpresa == IdEmpresa
                             && IdSucursalIni <= q.IdSucursal
                             && q.IdSucursal <= IdSucursalFin
                             && IdBodegaIni <= q.IdBodega
                             && q.IdBodega <= IdBodegaFin
                             && fecha_ini <= q.cm_fecha && q.cm_fecha <= fecha_fin
                             && q.Estado == (mostrar_anulados ? q.Estado : "A")).OrderByDescending(q => q.IdNumMovi).Select(q =>
                              new in_Ing_Egr_Inven_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                 IdBodega = q.IdBodega,
                                 IdNumMovi = q.IdNumMovi,
                                 tm_descripcion = q.tm_descripcion,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Estado = q.Estado,
                                 signo = q.signo,
                                 nom_bodega = q.bo_Descripcion,
                                 Su_Descripcion = q.Su_Descripcion,
                                 cm_observacion = q.cm_observacion,
                                 CodMoviInven = q.CodMoviInven,
                                 cm_fecha = q.cm_fecha,
                                 Desc_mov_inv = q.Desc_mov_inv,
                                 EstadoBool = q.Estado == "A" ? true : false,
                                 IdEstadoAproba = q.IdEstadoAproba,
                                 EstadoAprobacion = q.EstadoAprobacion,
                                 co_factura = q.co_factura
                                 
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<in_Ing_Egr_Inven_Info> get_list_orden_compra_x_ingresar(int IdEmpresa, int IdSucursal,  int IdMovi_inven_tipo)
        {
            try
            {
                List<in_Ing_Egr_Inven_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                        Lista = (from q in Context.in_Ing_Egr_Inven
                                 join t in Context.in_movi_inven_tipo
                                 on new { q.IdEmpresa, q.IdMovi_inven_tipo } equals new { t.IdEmpresa, t.IdMovi_inven_tipo }
                                 where q.IdEmpresa == IdEmpresa
                                 && IdSucursal == q.IdSucursal
                                 && q.IdMovi_inven_tipo == IdMovi_inven_tipo
                                 orderby new { q.IdNumMovi } descending
                                 select new in_Ing_Egr_Inven_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                     IdBodega = q.IdBodega,
                                     IdNumMovi = q.IdNumMovi,
                                     IdMotivo_Inv = q.IdMotivo_Inv,
                                     Estado = q.Estado,
                                     signo = q.signo,
                                     cm_observacion = q.cm_observacion,
                                     CodMoviInven = q.CodMoviInven,
                                     cm_fecha = q.cm_fecha,
                                     tm_descripcion = t.tm_descripcion,
                                     
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
        public List<in_Ing_Egr_Inven_Info> get_list_x_aprobar(int IdEmpresa, int IdSucursal, int IdBodega)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;
                int IdBodegaIni = IdBodega;
                int IdBodegaFin = IdBodega == 0 ? 9999 : IdBodega;

                List<in_Ing_Egr_Inven_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.vwin_Ing_Egr_Inven_PorAprobar
                                where q.IdEmpresa == IdEmpresa
                                && IdSucursalIni <= q.IdSucursal
                                && q.IdSucursal <= IdSucursalFin
                                && IdBodegaIni <= q.IdBodega
                                && q.IdBodega <= IdBodegaFin
                             orderby new { q.IdNumMovi } descending
                                select new in_Ing_Egr_Inven_Info
                                {
                                    IdEmpresa = q.IdEmpresa,
                                    IdSucursal = q.IdSucursal,
                                    IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                    IdBodega = q.IdBodega,
                                    nom_bodega = q.bo_Descripcion,
                                    IdNumMovi = q.IdNumMovi,
                                    IdMotivo_Inv = q.IdMotivo_Inv,
                                    Estado = q.Estado,
                                    signo = q.signo,
                                    cm_observacion = q.cm_observacion,
                                    CodMoviInven = q.CodMoviInven,
                                    cm_fecha = q.cm_fecha,
                                    tm_descripcion = q.tm_descripcion,
                                    Desc_mov_inv = q.Desc_mov_inv,
                                    IdEstadoAproba = q.IdEstadoAproba,
                                    IdUsuarioAR = q.IdUsuarioAR,
                                    FechaAR = q.FechaAR,
                                    FechaDespacho = q.FechaDespacho,
                                    IdUsuarioDespacho = q.IdUsuarioDespacho,
                                    EstadoAprobacion = q.EstadoAprobacion,
                                    EstadoBool = q.Estado == "A" ? true : false

                                }).ToList();

                    Lista.ForEach(q => q.SecuencialID = q.IdEmpresa.ToString("00") + q.IdSucursal.ToString("00") + q.IdMovi_inven_tipo.ToString("00") + q.IdNumMovi.ToString("00000000"));
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool aprobarDB(in_Ing_Egr_Inven_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Ing_Egr_Inven Entity = Context.in_Ing_Egr_Inven.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdMovi_inven_tipo == info.IdMovi_inven_tipo && q.IdNumMovi == info.IdNumMovi);
                    if (Entity == null) return false;

                    if (Entity.IdEstadoAproba == "APRO")
                        return false;

                    Entity.IdUsuarioAR = info.IdUsuarioAR;
                    Entity.IdEstadoAproba = info.IdEstadoAproba = "APRO";
                    Entity.FechaAR = DateTime.Now;

                    Context.spINV_aprobacion_ing_egr(info.IdEmpresa, info.IdSucursal, info.IdBodega, info.IdMovi_inven_tipo, info.IdNumMovi);
                    Contabilizar(info.IdEmpresa, info.IdSucursal, info.IdMovi_inven_tipo, info.IdNumMovi, info.cm_observacion, info.cm_fecha);

                    Context.SaveChanges();
                }

                return true;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<in_Ing_Egr_Inven_Info> get_list_x_reversar(int IdEmpresa, int IdSucursal, int IdBodega, string IdSigno, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;
                int IdBodegaIni = IdBodega;
                int IdBodegaFin = IdBodega == 0 ? 9999 : IdBodega;

                List<in_Ing_Egr_Inven_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.vwin_Ing_Egr_Inven_PorReversar
                             where q.IdEmpresa == IdEmpresa
                             && IdSucursalIni <= q.IdSucursal
                             && q.IdSucursal <= IdSucursalFin
                             && IdBodegaIni <= q.IdBodega
                             && q.IdBodega <= IdBodegaFin
                             && q.signo == IdSigno
                             && q.cm_fecha >= fecha_ini
                             && q.cm_fecha <= fecha_fin
                             orderby new { q.IdNumMovi } descending
                             select new in_Ing_Egr_Inven_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                 IdBodega = q.IdBodega,
                                 nom_bodega = q.bo_Descripcion,
                                 IdNumMovi = q.IdNumMovi,
                                 IdMotivo_Inv = q.IdMotivo_Inv,
                                 Estado = q.Estado,
                                 signo = q.signo,
                                 cm_observacion = q.cm_observacion,
                                 CodMoviInven = q.CodMoviInven,
                                 cm_fecha = q.cm_fecha,
                                 tm_descripcion = q.tm_descripcion,
                                 Desc_mov_inv = q.Desc_mov_inv,
                                 IdEstadoAproba = q.IdEstadoAproba,
                                 IdUsuarioAR = q.IdUsuarioAR,
                                 FechaAR = q.FechaAR,
                                 FechaDespacho = q.FechaDespacho,
                                 IdUsuarioDespacho = q.IdUsuarioDespacho,
                                 EstadoAprobacion = q.EstadoAprobacion,
                                 EstadoBool = q.Estado == "A" ? true : false

                             }).ToList();

                    Lista.ForEach(q => q.SecuencialID = q.IdEmpresa.ToString("00") + q.IdSucursal.ToString("00") + q.IdMovi_inven_tipo.ToString("00") + q.IdNumMovi.ToString("00000000"));
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool reversarDB(in_Ing_Egr_Inven_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Ing_Egr_Inven Entity = Context.in_Ing_Egr_Inven.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdMovi_inven_tipo == info.IdMovi_inven_tipo && q.IdNumMovi == info.IdNumMovi);
                    if (Entity == null) return false;

                    Entity.IdUsuarioAR = info.IdUsuarioAR;
                    Entity.IdEstadoAproba = info.IdEstadoAproba = "XAPRO";
                    Entity.FechaAR = DateTime.Now;

                    ReversarAprobacion(info.IdEmpresa, info.IdSucursal, info.IdMovi_inven_tipo, info.IdNumMovi);

                    Context.SaveChanges();
                }

                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<in_Ing_Egr_Inven_Info> GetListPorDespachar(int IdEmpresa, int IdSucursal, int IdBodega)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;
                int IdBodegaIni = IdBodega;
                int IdBodegaFin = IdBodega == 0 ? 9999 : IdBodega;
                List<in_Ing_Egr_Inven_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.vwin_Ing_Egr_Inven_PorDespachar
                             where q.IdEmpresa == IdEmpresa
                             && IdSucursalIni <= q.IdSucursal
                             && q.IdSucursal <= IdSucursalFin
                             && IdBodegaIni <= q.IdBodega
                             && q.IdBodega <= IdBodegaFin
                             orderby new { q.IdNumMovi } descending
                             select new in_Ing_Egr_Inven_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdBodega = q.IdBodega,
                                 IdSucursal = q.IdSucursal,
                                 IdNumMovi = q.IdNumMovi,
                                 IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                 signo = q.signo,
                                 CodMoviInven = q.CodMoviInven,
                                 cm_observacion = q.cm_observacion,
                                 cm_fecha = q.cm_fecha,
                                 IdMotivo_Inv = q.IdMotivo_Inv,
                                 IdEstadoAproba = q.IdEstadoAproba,
                                 IdUsuarioAR = q.IdUsuarioAR,
                                 FechaAR = q.FechaAR,
                                 IdUsuarioDespacho = q.IdUsuarioDespacho,
                                 FechaDespacho = q.FechaDespacho,
                                 nom_bodega = q.bo_Descripcion,
                                 Desc_mov_inv = q.Desc_mov_inv,
                                 tm_descripcion = q.tm_descripcion,
                                 Estado = q.Estado,
                                 EstadoAprobacion = q.EstadoAprobacion,
                                 EstadoBool = q.Estado == "A" ? true : false

                             }).ToList();
                    Lista.ForEach(q => q.SecuencialID = q.IdEmpresa.ToString("00") + q.IdSucursal.ToString("00") + q.IdMovi_inven_tipo.ToString("00") + q.IdNumMovi.ToString("00000000"));

                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DespacharDB(in_Ing_Egr_Inven_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Ing_Egr_Inven Entity = Context.in_Ing_Egr_Inven.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdMovi_inven_tipo == info.IdMovi_inven_tipo && q.IdNumMovi == info.IdNumMovi);
                    if (Entity == null) return false;

                    Entity.IdUsuarioDespacho = info.IdUsuarioDespacho;
                    Entity.IdEstadoAproba = "APRO";
                    Entity.FechaDespacho = DateTime.Now;
                    
                    Context.SaveChanges();
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
