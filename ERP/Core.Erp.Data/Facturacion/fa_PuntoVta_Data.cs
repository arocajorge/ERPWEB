﻿using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
    public class fa_PuntoVta_Data
    {

        public List<fa_PuntoVta_Info> get_list(int IdEmpresa)
        {
            try
            {
                List<fa_PuntoVta_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.fa_PuntoVta
                             where q.IdEmpresa == IdEmpresa
                             select new fa_PuntoVta_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdPuntoVta = q.IdPuntoVta,
                                 cod_PuntoVta = q.cod_PuntoVta,
                                 nom_PuntoVta = q.nom_PuntoVta,
                                 estado = q.estado == true

                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_PuntoVta_Info> get_list(int IdEmpresa, int IdSucursal, bool mostrar_anulados)
        {
            try
            {
                List<fa_PuntoVta_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    if (!mostrar_anulados)
                        Lista = (from q in Context.fa_PuntoVta
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.estado == true
                                 select new fa_PuntoVta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdBodega = q.IdBodega,
                                     IdPuntoVta = q.IdPuntoVta,
                                     cod_PuntoVta = q.cod_PuntoVta,
                                     nom_PuntoVta = q.nom_PuntoVta,
                                     estado = q.estado

                                 }).ToList();
                    else
                        Lista = (from q in Context.fa_PuntoVta
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 select new fa_PuntoVta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdBodega = q.IdBodega,
                                     IdPuntoVta = q.IdPuntoVta,
                                     cod_PuntoVta = q.cod_PuntoVta,
                                     nom_PuntoVta = q.nom_PuntoVta,
                                     estado = q.estado
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public fa_PuntoVta_Info get_info(int IdEmpresa, int IdSucursal, int IdPuntoVta)
        {
            try
            {
                fa_PuntoVta_Info info = new fa_PuntoVta_Info();
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_PuntoVta Entity = Context.fa_PuntoVta.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal  && q.IdPuntoVta == IdPuntoVta);
                    if (Entity == null) return null;
                    info = new fa_PuntoVta_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        IdPuntoVta = Entity.IdPuntoVta,
                        cod_PuntoVta = Entity.cod_PuntoVta,
                        nom_PuntoVta = Entity.nom_PuntoVta,
                        estado = Entity.estado

                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int get_id(int IdEmpresa, int IdSucursal)
        {

            try
            {
                int ID = 1;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_PuntoVta
                              where q.IdEmpresa == IdEmpresa
                              where q.IdSucursal == IdSucursal
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdPuntoVta) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_PuntoVta_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_PuntoVta Entity = new fa_PuntoVta
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdPuntoVta = info.IdPuntoVta = get_id(info.IdEmpresa, info.IdSucursal),
                        cod_PuntoVta = info.cod_PuntoVta,
                         nom_PuntoVta = info.nom_PuntoVta,
                        estado = info.estado = true
                    };
                    Context.fa_PuntoVta.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(fa_PuntoVta_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_PuntoVta Entity = Context.fa_PuntoVta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal  && q.IdPuntoVta == info.IdPuntoVta);
                    if (Entity == null) return false;
                    
                    Entity.cod_PuntoVta = info.cod_PuntoVta;
                    Entity.nom_PuntoVta = info.nom_PuntoVta;

                    Context.SaveChanges();
                
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(fa_PuntoVta_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_PuntoVta Entity = Context.fa_PuntoVta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdPuntoVta == info.IdPuntoVta);
                    if (Entity == null) return false;

                    Entity.estado = Entity.estado = false;

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
