﻿using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
   public class ro_EmpleadoNovedadCargaMasiva_Data
    {
        public List<ro_EmpleadoNovedadCargaMasiva_Info> get_list(int IdEmpresa, DateTime FechaInicio, DateTime FechaFin, bool mostrar_anulados)
        {
            try
            {
                List<ro_EmpleadoNovedadCargaMasiva_Info> lista;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (mostrar_anulados)
                        lista = (from q in Context.ro_EmpleadoNovedadCargaMasiva
                                 where q.IdEmpresa == IdEmpresa
                                 && q.FechaCarga >= FechaInicio
                                 && q.FechaCarga <= FechaFin
                                 select new ro_EmpleadoNovedadCargaMasiva_Info
                                 {
                                     IdEmpresa=q.IdEmpresa,
                                     IdCarga=q.IdCarga,
                                     FechaCarga = q.FechaCarga,
                                     Observacion = q.Observacion,
                                     IdRubro = q.IdRubro,
                                     Estado = q.Estado
                                 }
                               ).ToList();
                    else
                    {
                        lista = (from q in Context.ro_EmpleadoNovedadCargaMasiva
                                 where q.IdEmpresa == IdEmpresa
                                 && q.FechaCarga >= FechaInicio
                                 && q.FechaCarga <= FechaFin
                                 && q.Estado==true
                                 select new ro_EmpleadoNovedadCargaMasiva_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCarga = q.IdCarga,
                                     FechaCarga = q.FechaCarga,
                                     Observacion = q.Observacion,
                                     IdRubro = q.IdRubro,
                                     Estado = q.Estado
                                 }
                              ).ToList();
                    }

                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool GuardarDB(ro_EmpleadoNovedadCargaMasiva_Info info)
        {
            try
            {

                using (Entities_rrhh Contex=new Entities_rrhh())
                {

                    ro_EmpleadoNovedadCargaMasiva entity = new ro_EmpleadoNovedadCargaMasiva
                    {
                        IdEmpresa=info.IdEmpresa,
                        IdCarga=info.IdCarga=Get_id(info.IdEmpresa),
                        FechaCarga=info.FechaCarga,
                        Observacion=info.Observacion,
                        IdRubro=info.IdRubro,
                        IdUsuario=info.IdUsuario,
                        Fecha_Transac=DateTime.Now,
                    };
                    Contex.ro_EmpleadoNovedadCargaMasiva.Add(entity);
                    Contex.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB (ro_EmpleadoNovedadCargaMasiva_Info info)
        {
            try
            {
                using (Entities_rrhh contex=new Entities_rrhh())
                {

                    ro_EmpleadoNovedadCargaMasiva entity = contex.ro_EmpleadoNovedadCargaMasiva.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCarga == info.IdCarga);

                    if (entity == null)
                        return false;
                    entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    entity.Estado = false;
                    contex.SaveChanges  ();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_EmpleadoNovedadCargaMasiva_Info get_info(int IdEmpresa, decimal IdCarga)
        {
            try
            {
                ro_EmpleadoNovedadCargaMasiva_Info info = new ro_EmpleadoNovedadCargaMasiva_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_EmpleadoNovedadCargaMasiva Entity = Context.ro_EmpleadoNovedadCargaMasiva.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCarga == IdCarga);
                    if (Entity == null) return null;

                    info = new ro_EmpleadoNovedadCargaMasiva_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCarga = Entity.IdCarga,
                        IdRubro = Entity.IdRubro,
                        FechaCarga = Entity.FechaCarga,
                        Estado = Entity.Estado,
                        Observacion = Entity.Observacion
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private decimal Get_id(int IdEmpresa)
        {
            try
            {
                decimal Id = 0;
                using (Entities_rrhh Contex=new Entities_rrhh())
                {
                    var list = from q in Contex.ro_EmpleadoNovedadCargaMasiva
                               select q;
                    if (list.Count() > 0)
                       Id=list.Max(v =>v.IdCarga)+1;

                    return Id;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}