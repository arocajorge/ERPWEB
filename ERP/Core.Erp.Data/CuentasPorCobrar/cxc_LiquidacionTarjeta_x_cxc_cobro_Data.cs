﻿using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorCobrar
{
    public class cxc_LiquidacionTarjeta_x_cxc_cobro_Data
    {
        public List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> get_list_cobros_pendientes(int IdEmpresa, int IdSucursal)
        {
            try
            {
                List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> Lista;

                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    Lista = (from q in Context.vwcxc_LiquidacionTarjetaDet
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdLiquidacion == null
                             select new cxc_LiquidacionTarjeta_x_cxc_cobro_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 Valor = q.cr_TotalCobro,
                                 IdCobro = q.IdCobro,
                                 cr_fecha = q.cr_fecha
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
