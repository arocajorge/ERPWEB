﻿using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
   public class fa_guia_remision_Data
    {
        public List<fa_guia_remision_Info> get_list(int IdEmpresa, DateTime fecha_inicio, DateTime Fecha_fin)
        {
            try
            {
                List<fa_guia_remision_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_guia_remision
                             where q.IdEmpresa == IdEmpresa
                             && q.gi_fecha >= fecha_inicio
                             && q.gi_fecha <= Fecha_fin
                             select new fa_guia_remision_Info
                             {
                                IdEmpresa = q.IdEmpresa,
                                IdSucursal=q.IdSucursal,
                                IdBodega=q.IdBodega,
                                IdGuiaRemision=q.IdGuiaRemision,
                                CodGuiaRemision=q.CodGuiaRemision,
                                CodDocumentoTipo=q.CodDocumentoTipo,
                                Serie1=q.Serie1,
                                Serie2=q.Serie2,
                                NumGuia_Preimpresa=q.Serie1+"-"+q.Serie2+"-"+ q.NumGuia_Preimpresa,
                                NUAutorizacion=q.NUAutorizacion,
                                Fecha_Autorizacion=q.Fecha_Autorizacion,
                                IdCliente=q.IdCliente,
                                IdTransportista=q.IdTransportista,
                                gi_fecha=q.gi_fecha,
                                gi_plazo=q.gi_plazo,
                                gi_fech_venc=q.gi_fech_venc,
                                gi_Observacion=q.gi_Observacion,
                                Impreso=q.Impreso,
                                gi_FechaInicioTraslado=q.gi_FechaInicioTraslado,
                                gi_FechaFinTraslado=q.gi_FechaFinTraslado,
                                placa=q.placa,
                                ruta=q.ruta,
                                Direccion_Destino=q.Direccion_Destino,
                                Direccion_Origen=q.Direccion_Origen,
                                Estado=q.Estado,
                                pe_nombreCompleto=q.pe_nombreCompleto,
                                pe_cedulaRuc=q.pe_cedulaRuc,

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

        public fa_guia_remision_Info get_info(int IdEmpresa, decimal IdGuiaRemision)
        {
            try
            {
                fa_guia_remision_Info info = new fa_guia_remision_Info();
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_guia_remision Entity = Context.fa_guia_remision.FirstOrDefault(q => q.IdGuiaRemision == IdGuiaRemision && q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new fa_guia_remision_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        IdGuiaRemision = Entity.IdGuiaRemision,
                        CodGuiaRemision = Entity.CodGuiaRemision,
                        CodDocumentoTipo = Entity.CodDocumentoTipo,
                        Serie1 = Entity.Serie1,
                        Serie2 = Entity.Serie2,
                        NumGuia_Preimpresa = Entity.NumGuia_Preimpresa,
                        NUAutorizacion = Entity.NUAutorizacion,
                        Fecha_Autorizacion = Entity.Fecha_Autorizacion,
                        IdCliente = Entity.IdCliente,
                        IdTransportista = Entity.IdTransportista,
                        gi_fecha = Entity.gi_fecha,
                        gi_plazo = Entity.gi_plazo,
                        gi_fech_venc = Entity.gi_fech_venc,
                        gi_Observacion = Entity.gi_Observacion,
                        Impreso = Entity.Impreso,
                        gi_FechaInicioTraslado = Entity.gi_FechaInicioTraslado,
                        gi_FechaFinTraslado = Entity.gi_FechaFinTraslado,
                        placa = Entity.placa,
                        ruta = Entity.ruta,
                        Direccion_Destino = Entity.Direccion_Destino,
                        Direccion_Origen = Entity.Direccion_Origen,
                        Estado = Entity.Estado,
                        IdCatalogo_traslado=Entity.IdCatalogo_traslado
                    };
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
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_guia_remision
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdGuiaRemision) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_guia_remision_Info info)
        {
            try
            {
                int secuencia = 1;

                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_guia_remision Entity = new fa_guia_remision
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdGuiaRemision =info.IdGuiaRemision= get_id(info.IdEmpresa),
                        CodGuiaRemision = info.CodGuiaRemision,
                        CodDocumentoTipo = info.CodDocumentoTipo,
                        Serie1 = info.Serie1,
                        Serie2 = info.Serie2,
                        NumGuia_Preimpresa = info.NumGuia_Preimpresa,
                        NUAutorizacion = info.NUAutorizacion,
                        Fecha_Autorizacion = info.Fecha_Autorizacion,
                        IdCliente = info.IdCliente,
                        IdContacto=info.IdContacto,
                        IdTransportista = info.IdTransportista,
                        gi_fecha = info.gi_fecha.Date,
                        gi_plazo = info.gi_plazo,
                        gi_fech_venc = info.gi_fech_venc,
                        gi_Observacion = info.gi_Observacion,
                        Impreso = info.Impreso,
                        gi_FechaInicioTraslado = info.gi_FechaInicioTraslado.Date,
                        gi_FechaFinTraslado = info.gi_FechaFinTraslado.Date,
                        placa = info.placa,
                        ruta = info.ruta,
                        Direccion_Destino = info.Direccion_Destino,
                        Direccion_Origen = info.Direccion_Origen,
                        IdCatalogo_traslado=info.IdCatalogo_traslado,
                        Estado = info.Estado="A",
                        IdUsuario=info.IdUsuario,
                        Fecha_Transac=info.Fecha_Transac=DateTime.Now

                    };
                    Context.fa_guia_remision.Add(Entity);
                    foreach (var item in info.lst_detalle)
                    {
                        Context.fa_guia_remision_det.Add(new fa_guia_remision_det
                        {
                            IdEmpresa=info.IdEmpresa,
                            IdSucursal=info.IdSucursal,
                            IdBodega=info.IdBodega,
                            IdGuiaRemision=info.IdGuiaRemision,
                            Secuencia=secuencia,
                            IdProducto=item.IdProducto,
                            gi_cantidad=item.gi_cantidad,
                            gi_detallexItems =item.gi_detallexItems
                        });
                        Context.fa_guia_remision_det_x_factura.Add(new fa_guia_remision_det_x_factura
                        {
                            IdEmpresa_fact = info.IdEmpresa,
                            IdSucursal_fact = info.IdSucursal,
                            IdBodega_fact = info.IdBodega,
                            IdCbteVta_fact = Convert.ToDecimal(item.IdCbteVta),
                            IdGuiaRemision_guia = info.IdGuiaRemision,
                            Secuencia_fact = Convert.ToInt32(item.Secuencia_fact),
                            Secuencia_guia = secuencia,
                            IdBodega_guia = info.IdBodega,
                            IdSucursal_guia = info.IdSucursal,
                            IdEmpresa_guia = info.IdEmpresa
                        });
                        secuencia++;
                    }
                    foreach (var item in info.lst_detalle_x_factura)
                    {
                        Context.fa_factura_x_fa_guia_remision.Add(new fa_factura_x_fa_guia_remision
                        {
                            fa_IdEmpresa = info.IdEmpresa,
                            fa_IdSucursal = info.IdSucursal,
                            fa_IdBodega = info.IdBodega,
                            fa_IdCbteVta = item.IdCbteVta,
                            gi_IdEmpresa = info.IdEmpresa,
                            gi_IdSucursal = info.IdSucursal,
                            gi_IdBodega = info.IdBodega,
                            gi_IdGuiaRemision = info.IdGuiaRemision
                        });
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

        public bool modificarDB(fa_guia_remision_Info info)
        {
            try
            {
                int secuencia = 1;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_guia_remision Entity = Context.fa_guia_remision.FirstOrDefault(q => q.IdGuiaRemision == info.IdGuiaRemision);
                    if (Entity == null) return false;
                       Entity.CodGuiaRemision = info.CodGuiaRemision;
                        Entity.CodDocumentoTipo = info.CodDocumentoTipo;
                        Entity.Serie1 = info.Serie1;
                        Entity.Serie2 = info.Serie2;
                        Entity.NumGuia_Preimpresa = info.NumGuia_Preimpresa;
                        Entity.NUAutorizacion = info.NUAutorizacion;
                        Entity.Fecha_Autorizacion = info.Fecha_Autorizacion;
                        Entity.IdCliente = info.IdCliente;
                        Entity.IdTransportista = info.IdTransportista;
                        Entity.gi_fecha = info.gi_fecha.Date;
                        Entity.gi_plazo = info.gi_plazo;
                        Entity.gi_fech_venc = info.gi_fech_venc;
                        Entity.Impreso = info.Impreso;
                        Entity.gi_FechaInicioTraslado = info.gi_FechaInicioTraslado.Date;
                        Entity.gi_FechaFinTraslado = info.gi_FechaFinTraslado.Date;
                        Entity.placa = info.placa;
                        Entity.ruta = info.ruta;
                        Entity.Direccion_Destino = info.Direccion_Destino;
                        Entity.Direccion_Origen = info.Direccion_Origen;
                    Entity.IdCatalogo_traslado = info.IdCatalogo_traslado;
                        Entity.Fecha_UltMod = info.Fecha_UltMod=DateTime.Now;
                        Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.gi_Observacion = info.gi_Observacion;
                    foreach (var item in info.lst_detalle)
                    {
                        Context.fa_guia_remision_det.Add(new fa_guia_remision_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdBodega = info.IdBodega,
                            IdGuiaRemision = info.IdGuiaRemision,
                            Secuencia = secuencia,
                            IdProducto = item.IdProducto,
                            gi_cantidad = item.gi_cantidad,
                            gi_detallexItems = item.gi_detallexItems
                        });
                        Context.fa_guia_remision_det_x_factura.Add(new fa_guia_remision_det_x_factura
                        {
                            IdEmpresa_fact = info.IdEmpresa,
                            IdSucursal_fact = info.IdSucursal,
                            IdBodega_fact = info.IdBodega,
                            IdCbteVta_fact = Convert.ToDecimal(item.IdCbteVta),
                            IdGuiaRemision_guia = info.IdGuiaRemision,
                            Secuencia_fact = Convert.ToInt32(item.Secuencia_fact),
                            Secuencia_guia = secuencia,
                            IdBodega_guia = info.IdBodega,
                            IdSucursal_guia=info.IdSucursal,
                            IdEmpresa_guia=info.IdEmpresa
                        
                        });
                        secuencia++;
                    }
                    foreach (var item in info.lst_detalle_x_factura)
                    {
                        Context.fa_factura_x_fa_guia_remision.Add(new fa_factura_x_fa_guia_remision
                        {
                            fa_IdEmpresa = info.IdEmpresa,
                            fa_IdSucursal = info.IdSucursal,
                            fa_IdBodega = info.IdBodega,
                            fa_IdCbteVta = item.IdCbteVta,
                            gi_IdEmpresa = info.IdEmpresa,
                            gi_IdSucursal = info.IdSucursal,
                            gi_IdBodega = info.IdBodega,
                            gi_IdGuiaRemision = info.IdGuiaRemision
                        });
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
        public bool anularDB(fa_guia_remision_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_guia_remision Entity = Context.fa_guia_remision.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdGuiaRemision == info.IdGuiaRemision);
                    if (Entity == null) return false;
                    Entity.Estado = info.Estado = "I";
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
                    Entity.MotiAnula = info.MotiAnula;

                    var lst_det = Context.fa_guia_remision_det_x_factura.Where(q => q.IdEmpresa_guia == info.IdEmpresa && q.IdGuiaRemision_guia == info.IdGuiaRemision).ToList();
                    Context.fa_guia_remision_det_x_factura.RemoveRange(lst_det);

                    var lst_fac = Context.fa_factura_x_fa_guia_remision.Where(q => q.gi_IdEmpresa == info.IdEmpresa && q.gi_IdGuiaRemision == info.IdGuiaRemision).ToList();
                    Context.fa_factura_x_fa_guia_remision.RemoveRange(lst_fac);

                    Context.SaveChanges();

                    
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool si_existe(int IdEmpresa, string Establecimiento, string Puntoemision, string Numero)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_guia_remision
                              where q.IdEmpresa == IdEmpresa
                              && q.Serie1==Establecimiento
                              && q.Serie2==Puntoemision
                              && q.NumGuia_Preimpresa==null
                              select q;
                    if (lst.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
