﻿using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_tipo_nota_Data
    {
        public List<ba_tipo_nota_Info> get_list(int IdEmpresa,string CodTipo, bool mostrar_anulados)
        {
            try
            {
                List<ba_tipo_nota_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                 if(mostrar_anulados)
                        Lista = (from q in Context.ba_tipo_nota
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Tipo.Contains(CodTipo)
                                 select new ba_tipo_nota_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipoNota = q.IdTipoNota,
                                     Descripcion = q.Descripcion,
                                     IdCentroCosto = q.IdCentroCosto,
                                     IdCtaCble = q.IdCtaCble,
                                     Tipo = q.Tipo,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                 else
                        Lista = (from q in Context.ba_tipo_nota
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 && q.Tipo.Contains(CodTipo)
                                 select new ba_tipo_nota_Info
                                 {                                     
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipoNota = q.IdTipoNota,
                                     Descripcion = q.Descripcion,
                                     IdCentroCosto = q.IdCentroCosto,
                                     IdCtaCble = q.IdCtaCble,
                                     Tipo = q.Tipo,
                                     Estado = q.Estado,

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

        public ba_tipo_nota_Info get_info(int IdEmpresa, int IdTipoNota)
        {
            try
            {
                ba_tipo_nota_Info info = new ba_tipo_nota_Info();
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_tipo_nota Entity = Context.ba_tipo_nota.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdTipoNota == IdTipoNota);
                    if (Entity == null) return null;
                    info = new ba_tipo_nota_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoNota = Entity.IdTipoNota,
                        IdCentroCosto = Entity.IdCentroCosto,
                        IdCtaCble = Entity.IdCtaCble,
                        Descripcion = Entity.Descripcion,
                        Estado = Entity.Estado,
                        Tipo = Entity.Tipo
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;
                using (Entities_banco Context = new Entities_banco())
                {
                    var lst = from q in Context.ba_tipo_nota
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdTipoNota) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ba_tipo_nota_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_tipo_nota Entity = new ba_tipo_nota
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoNota = info.IdTipoNota=get_id(info.IdEmpresa),
                        IdCentroCosto = info.IdCentroCosto,
                        IdCtaCble = info.IdCtaCble,
                        Descripcion = info.Descripcion,
                        Estado = info.Estado="A",
                        Tipo = info.Tipo
                    };
                    Context.ba_tipo_nota.Add(Entity);
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ba_tipo_nota_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_tipo_nota Entity = Context.ba_tipo_nota.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoNota == info.IdTipoNota);
                    if (Entity == null) return false;

                    Entity.Descripcion = info.Descripcion;
                    Entity.Tipo = info.Tipo;
                    Entity.IdCtaCble = info.IdCtaCble;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ba_tipo_nota_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_tipo_nota Entity = Context.ba_tipo_nota.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoNota == info.IdTipoNota);
                    if (Entity == null) return false;

                    Entity.Estado = info.Estado="I";

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
