﻿using Core.Erp.Info.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Caja
{
    public class caj_parametro_Data
    {
        public caj_parametro_Info get_info (int IdEmpresa)
        {
            try
            {
                caj_parametro_Info info = new caj_parametro_Info();
                using (Entities_caja Context = new Entities_caja())
                {
                    caj_parametro Entity = Context.caj_parametro.FirstOrDefault(q => q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new caj_parametro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoCbteCble_MoviCaja_Egr = Entity.IdTipoCbteCble_MoviCaja_Egr,
                        IdTipoCbteCble_MoviCaja_Egr_Anu = Entity.IdTipoCbteCble_MoviCaja_Egr_Anu,
                        IdTipoCbteCble_MoviCaja_Ing_Anu = Entity.IdTipoCbteCble_MoviCaja_Ing_Anu,
                        IdTipoCbteCble_MoviCaja_Ing = Entity.IdTipoCbteCble_MoviCaja_Ing,
                        IdTipo_movi_ing_x_reposicion = Entity.IdTipo_movi_ing_x_reposicion

                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(caj_parametro_Info info)
        {
            try
            {
                using (Entities_caja Context = new Entities_caja())
                {
                    caj_parametro Entity = Context.caj_parametro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null)
                    {
                        Entity = new caj_parametro
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdTipoCbteCble_MoviCaja_Egr = info.IdTipoCbteCble_MoviCaja_Egr,
                            IdTipoCbteCble_MoviCaja_Egr_Anu = info.IdTipoCbteCble_MoviCaja_Egr_Anu,
                            IdTipoCbteCble_MoviCaja_Ing = info.IdTipoCbteCble_MoviCaja_Ing,
                            IdTipoCbteCble_MoviCaja_Ing_Anu = info.IdTipoCbteCble_MoviCaja_Ing_Anu,
                            IdTipo_movi_ing_x_reposicion = info.IdTipo_movi_ing_x_reposicion,

                            IdUsuario = info.IdUsuario,
                            Fecha_Transac = DateTime.Now
                        };
                        Context.caj_parametro.Add(Entity);
                    }
                    else
                    {
                        Entity.IdTipoCbteCble_MoviCaja_Egr = info.IdTipoCbteCble_MoviCaja_Egr;
                        Entity.IdTipoCbteCble_MoviCaja_Egr_Anu = info.IdTipoCbteCble_MoviCaja_Egr_Anu;
                        Entity.IdTipoCbteCble_MoviCaja_Ing = info.IdTipoCbteCble_MoviCaja_Ing;
                        Entity.IdTipoCbteCble_MoviCaja_Ing_Anu = info.IdTipoCbteCble_MoviCaja_Ing_Anu;
                        Entity.IdTipo_movi_ing_x_reposicion = info.IdTipo_movi_ing_x_reposicion;

                        Entity.FechaUltMod = info.FechaUltMod;
                    }
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
